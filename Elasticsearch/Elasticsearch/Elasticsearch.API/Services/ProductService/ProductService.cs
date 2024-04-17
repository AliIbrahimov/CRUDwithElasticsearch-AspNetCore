using Elasticsearch.API.DTOs;
using Elasticsearch.API.DTOs.Product;
using Elasticsearch.API.Repository;
using Elasticsearch.API.DTOs.ProductFeature;
using System.Net;
namespace Elasticsearch.API.Services.ProductService;

public class ProductService
{
    private readonly ProductRepository _repository;

    public ProductService(ProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResponseDTO<ProductDTO>> SaveAsync(ProductCreateDTO request)
    {
        var response = await _repository.SaveAsync(request.CreateProduct());

        if (response is null)
            return ResponseDTO<ProductDTO>.Fail(new List<string> { "save error!" }, System.Net.HttpStatusCode.NotAcceptable);

        return ResponseDTO<ProductDTO>.Success(response.CreateDTO(), System.Net.HttpStatusCode.Created);

    }

    public async Task<ResponseDTO<List<ProductDTO>>> GetAllAsync()
    {
        var productListDTO = new List<ProductDTO>();    
        var products = await _repository.GetAllAsync();

      
        foreach (var product in products)
        {
            if (product.Feature is null)
                productListDTO.Add(new ProductDTO(product.Id, product.Name, product.Price, product.Stock, null));
            else
                productListDTO.Add(new ProductDTO(product.Id, product.Name, product.Price, product.Stock, new ProductFeatureDTO(product.Feature.Width, product.Feature.Height, product.Feature.Color.ToString())));
        }
        
        
        return ResponseDTO<List<ProductDTO>>.Success(productListDTO, System.Net.HttpStatusCode.OK);
    }
    public async Task<ResponseDTO<ProductDTO>> GetByIdAsync(string id)
    {
        var hasProduct = await _repository.GetById(id);

        if (hasProduct is null)
            return ResponseDTO<ProductDTO>.Fail("Product is not find", HttpStatusCode.NotFound);

        var productDTO = hasProduct.CreateDTO();

        return ResponseDTO<ProductDTO>.Success(productDTO, HttpStatusCode.OK);

    }
    public async Task<ResponseDTO<bool>> UpdateAsync(ProductUpdateDTO productUpdate)
    {
        var isSuccess =await _repository.UpdateAsync(productUpdate);

        if (!isSuccess)
        {
            return ResponseDTO<bool>.Fail(new List<string> { "product is not updated!"}, HttpStatusCode.NotModified);

        }

        return ResponseDTO<bool>.Success(true, HttpStatusCode.NoContent);
    }
    public async Task<ResponseDTO<bool>> DeleteAsync(string id)
    {
        var isSuccess = await _repository.DeleteAsync(id);

        if (!isSuccess)
            return ResponseDTO<bool>.Fail(new List<string> { "product is not deleted!" }, HttpStatusCode.NotModified);

        

        return ResponseDTO<bool>.Success(true, HttpStatusCode.NoContent);
    }

}

