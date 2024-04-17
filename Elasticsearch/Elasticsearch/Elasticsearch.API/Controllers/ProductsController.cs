using Elasticsearch.API.DTOs.Product;
using Elasticsearch.API.Services.ProductService;
using Microsoft.AspNetCore.Mvc;

namespace Elasticsearch.API.Controllers;

public class ProductsController : BaseController
{
    private readonly ProductService _productService;

    public ProductsController(ProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    public async Task<IActionResult> Save(ProductCreateDTO request)
    {
        return CreateActionResult((await _productService.SaveAsync(request)));
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProductUpdateDTO request)
    {
        return CreateActionResult((await _productService.UpdateAsync(request)));
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return CreateActionResult((await _productService.GetAllAsync()));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        return CreateActionResult(await _productService.GetByIdAsync(id));
    }
    [HttpDelete("{id}")]

    public async Task<IActionResult> Delete(string id)
    {
        return CreateActionResult(await _productService.DeleteAsync(id));

    }

}
