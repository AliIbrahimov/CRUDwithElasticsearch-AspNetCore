using Elasticsearch.API.DTOs.ProductFeature;
using Nest;

namespace Elasticsearch.API.DTOs.Product
{
    public record ProductDTO(string Id, string Name,decimal Price ,int Stock ,ProductFeatureDTO? Feature)
    {
    }
}
