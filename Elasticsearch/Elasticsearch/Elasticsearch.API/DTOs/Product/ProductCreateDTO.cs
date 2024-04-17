using Elasticsearch.API.DTOs.ProductFeature;
using Elasticsearch.API.Enums;
using Elasticsearch.API.Models;

namespace Elasticsearch.API.DTOs.Product;

public record ProductCreateDTO(string Name, decimal Price, int Stock, ProductFeatureDTO Feature )
{
    public Models.Product CreateProduct()
    {
        return new Models.Product
        {
            Name = Name,
            Price = Price,
            Stock = Stock,
            Feature = new Models.ProductFeature()
            {
                Width = Feature.Width,
                Height = Feature.Height,
                Color = (EColor)int.Parse(Feature.Color)
            }
        };
    }
}
