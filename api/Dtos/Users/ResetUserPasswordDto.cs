using System;

namespace api.Dtos.Users;

public class ResetUserPasswordDto
{
    public int Id { get; set; }
    public string NewPassword { get; set; } = string.Empty;
    public string OldPassword { get; set; } = string.Empty;
}
