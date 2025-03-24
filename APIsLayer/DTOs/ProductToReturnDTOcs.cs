using CoreLayer.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APIsLayer.DTOs
{
    public class ProductToReturnDTOcs
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        
        public decimal Price { get; set; }
        public int? BrandId { get; set; }
        
        public String? Brand { get; set; }
        public int? CategoryId { get; set; }
        
        public String? Category { get; set; }
    }
}
