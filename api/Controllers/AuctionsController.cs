using api.Data;
using api.Dtos.AuctionItems;
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
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AuctionsController(APIContext context, IWebHostEnvironment webHostEnvironment)
        {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
        }
        

        [HttpGet]
        public IActionResult GetAll()
        {
            var auctions =  _context.Auctions.ToList().Select(s => s.ToAuctionsDtoGet());
            return Ok(auctions);
        }

        [Route("my")]
        [HttpPost]
        public IActionResult GetMyAuctions([FromBody] GetMyAuctionsDto request)
        {
            var auctions =  _context.Auctions.Where(x => x.SellerId == request.Id).ToList().Select(s => s.ToAuctionsDtoGet());
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
            var seller = _context.Users.FirstOrDefault(x => x.UserId == auction.SellerId);
            if (seller == null)
            {
                var errorResponse = new {
                    success = false,
                    message = "UserNotFound"
                };
                return NotFound(errorResponse);
            }
            var auctionItems = _context.AuctionItems.Where(x => x.AuctionId == auction.AuctionId).ToList();
            var bids = _context.Bids.Where(x => x.AuctionId == auction.AuctionId).ToList();
            auction.Bids = bids;
            auction.AuctionItems = auctionItems;
            var successResponse = new {
                success = true,
                message = "ok",
                data = new {
                    auction = auction,
                    seller = seller.ToUserDtoGet()
                },
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
                StartingBid = createAuction.StartingBid,
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

        [Route("additem")]
        [HttpPost]
        public IActionResult AddAuctionItem([FromBody] CreateAuctionItemDto createAuctionItemDto)
        {
            var auction = _context.Auctions.FirstOrDefault(x => x.AuctionId == createAuctionItemDto.AuctionId);
            if (auction == null)
            {
                var errorResponse = new {
                    success = false,
                    message = "AuctionNotFound"
                };
                return NotFound(errorResponse);
            }
            var auctionItem = new AuctionItem {
                AuctionId = auction.AuctionId,
                ItemName = createAuctionItemDto.ItemName,
                ItemDescription = createAuctionItemDto.ItemDescription,
                ItemImage = createAuctionItemDto.ItemImage,
                ItemCategory = createAuctionItemDto.ItemCategory,
            };
            _context.AuctionItems.Add(auctionItem);
            _context.SaveChanges();
            return Ok(new
            {
                success = true,
                message = "ok",
                data = auctionItem
            });
        }

        [HttpPost("upload-image")]
        public IActionResult UploadAuctionImage(IFormFile file)
        {
            // Check extension
            List<string> validExtensions = new List<string> { ".jpg", ".jpeg", ".png" };
            string ext = Path.GetExtension(file.FileName);
            if (!validExtensions.Contains(ext)) {
                return BadRequest(new {
                    success = false,
                    message = "Invalid file type"
                });
            }
            // Check file size
            long size = file.Length;
            if (size > 5 * 1024 * 1024) {
                return BadRequest(new {
                    success = false,
                    message = "File size too large"
                });
            }
            string fileName = Guid.NewGuid().ToString() + ext;
            string path = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
            using FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create);
            file.CopyTo(stream);

            return Ok(new {
                success = true,
                message = "ok",
                data = new {
                    fileName = fileName
                }
            });
        }

    }
}
