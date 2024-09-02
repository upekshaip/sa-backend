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

        // Get All users API
        [HttpGet]
        public IActionResult GetAll()
        {
            var users =  _context.Users.ToList().Select(s => s.ToUserDtoGet());
            return Ok(users);
        }

        // User get bu id API
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

        // User Create API
        [Route("signup")]
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
        }

        // Update Users API (CreateUserDto same as above)
        [HttpPut]
        [Route("edit")]
        public IActionResult Update([FromBody] UpdateUserDto updateUserDto)
        {
            var userModel = _context.Users.FirstOrDefault(x => x.UserId == updateUserDto.Id);
            if (userModel == null)
            {
                var errorResponse = new {
                    success = false,
                    message = "UserNotFound"
                };
                return NotFound(errorResponse);
            }
            userModel.FirstName = updateUserDto.FirstName;
            userModel.LastName = updateUserDto.LastName;
            userModel.Username = updateUserDto.Username;
            userModel.Address = updateUserDto.Address;
            userModel.Gender = updateUserDto.Gender;
            userModel.Email = updateUserDto.Email;
            userModel.Mobile = updateUserDto.Mobile;
            userModel.UpdatedAt = DateTime.Now;

            
            _context.SaveChanges();

            return Ok(new {
                success = true, 
                message = "ok", 
                data = userModel.ToUserDtoGet()
            });
        }
        
        [HttpPost]
        [Route("resetPassword")]
        public IActionResult UpdatePassword([FromBody] ResetUserPasswordDto resetUserDto)
        {
            var userModel = _context.Users.FirstOrDefault(x => x.UserId == resetUserDto.Id);
            if (userModel == null)
            {
                var errorResponse = new {
                    success = false,
                    message = "UserNotFound"
                };
                return NotFound(errorResponse);
            }
            if (userModel.Password == resetUserDto.OldPassword)
            {
                userModel.Password = resetUserDto.NewPassword;
                userModel.UpdatedAt = DateTime.Now;
                _context.SaveChanges();
                return Ok(new {
                    success = true, 
                    message = "ok", 
                    data = userModel.ToUserDtoGet()
                });
            } else {
                var errorResponse = new {
                    success = false,
                    message = "OldPasswordNotMatch"
                };
                return BadRequest(errorResponse);
            }
        }

        [HttpPost]
        [Route("login")]
        public IActionResult LoginUser([FromBody] UserLoginDto userLoginDto)
        {
            var userModel = _context.Users.FirstOrDefault(x => (x.Username == userLoginDto.Username || x.Email == userLoginDto.Username) && x.Password == userLoginDto.Password);
            if (userModel == null)
            {
                var errorResponse = new {
                    success = false,
                    message = "InvalidUsernameOrPassword"
                };
                return NotFound(errorResponse);
            } else {
                var successResponse = new {
                    success = true,
                    message = "LoginSuccess",
                    data = userModel.ToUserDtoGet()
                };
                return BadRequest(successResponse);
            }
        }
        
    }
}
