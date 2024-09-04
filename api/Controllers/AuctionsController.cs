using api.Data;
using api.Dtos.Auctions;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;


namespace api.Controllers
{
    [Route("api/auctions")]
    [ApiController]
    public class AuctionsController : ControllerBase
    {
        private readonly APIContext _context;
        public AuctionsController(APIContext context)
        {
        _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var auctions =  _context.Auctions.ToList().Select(s => s.ToAuctionsDtoGet());
            return Ok(auctions);
        }

        
        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var auction = _context.Auctions.FirstOrDefault(x => x.AuctionId == id);
            if (auction == null)
            {
                var errorResponse = new {
                    success = false,
                    message = "AuctionNotFound"
                };
                return NotFound(errorResponse);
            }
            var user = _context.Users.FirstOrDefault(x => x.UserId == auction.SellerId);
            if (user == null)
            {
                var errorResponse = new {
                    success = false,
                    message = "UserNotFound"
                };
                return NotFound(errorResponse);
            }
            // auction.Seller = user;
            var successResponse = new {
                success = true,
                message = "ok",
                data = auction
            };
            return Ok(successResponse);
        }
        
        // Auction Create API
        [Route("create")]
        [HttpPost]
        public IActionResult CreateAuction([FromBody] CreateAuctionDto createAuction)
        {
            var user =  _context.Users.FirstOrDefault(x => x.UserId == createAuction.SellerId);
            if (user == null)
            {
                var errorResponse = new {
                    success = false,
                    message = "UserNotFound"
                };
                return NotFound(errorResponse);
            }
            var auctionModel = new Auction {
                Title = createAuction.Title,
                Description = createAuction.Description,
                AuctionImage = createAuction.AuctionImage,
                AuctionCategory = createAuction.AuctionCategory,
                SellerId = user.UserId,
                StartTime = createAuction.StartTime,
                EndTime = createAuction.EndTime,
                Status = "pending",
            };
            
            _context.Auctions.Add(auctionModel);
            _context.SaveChanges();

            return Ok(new
            { 
            success = true, 
            message = "ok", 
            data = auctionModel.ToAuctionsDtoGet()
            });
        }
        
    }
}
