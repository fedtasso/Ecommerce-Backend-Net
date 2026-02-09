using Ecommerce.Application.DTOs.User;
using Ecommerce.Application.DTOs.Users;
using Ecommerce.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Api.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    // POST /api/users/register
    [HttpPost("register")]
    public async Task<ActionResult<UserResponse>> CreateUser(
        [FromBody] UserCreateRequest request)
    {
        var response = await _userService.CreateUserAsync(request);
        return Ok(response);
    }

    // POST /api/users/login
    [HttpPost("login")]
    public async Task<ActionResult<UserResponse>> Login(
        [FromBody] UserLoginRequest request)
    {
        var response = await _userService.LoginAsync(
            request.Email,
            request.Password
        );

        return Ok(response);
    }

    // GET /api/users/search?email=...
    [HttpGet("search")]
    public async Task<ActionResult<UserResponse>> GetUserByEmail(
        [FromQuery][Required][EmailAddress] string email)
    {
        var response = await _userService.GetUserByEmailAsync(email);
        return Ok(response);
    }

    // GET /api/users
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserResponse>>> GetAllUsers()
    {
        var response = await _userService.GetAllUsersAsync();
        return Ok(response);
    }

    // GET /api/users/{id}
    [HttpGet("{id:long}")]
    public async Task<ActionResult<UserResponse>> GetUserById(
        [FromRoute] long id)
    {
        var response = await _userService.GetUserByIdAsync(id);
        return Ok(response);
    }

    // PUT /api/users/{id}
    [HttpPut("{id:long}")]
    public async Task<ActionResult<UserResponse>> UpdateUser(
        [FromRoute] long id,
        [FromBody] UserUpdateRequest request)
    {
        var response = await _userService.UpdateUserAsync(id, request);
        return Ok(response);
    }

    // PATCH /api/users/{id}/password
    [HttpPatch("{id:long}/password")]
    public async Task<IActionResult> UpdatePassword(
        [FromRoute] long id,
        [FromBody] UserPasswordUpdateRequest request)
    {
        await _userService.UpdatePasswordAsync(id, request);
        return NoContent();
    }

    // DELETE /api/users/{id}
    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteUser(
        [FromRoute] long id)
    {
        await _userService.DeleteUserAsync(id);
        return NoContent();
    }
}
