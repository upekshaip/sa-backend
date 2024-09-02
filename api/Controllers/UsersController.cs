using api.Data;
using api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using api.Mappers;
using api.Dtos.Users;
using System.Reflection.Metadata.Ecma335;

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
            var users =  _context.Users.ToList().Select(s => s.ToUserDtoGet());
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
                    message = "UserNotFound"
                };
                return NotFound(errorResponse);
            }
            var successResponse = new {
                success = true,
                message = "ok",
                data = user
            };
            return Ok(successResponse);
        }

        [Route("create")]
        [HttpPost]
        public IActionResult Create([FromBody] CreateUserDto userDto) 
        {
            var userModel = userDto.ToUserFromCreateDto();

            _context.Users.Add(userModel);
            _context.SaveChanges();

            return Ok(new
            { 
            success = true, 
            message = "ok", 
            data = userModel.ToUserDtoGet()
            });
            // return CreatedAtAction(nameof(GetById), new {id = userModel.UserId}, userModel);
        }
    }
}
            
