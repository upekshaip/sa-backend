using api.Data;
using api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly APIContext _context;
        public UsersController(APIContext context)
        {
        _context = context;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var users =  _context.Users.ToList();
            return Ok(users);
        }

        
        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                var errorResponse = new {
                    success = false,
                    message = "User not found"
                };
                return NotFound(errorResponse);
            }
            var successResponse = new {
                success = true,
                message = "User found",
                data = user
            };
            return Ok(successResponse);
        }
    }
}
            
