using System;
using api.Dtos.Users;
using api.Models;

namespace api.Mappers;

public static class UserMappers
{

    public static UserDto ToUserDtoGet(this User userModel)
    {
        return new UserDto
        {
            Id = userModel.UserId,
            FirstName = userModel.FirstName,
            LastName = userModel.LastName,
            Email = userModel.Email,
            Username = userModel.Username,
            Gender = userModel.Gender,
            Mobile = userModel.Mobile,
            Address = userModel.Address,
            CreatedAt = userModel.CreatedAt,
            UpdatedAt = userModel.UpdatedAt,
            Token = userModel.Token

        };

    }
}
