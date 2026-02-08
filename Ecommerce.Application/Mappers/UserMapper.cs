using Ecommerce.Application.DTOs.User;
using Ecommerce.Application.DTOs.Users;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.Mappers;
public class UserMapper
{
    public User ToEntity(UserCreateRequest request)
    {
        var user = new User(
            name: request.Name,
            email: request.Email,
            password: request.Password
        );

        if (!string.IsNullOrWhiteSpace(request.Avatar))
            user.SetAvatar(request.Avatar);

        if (!string.IsNullOrWhiteSpace(request.Phone))
            user.SetPhone(request.Phone);

        return user;
    }

    public UserResponse ToResponse(User user)
    {
        return new UserResponse
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Active = user.Active,
            Avatar = user.Avatar,
            Phone = user.Phone,
            Role = user.Role,
            CreatedAt = user.CreatedAt,
            LastLogin = user.LastLogin
        };
    }
}
