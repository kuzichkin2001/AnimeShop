using System.Runtime.CompilerServices;
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

    [HttpPut]
    [Route("product/update")]
    public async Task<IActionResult> UpdateProduct(ProductView productView)
    {
        try
        {
            var product = _mapper.Map<Product>(productView);
            await _productLogic.UpdateProductAsync(product);

            return Ok(new { Message = "Success", Result = true });
        }
        catch (Exception exp)
        {
            return NotFound($"There's no such product.\n" +
                            $"{exp.GetType()}: {exp.Message}");
        }
    }

    [HttpDelete]
    [Route("product/delete")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        try
        {
            await _productLogic.DeleteProductAsync(id);

            return Ok(new { Message = "Success", Result = true });
        }
        catch (Exception exp)
        {
            return NotFound($"There's no product with such id.\n" +
                            $"{exp.GetType()}: {exp.Message}");
        }
    }

    [HttpDelete]
    [Route("product/delete/multiple")]
    public async Task<IActionResult> DeleteProducts(IEnumerable<int> ids)
    {
        int incorrectId = -1;
        try
        {
            foreach (var id in ids)
            {
                incorrectId = id;
                await _productLogic.DeleteProductAsync(id);
            }

            return Ok(new { Message = "Success", Result = true });
        }
        catch (Exception exp)
        {
            return NotFound($"There's no product with such id: {incorrectId}." +
                            $"{exp.GetType()}: {exp.Message}");
        }
    }
}