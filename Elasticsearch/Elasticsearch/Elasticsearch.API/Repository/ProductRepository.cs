using Elasticsearch.API.DTOs.Product;
using Elasticsearch.API.Models;
using Nest;
using System.Collections.Immutable;

namespace Elasticsearch.API.Repository;

public class ProductRepository
{
    private readonly ElasticClient _client;
    private const string indexName = "products";

    public ProductRepository(ElasticClient client)
    {
        _client = client;
    }
    public async Task<Product> SaveAsync(Product NewProduct)
    {
        NewProduct.Created = DateTime.Now;

        var response = await _client.IndexAsync(NewProduct, x => x.Index(indexName).Id(Guid.NewGuid().ToString()));

        if (!response.IsValid) return null;
        NewProduct.Id = response.Id;
        return NewProduct;
    }

    public async Task<IImmutableList<Product>> GetAllAsync()
    {
        var result = await _client.SearchAsync<Product>(s => s.Index(indexName).Query(q => q.MatchAll()));

        foreach (var hit in result.Hits) hit.Source.Id = hit.Id;

        return result.Documents.ToImmutableList();
    }

    public async Task<Product?> GetById(string id)
    {
        var response = await _client.GetAsync<Product>(id, x => x.Index(indexName));

        if (!response.IsValid) return null;

        response.Source.Id = response.Id;
        return response.Source;
    }
    public async Task<bool> UpdateAsync(ProductUpdateDTO productUpdate)
    {

        var response = await _client.UpdateAsync<Product, ProductUpdateDTO>(productUpdate.id, x => x.Index(indexName).Doc(productUpdate));


        return response.IsValid;


    }
    public async Task<bool> DeleteAsync(string id)
    {
        var response = await _client.DeleteAsync<Product>(id,x=>x.Index(indexName));

        return response.IsValid;
    }

}

