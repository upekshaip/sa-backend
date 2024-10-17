using api.Data;
using Microsoft.AspNetCore.Mvc;
using api.Mappers;
using api.Dtos.Users;


namespace api.Controllers {
    [Route("api/notifications")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly APIContext _context;
        public NotificationsController(APIContext context)
        {
        _context = context;
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
            var notifications = _context.Notifications.Where(x => x.UserId == id).ToList();
            var successResponse = new {
                success = true,
                message = "ok",
                data = notifications
            };
            return Ok(successResponse);
        }

    }

}