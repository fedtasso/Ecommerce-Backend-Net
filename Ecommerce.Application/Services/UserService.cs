using Ecommerce.Application.DTOs.User;
using Ecommerce.Application.DTOs.Users;
using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Mappers;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly UserMapper _userMapper;
    private readonly IPasswordHasher _passwordHasher;

    public UserService(
        IUserRepository userRepository,
        UserMapper userMapper,
        IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _userMapper = userMapper;
        _passwordHasher = passwordHasher;
    }

    public async Task<UserResponse> CreateUserAsync(UserCreateRequest request)
    {
        if (await _userRepository.ExistsByEmailAsync(request.Email))
            throw new Exception($"Email already exists: {request.Email}");

        var user = _userMapper.ToEntity(request);

        user.SetPassword(
            _passwordHasher.Hash(request.Password)
        );

        var savedUser = await _userRepository.AddAsync(user);

        return _userMapper.ToResponse(savedUser);
    }

    public async Task<IEnumerable<UserResponse>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllAsync();

        return users.Select(_userMapper.ToResponse);
    }

    public async Task<UserResponse> GetUserByEmailAsync(string email)
    {
        var user = await _userRepository.GetByEmailAsync(email)
                   ?? throw new Exception("User not found");

        return _userMapper.ToResponse(user);
    }

    public async Task<UserResponse> GetUserByIdAsync(long id)
    {
        var user = await _userRepository.GetByIdAsync(id)
                   ?? throw new Exception("User not found");

        return _userMapper.ToResponse(user);
    }

    public async Task<UserResponse> UpdateUserAsync(long id, UserUpdateRequest request)
    {
        var dbUser = await _userRepository.GetByIdAsync(id)
                     ?? throw new Exception("User not found");

        if (!string.IsNullOrWhiteSpace(request.Email) &&
            request.Email != dbUser.Email &&
            await _userRepository.ExistsByEmailAsync(request.Email))
        {
            throw new Exception($"Email already exists: {request.Email}");
        }

        if (!string.IsNullOrWhiteSpace(request.Name))
            dbUser.SetName(request.Name);

        if (request.Avatar != null)
            dbUser.SetAvatar(request.Avatar);

        if (request.Phone != null)
            dbUser.SetPhone(request.Phone);

        if (!string.IsNullOrWhiteSpace(request.Email))
            dbUser.SetEmail(request.Email);

        var updatedUser = await _userRepository.UpdateAsync(dbUser);

        return _userMapper.ToResponse(updatedUser);
    }

    public async Task DeleteUserAsync(long id)
    {
        var user = await _userRepository.GetByIdAsync(id)
                   ?? throw new Exception("User not found");

        await _userRepository.DeleteAsync(user);
    }

    public async Task UpdatePasswordAsync(long id, UserPasswordUpdateRequest request)
    {
        var user = await _userRepository.GetByIdAsync(id)
                   ?? throw new Exception("User not found");

        if (!_passwordHasher.Verify(request.CurrentPassword, user.Password))
            throw new Exception("Invalid credentials");

        if (_passwordHasher.Verify(request.NewPassword, user.Password))
            throw new Exception("Same password");

        user.SetPassword(
            _passwordHasher.Hash(request.NewPassword)
        );

        await _userRepository.UpdateAsync(user);
    }

    public async Task<UserResponse> LoginAsync(string email, string password)
    {
        var user = await _userRepository.GetByEmailAsync(email);

        if (user == null || !_passwordHasher.Verify(password, user.Password))
            throw new Exception("Invalid credentials");

        return _userMapper.ToResponse(user);
    }
}
