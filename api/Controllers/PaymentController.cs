using api.Data;
using api.Dtos.Payments;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace api.Controllers
{
    [Route("api/payments")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly APIContext _context;
        public PaymentController(APIContext context)
        {
        _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult GetByUserId([FromRoute] int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                var errorResponse = new {
                    success = false,
                    message = "UserNotFound"
                };
                return NotFound(errorResponse);
            }
            var payments = _context.Payments.Where(x => x.UserId == id).ToList();
            var successResponse = new {
                success = true,
                message = "ok",
                data = payments
            };
            return Ok(successResponse);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentDto PaymentDto) {
            var user = _context.Users.Where(x => x.UserId == PaymentDto.UserId).FirstOrDefault();
            if (user == null)
            {
                var errorResponse = new {
                    success = false,
                    message = "UserNotFound"
                };
                return NotFound(errorResponse);
            }
            var auction = _context.Auctions.Where(x => x.AuctionId == PaymentDto.AuctionId).FirstOrDefault();
            if (auction == null)
            {
                var errorResponse = new {
                    success = false,
                    message = "AuctionNotFound"
                };
                return NotFound(errorResponse);
            }
            if (PaymentDto.Amount < 0)
            {
                var errorResponse = new {
                    success = false,
                    message = "InvalidAmount"
                };
                return BadRequest(errorResponse);
            }
            if (PaymentDto.Type != "StartingBid" && PaymentDto.Type != "AuctionPurchase")
            {
                var errorResponse = new {
                    success = false,
                    message = "InvalidType"
                };
                return BadRequest(errorResponse);
            }
            var payment = new Payment
            {
                UserId = PaymentDto.UserId,
                AuctionId = PaymentDto.AuctionId,
                Amount = PaymentDto.Amount,
                PaymentMethod = "card",
                Type = PaymentDto.Type,
                IsOK = true,
                PaymentStatus = "paid"
            };
            _context.Payments.Add(payment);


            StripeConfiguration.ApiKey = "sk_test_51QBE7uDOXxHdMorR4utkjoNQ9Lem2QepUwCy3tyr7lIeZbhJSkYP9y7i5pXcPkrpN3ovO01zjWaZAKULMD8cMymc00iHkBI4fX";
            // StartingBids only
            if (PaymentDto.Type == "StartingBid") {

            
                _context.Notifications.Add(new Notification
                {
                    UserId = PaymentDto.UserId,
                    Title = "Payment successful",
                    Message = "Your payment was successful. Now you can bid on the auction any number of times.",
                    Link = "/mypayments",
                });
                var bidModel = new Bid {
                    AuctionId = PaymentDto.AuctionId,
                    BidderId = PaymentDto.UserId,
                    Status = "active",
                    BidAmount = auction.StartingBid + 10,
                    BidderName = user.FirstName + " " + user.LastName
                };
                _context.Bids.Add(bidModel);
                
                _context.Notifications.Add(new Notification {
                    UserId = PaymentDto.UserId,
                    Message = $"You have successfully created a bid for {auction.Title}",
                    Title = "Bid Created",
                    Link = $"/auction/{auction.AuctionId}"
                });
                
                _context.Notifications.Add(new Notification {
                    UserId = auction.SellerId,
                    Message = $"You have a new bid for {auction.Title} from {user.FirstName} {user.LastName}",
                    Title = "New Bid",
                    Link = $"/auction/{auction.AuctionId}"
                });
                
                _context.SaveChanges();
            }
            if (PaymentDto.Type == "AuctionPurchase") {
                var ThatBid = _context.Bids.Where(x => x.BidderId == PaymentDto.UserId && x.AuctionId == PaymentDto.AuctionId && x.Status == "active").OrderByDescending(x => x.BidAmount).ToList().FirstOrDefault();
                if (ThatBid == null)
                {
                    var errorResponse = new {
                        success = false,
                        message = "NoActiveBid"
                    };
                    return BadRequest(errorResponse);
                }
                ThatBid.Status = "paid";
                
                _context.Notifications.Add(new Notification
                {
                    UserId = PaymentDto.UserId,
                    Title = "Payment successful - You bought the item",
                    Message = "Your payment was successful. You have bought the item.",
                    Link = "/auction/" + PaymentDto.AuctionId,
                });

                _context.Notifications.Add(new Notification
                {
                    UserId = auction.SellerId,
                    Title = "Your item sold",
                    Message = "Your item was bought by " + user.FirstName + " " + user.LastName,
                    Link = "/auction/" + PaymentDto.AuctionId,
                });

                _context.SaveChanges();

                var other_bidders = _context.Bids.Where(x => x.AuctionId == PaymentDto.AuctionId && x.Status == "active").ToList();
                foreach (var bidder in other_bidders)
                {
                    bidder.Status = "exited";
                    if (bidder.BidderId != PaymentDto.UserId) {

                    _context.Notifications.Add(new Notification
                    {
                        UserId = bidder.BidderId,
                        Title = "Auction is bought by someone else",
                        Message = "The auction is bought by someone else. You can no longer bid on this auction. Your starting bid money will be refunded.",
                        Link = "/auction/" + PaymentDto.AuctionId,
                    });
                    }
                }

                _context.SaveChanges();
            }

            var options = new PaymentIntentCreateOptions
                {
                    Amount = (int)(PaymentDto.Amount * 100), // Amount in cents ($50.00)
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" },
                };

            var service = new PaymentIntentService();
            PaymentIntent intent = await service.CreateAsync(options);

            return Ok(new { clientSecret = intent.ClientSecret });
        } 


        [HttpPost]
        [Route("info")]
        public IActionResult GetInfo([FromBody] GetInfoDto GetInfo) 
        {
            var auction = _context.Auctions.FirstOrDefault(x => x.AuctionId == GetInfo.AuctionId);
            
            if (auction == null)
            {
                var errorResponse = new
                {
                    success = false,
                    message = "AuctionNotFound"
                };
                return BadRequest(errorResponse);
            }
            var user = _context.Users.FirstOrDefault(x => x.UserId == GetInfo.UserId);
            if (user == null)
            {
                var errorResponse = new
                {
                    success = false,
                    message = "UserNotFound"
                };
                return BadRequest(errorResponse);
            }
            if (GetInfo.Type == "AuctionPurchase") {
                var errorResponse = new
                {
                    success = true,
                    message = "ok",
                    data = new
                    {
                        auction = auction,
                        amount = auction.WinningBid - (auction.StartingBid * 0.1m),
                        calc = new {
                            previousPayment = _context.Payments.FirstOrDefault(x => x.UserId == GetInfo.UserId && x.AuctionId == GetInfo.AuctionId && x.Type == "StartingBid")?.Amount ?? 0,
                            total = auction.WinningBid,
                            amount = auction.WinningBid - (auction.StartingBid * 0.1m),
                        }
                    }
                };
                return Ok(errorResponse);
            }
            
            if (GetInfo.Type == "StartingBid") {
                var successResponse = new
                {
                    success = true,
                    message = "ok",
                    data = new
                    {
                        auction = auction,
                        amount = auction.StartingBid * 0.1m
                    }
                };
                return Ok(successResponse);
            }
            return NotFound();
        }

        [HttpPost("check")]
        public async Task<IActionResult> CreatePaymentIntent([FromBody] CreatePaymentDto PaymentDto)
        {
            StripeConfiguration.ApiKey = "sk_test_51QBE7uDOXxHdMorR4utkjoNQ9Lem2QepUwCy3tyr7lIeZbhJSkYP9y7i5pXcPkrpN3ovO01zjWaZAKULMD8cMymc00iHkBI4fX";

            var options = new PaymentIntentCreateOptions
            {
                Amount = (int)(PaymentDto.Amount * 100), // Amount in cents ($50.00)
                Currency = "usd",
                PaymentMethodTypes = new List<string> { "card" },
            };

            var service = new PaymentIntentService();
            PaymentIntent intent = await service.CreateAsync(options);

            return Ok(new { clientSecret = intent.ClientSecret });
        }
        
        
        [HttpPost]
        [Route("my")]
        public IActionResult GetMyPayments([FromBody] GetMyPaymentsDto paymentsDto)
        {
            var user = _context.Users.FirstOrDefault(x => x.UserId == paymentsDto.UserId);
            if (user == null)
            {
                var errorResponse = new
                {
                    success = false,
                    message = "UserNotFound"
                };
                return NotFound(errorResponse);
            }

            // Retrieve payments for the user
            var payments = _context.Payments
                .Where(x => x.UserId == paymentsDto.UserId)
                .Select(payment => new
                {
                    payment, // Include payment details
                    auction = _context.Auctions.FirstOrDefault(a => a.AuctionId == payment.AuctionId)!.ToAuctionsDtoGet() // Include auction details
                })
                .ToList();

            var successResponse = new
            {
                success = true,
                message = "ok",
                data = payments
            };

            return Ok(successResponse);
        }


    }
}
