using Elasticsearch.API.DTOs.Product;
using Elasticsearch.API.DTOs.ProductFeature;
using Nest;

namespace Elasticsearch.API.Models;

public class Product
{


    [PropertyName("_id")]
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public DateTime Created{ get; set; }
        public DateTime Updated{ get; set;}
        public ProductFeature? Feature { get; set; }

    public ProductDTO CreateDTO()
    {
        if (Feature == null) 
            return new ProductDTO(Id, Name, Price, Stock,null);

        return new ProductDTO(Id, Name, Price, Stock,new ProductFeatureDTO(
            Feature.Width, Feature.Height, Feature.Color.ToString()));
    }
    
}
