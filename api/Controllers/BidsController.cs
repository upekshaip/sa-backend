using api.Data;
using api.Dtos.Bids;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/bids")]
    [ApiController]
    public class BidsController : ControllerBase
    {
        private readonly APIContext _context;
        public BidsController(APIContext context)
        {
        _context = context;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var bids =  _context.Bids.ToList().Select(s => s.ToBidDtoGet());
            return Ok(bids);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var bid = _context.Bids.FirstOrDefault(x => x.BidId == id);
            if (bid == null)
            {
                var errorResponse = new {
                    success = false,
                    message = "BidNotFound"
                };
                return NotFound(errorResponse);
            }
            var bidder = _context.Users.FirstOrDefault(x => x.UserId == bid.BidderId);
            if (bidder == null)
            {
                var errorResponse = new {
                    success = false,
                    message = "BidderNotFound"
                };
                return NotFound(errorResponse);
            }
            var auction = _context.Auctions.FirstOrDefault(x => x.AuctionId == bid.AuctionId);
            if (auction == null)
            {
                var errorResponse = new {
                    success = false,
                    message = "AuctionNotFound"
                };
                return NotFound(errorResponse);
            }
            var successResponse = new {
                success = true,
                message = "ok",
                data = new {
                    bid = bid,
                    bidder = bidder.ToUserDtoGet(),
                    auction = auction.ToAuctionsDtoGet()
                },
            };
            return Ok(successResponse);
        }

        [Route("create")]
        [HttpPost]
        public IActionResult CreateBid([FromBody] CreateBidDto createBid)
        {
            var auction =  _context.Auctions.FirstOrDefault(x => x.AuctionId == createBid.AuctionId);
            if (auction == null)
            {
                var errorResponse = new {
                    success = false,
                    message = "AuctionNotFound"
                };
                return NotFound(errorResponse);
            }
            var bidder =  _context.Users.FirstOrDefault(x => x.UserId == createBid.BidderId);
            if (bidder == null)
            {
                var errorResponse = new {
                    success = false,
                    message = "BidderNotFound"
                };
                return NotFound(errorResponse);
            }

            var payment = _context.Payments.FirstOrDefault(x => x.UserId == createBid.BidderId && x.AuctionId == createBid.AuctionId && x.Type == "StartingBid");        
            if (payment == null) {
                var makePaymentResponse = new {
                    success = false,
                    data = new {
                        amount = auction.StartingBid * (decimal)0.1,
                        type = "StartingBid",
                        auctionId = auction.AuctionId,
                        userId = bidder.UserId,
                    },
                    message = "Need to make a payment to start bidding"
                };
                return Ok(makePaymentResponse);
            }

            
            var bidModel = new Bid {
                AuctionId = createBid.AuctionId,
                BidderId = createBid.BidderId,
                Status = createBid.Status,
                BidAmount = createBid.BidAmount,
                BidderName = bidder.FirstName + " " + bidder.LastName
            };
            
            _context.Bids.Add(bidModel);
            _context.SaveChanges();
            
            _context.Notifications.Add(new Notification {
                UserId = bidModel.BidderId,
                Message = $"You have successfully created a bid for {auction.Title}",
                Title = "Bid Created",
                Link = $"/auction/{auction.AuctionId}"
            });
            _context.Notifications.Add(new Notification {
                UserId = auction.SellerId,
                Message = $"You have a new bid for {auction.Title} from {bidder.FirstName} {bidder.LastName}",
                Title = "New Bid",
                Link = $"/auction/{auction.AuctionId}"
            });
            _context.SaveChanges();

            return Ok(new
            { 
            success = true, 
            message = "ok", 
            data = bidModel.ToBidDtoGet()
            });
        }
    }
}
