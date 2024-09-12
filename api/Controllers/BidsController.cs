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
            
            
            var bidModel = new Bid {
                AuctionId = createBid.AuctionId,
                BidderId = createBid.BidderId,
                Status = createBid.Status,
                BidAmount = createBid.BidAmount
            };
            
            _context.Bids.Add(bidModel);
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
