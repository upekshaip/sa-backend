using api.Data;
using api.Mappers;
using Microsoft.AspNetCore.Http;
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
            var auction = _context.Auctions.Find(id);
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
                data = auction
            };
            return Ok(successResponse);
        }
        
    }
}
