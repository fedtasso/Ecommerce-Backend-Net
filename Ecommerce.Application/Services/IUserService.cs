using Ecommerce.Application.DTOs.User;
using Ecommerce.Application.DTOs.Users;

namespace Ecommerce.Application.Services;

public interface IUserService
{
    Task<UserResponse> CreateUserAsync(UserCreateRequest request);

    Task<IEnumerable<UserResponse>> GetAllUsersAsync();

    Task<UserResponse> GetUserByIdAsync(long id);

    Task<UserResponse> GetUserByEmailAsync(string email);

    Task<UserResponse> UpdateUserAsync(long id, UserUpdateRequest request);

    Task DeleteUserAsync(long id);

    Task UpdatePasswordAsync(long id, UserPasswordUpdateRequest request);

    Task<UserResponse> LoginAsync(string email, string password);
}
