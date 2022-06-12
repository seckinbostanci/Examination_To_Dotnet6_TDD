using IncirAgaci.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace IncirAgaci.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    //private readonly ILogger<UsersController> _logger;

    //public UsersController(ILogger<UsersController> logger)
    //{
    //    _logger = logger;
    //}

    [HttpGet(Name = "GetUsers")]
    public async Task<IActionResult> Get()
    {
        var users = await _userService.GetAllUsers();

        if (users.Any())
        {
            return Ok(users);
        }
        
        return NotFound();
    }
}
