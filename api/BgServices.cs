using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using api.Data;
using api.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace api
{
    public class BgServices : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public BgServices(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine("Background service is running");

                // Call the method to update auction statuses
                await UpdateAuctionStatusesAsync();
                await UpdateWinnerAsync();

                // Wait for 1 minute before running the task again
                await Task.Delay(TimeSpan.FromMinutes(2), stoppingToken);
            }
        }

        public async Task UpdateWinnerAsync()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<APIContext>();

                var closedAuctions = _context.Auctions.Where(x => x.Status == "closed").ToList();

                foreach (var auction in closedAuctions)
                {
                    var bids = _context.Bids
                        .Where(x => x.AuctionId == auction.AuctionId && (x.Status == "active" || x.Status == "paid"))
                        .OrderByDescending(x => x.BidAmount) // Sort bids from highest to lowest
                        .ToList();
                    Console.WriteLine("Bids count: " + bids.Count);
                    if (bids.Count == 0)
                    {
                        auction.WinnerId = null;
                        auction.WinningBid = null;
                        _context.SaveChanges();
                        continue; // No bids available, so no winner
                    }

                    bool winnerFound = false;

                    foreach (var bid in bids)
                    {
                        // Skip bidders who are already disqualified
                        if (bid.Status == "Not qualified")
                        {
                            if (auction.WinnerId == bid.BidderId)
                            {
                                auction.WinnerId = null;
                                auction.WinningBid = null;
                            }
                            continue; // Skip to the next bid
                        }

                        // If this bid has already been marked as paid, they are the winner
                        if (bid.Status == "paid")
                        {
                            auction.WinnerId = bid.BidderId;
                            auction.WinningBid = bid.BidAmount;
                            winnerFound = true;
                            break; // Stop checking as we found a winner
                        }

                        int bidIndex = bids.IndexOf(bid);
                        int allowedTimeWindow = (bidIndex + 1) * 24; // 24 hours for the first bidder, 48 for the second, etc.

                        // Calculate the time since the auction ended
                        TimeSpan timeSinceAuctionEnd = DateTime.Now - auction.EndTime;


                        if (timeSinceAuctionEnd.TotalHours > allowedTimeWindow)
                        {
                            // Mark the bidder as not qualified if they missed the payment window
                            bid.Status = "Not qualified";
                            // Update all bids from the same bidder to "Not qualified"
                            var allBidsFromBidder = bids.Where(b => b.BidderId == bid.BidderId).ToList();
                            foreach (var bidderBid in allBidsFromBidder)
                            {
                                bidderBid.Status = "Not qualified"; // Update each bid status
                            }

                            Notification notification = new Notification
                            {
                                UserId = bid.BidderId,
                                Title = "Auction Disqualification",
                                Link = "/auction/" + auction.AuctionId,
                                Message = "You have been disqualified from the auction for failing to pay within the required time window (24 hrs after the previous member has discolified / 24 hours after the auction end time). Now you cannot refund the amount you paid."
                            };
                            _context.Notifications.Add(notification);
                            _context.SaveChanges(); // Save the status update immediately
                            continue;

                            // Check if the bid is still active after marking as not qualified
                        }
                        if (bid.Status == "active")
                        {
                            auction.WinnerId = bid.BidderId;
                            auction.WinningBid = bid.BidAmount;
                            winnerFound = true;
                            var is_notified = _context.Notifications.Where(x => x.UserId == bid.BidderId && x.Title == "Auction Win" && x.Link == ("/auction/" + auction.AuctionId));
                            if (is_notified.Count() == 0){
                                Notification notification = new Notification
                                {
                                    UserId = bid.BidderId,
                                    Title = "Auction Win",
                                    Link = "/auction/" + auction.AuctionId,
                                    Message = "Congratulations! You have won the auction. Please pay within 24 hours to claim your prize. If you fail to pay within the required time window, you will be disqualified and the next highest bidder will be selected as the winner. Thank you for participating in the auction."
                                };
                                _context.Notifications.Add(notification);                                                       
                            }
                            break; // Stop checking as we found a winner
                        }
                    }

                    // Save the auction details if a winner was found
                    if (winnerFound)
                    {
                        _context.SaveChanges();
                    }
                }
            }
        }



        private async Task UpdateAuctionStatusesAsync()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<APIContext>();

                var liveAuctions = _context.Auctions
                    .Where(auction => auction.IsLive == "yes")
                    .ToList();

                // Loop through each auction to check its status
                foreach (var auction in liveAuctions)
                {
                    if (auction.EndTime < DateTime.Now)
                    {
                        auction.Status = "closed";
                    }
                    else
                    {
                        auction.Status = "active";
                    }
                }

                // Save any changes to the database
                await _context.SaveChangesAsync();
            }
        }
    }
}
