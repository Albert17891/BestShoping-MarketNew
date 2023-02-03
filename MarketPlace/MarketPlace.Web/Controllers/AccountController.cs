using Mapster;
using MarketPlace.Core.Entities;
using MarketPlace.Core.Interfaces.Account;
using MarketPlace.Web.ApiModels.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace MarketPlace.Web.Controllers;
[Route("[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IUserAuthentication _userAuthentication;

    public AccountController(IUserAuthentication userAuthentication)
    {
        _userAuthentication = userAuthentication;
    }

    [Route("login")]
    [HttpPost]
    public async Task<IActionResult> Login(LoginRequest loginRequest)
    {
        if (loginRequest is null)
            return BadRequest();

        var response = await _userAuthentication.AuthenticateAsync(loginRequest.Adapt<Login>());

        if (response is null)
            return Unauthorized();

        return Ok(response);
    }

    [Route("register")]
    [HttpPost]
    public async Task<IActionResult> Register(RegisterRequest registerRequest)
    {
        if (registerRequest is null)
            return BadRequest();

        var result = await _userAuthentication.RegisterAsync(registerRequest.Adapt<Register>());

        return Ok(JsonSerializer.Serialize(result));
    }
}
