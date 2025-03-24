using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Entities
{
   public class Product:BaseEntity
    {
        [MaxLength(100)]
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public int? BrandId { get; set; }
        [ForeignKey("BrandId")]
        public virtual ProductBrand? Brand { get; set; }
        public int? CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual ProductCategory? Category { get; set; }
    }
}
