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

    public static User ToUserFromCreateDto(this CreateUserDto userDto) {
        return new User {
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            Username = userDto.Username,
            Email = userDto.Email,
            Gender = userDto.Gender,
            Password = userDto.Password,
            Mobile = userDto.Mobile,
            Address = userDto.Address,
            Token = userDto.Username
        };
    }
}
