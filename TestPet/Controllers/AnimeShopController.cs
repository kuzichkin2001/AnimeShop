using AnimeShop.Bll.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TestPet.Views;

namespace TestPet.Controllers;

[Route("api/")]
[ApiController]
public class AnimeShopController : Controller
{
    private readonly IMapper _mapper;
    private readonly IAnimeShopLogic _animeShopLogic;

    public AnimeShopController(IMapper mapper, IAnimeShopLogic animeShopLogic)
    {
        _mapper = mapper;
        _animeShopLogic = animeShopLogic;
    }

    [HttpGet]
    [Route("animeshop/receive")]
    public async Task<IActionResult> GetAnimeShop(int animeshopId)
    {
        try
        {
            var animeshop = await _animeShopLogic.GetAnimeShopByIdAsync(animeshopId);

            return Ok(animeshop);
        }
        catch (Exception exp)
        {
            return NotFound($"There's no animeshop with such id.\n" +
                            $"{exp.GetType()}: {exp.Message}");
        }
    }

    [HttpGet]
    [Route("animeshop/receive/all")]
    public IActionResult GetAllAnimeShops()
    {
        var animeshops = _animeShopLogic.GetAllAnimeShops();

        return Ok(animeshops);
    }

    [HttpPost]
    [Route("animeshop/create")]
    public async Task<IActionResult> CreateAnimeShops(AnimeShopView shopView)
    {
        try
        {
            var animeshop = _mapper.Map<AnimeShop.Common.AnimeShop>(shopView);
            await _animeShopLogic.CreateAnimeShopAsync(animeshop);

            return Ok(new { Message = "Success", Result = true });
        }
        catch (Exception exp)
        {
            return BadRequest($"Some error on creating anime shop.\n" +
                              $"{exp.GetType()}: {exp.Message}");
        }
    }

    [HttpPut]
    [Route("animeshop/update")]
    public async Task<IActionResult> UpdateAnimeShop(AnimeShopView shopView)
    {
        try
        {
            var animeshop = _mapper.Map<AnimeShop.Common.AnimeShop>(shopView);
            await _animeShopLogic.UpdateAnimeShopAsync(animeshop);

            return Ok(new { Message = "Success", Result = true });
        }
        catch (Exception exp)
        {
            return BadRequest($"There's no such anime shop.\n" +
                              $"{exp.GetType()}: {exp.Message}");
        }
    }

    [HttpDelete]
    [Route("animeshop/delete")]
    public async Task<IActionResult> DeleteAnimeShop(int id)
    {
        var result = await _animeShopLogic.RemoveAnimeShopAsync(id);

        if (result)
        {
            return Ok(new { Message = "Success", Result = true });
        }

        return NotFound($"There's no anime shop with id.\n");
    }
}