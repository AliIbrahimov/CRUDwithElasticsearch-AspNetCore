using Elasticsearch.API.DTOs.ProductFeature;

namespace Elasticsearch.API.DTOs.Product;

public record ProductUpdateDTO(string id,string Name, decimal Price, int Stock, ProductFeatureDTO Feature)
{

}
