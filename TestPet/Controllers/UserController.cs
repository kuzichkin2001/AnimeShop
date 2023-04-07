using AnimeShop.Bll.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TestPet.Views;

namespace TestPet.Controllers;

[Route("api/[controller]")]
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
    [Route("register")]
    public async Task<IActionResult> Register(UserCredentialsView userCredentials)
    {
        var user = _mapper.Map<AnimeShop.Common.User>(userCredentials);

        await _userLogic.RegisterUserAsync(user);

        return Ok();
    }
}