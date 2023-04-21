using AnimeShop.Bll.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TestPet.Views;

namespace TestPet.Controllers;

[Route("api/")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserLogic _userLogic;
    private readonly IMapper _mapper;

    
    public UserController(IUserLogic userLogic, IMapper mapper)
    {
        _userLogic = userLogic;
        _mapper = mapper;
    }
    
    [HttpPost]
    [Route("user/register")]
    public async Task<IActionResult> Register(UserCredentialsView userCredentials)
    {
        var user = _mapper.Map<AnimeShop.Common.User>(userCredentials);

        await _userLogic.RegisterUserAsync(user);

        return Ok();
    }

    [HttpGet]
    [Route("user/receive")]
    public async Task<IActionResult> GetUser(string login, string password)
    {
        try
        {
            var result = await _userLogic.GetUserAsync(login, password);

            return Ok(result);
        }
        catch (Exception exp)
        {
            return BadRequest($"{exp.GetType()}: {exp.Message}");
        }
    }

    [HttpGet]
    [Route("user/check_existence")]
    public async Task<IActionResult> CheckUserExistence(string login, string password)
    {
        var result = await _userLogic.CheckUserCredentialsAsync(login, password);

        if (result.HasValue && result.Value)
        {
            return Ok();
        }

        return BadRequest($"There's no user with such credentials.");
    }

    [HttpPut]
    [Route("user/change")]
    public async Task<IActionResult> ChangeUserPersonalInfo(UserCredentialsView userCreds)
    {
        try
        {
            var user = _mapper.Map<AnimeShop.Common.User>(userCreds);
            var result = await _userLogic.ChangePersonalInfoAsync(user);

            return Ok(result);
        }
        catch (Exception exp)
        {
            return BadRequest($"There's no user with such credentials.\n" +
                              $"{exp.GetType()}: {exp.Message}");
        }
    }
}