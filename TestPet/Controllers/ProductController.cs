using System.Runtime.InteropServices;
using AnimeShop.Bll.Interfaces;
using AnimeShop.Common;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TestPet.Views;

namespace TestPet.Controllers;

[Route("api/")]
[ApiController]
public class ProductController : Controller
{
    private readonly IProductLogic _productLogic;
    private readonly IMapper _mapper;

    public ProductController(IProductLogic productLogic, IMapper mapper)
    {
        _productLogic = productLogic;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("product/receive")]
    public async Task<IActionResult> GetProductById(int productId)
    {
        try
        {
            var product = await _productLogic.GetProductByIdAsync(productId);

            return Ok(product);
        }
        catch (Exception exp)
        {
            return NotFound($"There's no product with such id.\n" +
                            $"{exp.GetType()}: {exp.Message}");
        }
    }

    [HttpGet]
    [Route("product/all")]
    public IActionResult GetAllProducts()
    {
        var products = _productLogic.GetAllProducts().ToList();

        return Ok(products);
    }

    [HttpPost]
    [Route("product/create")]
    public async Task<IActionResult> CreateProduct(ProductView productView)
    {
        try
        {
            var product = _mapper.Map<Product>(productView);
            await _productLogic.CreateProductAsync(product);

            return Ok(new { Message = "Success", Result = true });
        }
        catch (Exception exp)
        {
            return BadRequest($"Some error.\n" +
                              $"{exp.GetType()}: {exp.Message}");
        }
    }

    [HttpPost]
    [Route("product/create/multiple")]
    public async Task<IActionResult> CreateProduct(IEnumerable<ProductView> productViews)
    {
        try
        {
            foreach (var view in productViews)
            {
                var product = _mapper.Map<Product>(view);
                await _productLogic.CreateProductAsync(product);
            }

            return Ok(new { Message = "Success", Result = true });
        }
        catch (Exception exp)
        {
            return BadRequest($"Some error.\n" +
                              $"{exp.GetType()}: {exp.Message}");
        }
    }
}