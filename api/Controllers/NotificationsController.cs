using api.Data;
using Microsoft.AspNetCore.Mvc;
using api.Mappers;
using api.Dtos.Users;
using api.Dtos.Notifications;


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
            var notifications = _context.Notifications.Where(x => x.UserId == id).ToList();
            var successResponse = new {
                success = true,
                message = "ok",
                data = notifications
            };
            return Ok(successResponse);
        }

        [HttpPost]
        [Route("check")]
        public IActionResult Check([FromBody] CheckNotificationsDto notificationsDto) {

            var user = _context.Users.FirstOrDefault(x => x.UserId == notificationsDto.UserId);
            if (user == null)
            {
                var errorResponse = new {
                    success = false,
                    message = "UserNotFound"
                };
                return NotFound(errorResponse);
            }
            var notification = _context.Notifications
                .Where(x => x.UserId == notificationsDto.UserId && x.IsRead == false)
                .Count();

            return Ok(new {
                success = true,
                message = "ok",
                data = notification
            });
        }


        [HttpPost]
        [Route("read")]
        public IActionResult Read([FromBody] ReadNotifications notificationsDto) 
        {
            var notification = _context.Notifications
                .FirstOrDefault(x => x.Id == notificationsDto.Id && x.UserId == notificationsDto.UserId);
            
            if (notification == null)
            {
                var errorResponse = new
                {
                    success = false,
                    message = "NotificationNotFound"
                };
                return BadRequest(errorResponse);
            }

            // Change the IsRead property of the notification to true
            notification.IsRead = true;
            _context.SaveChanges();

            return Ok(new
            { 
                success = true, 
                message = "ok"
            });
        }


    }

}