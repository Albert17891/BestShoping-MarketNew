﻿using Mapster;
using MarketPlace.Core.Entities;
using MarketPlace.Core.Interfaces.Account;
using MarketPlace.Web.ApiModels.Request;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace MarketPlace.Web.Controllers;

[Route("[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IUserAuthentication _userAuthentication;
    private readonly SignInManager<AppUser> _signInManager;


    public AccountController(IUserAuthentication userAuthentication, SignInManager<AppUser> signInManager)
    {
        _userAuthentication = userAuthentication;
        _signInManager = signInManager;

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

    //[Route("login-oauth")]
    //[HttpGet]
    //public  IActionResult LoginByGithub()
    //{
    //    return Challenge(
    //        new AuthenticationProperties()
    //        {
    //            RedirectUri = "https://localhost:7246/Admin/GetUsers"
    //        },
    //        authenticationSchemes:new List<string>() { "github" }[0] ) ;
    //}

    [Route("register")]
    [HttpPost]
    public async Task<IActionResult> Register(RegisterRequest registerRequest)
    {
        if (registerRequest is null)
            return BadRequest();

        var result = await _userAuthentication.RegisterAsync(registerRequest.Adapt<Register>());

        return Ok(JsonSerializer.Serialize(result));
    }

    [Route("logout")]
    [HttpGet]
    public async Task<IActionResult> LogOut()
    {
        await HttpContext.SignOutAsync();

        if(User.Identity.IsAuthenticated)
            return Ok();

        return Ok("Failed");
    }
}
