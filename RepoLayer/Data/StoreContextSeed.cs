using CoreLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RepoLayer.Data
{
    public class StoreContextSeed
    {
        public async static Task SeedAsync(AppDbContext _context) 
        {
            //if (_context.T.Count() == 0)
            //{
            //    var BrandsData = File.ReadAllText("../TalabatRepo/Data/DataSeed/brands.json");
            //    if (BrandsData?.Count() > 0)
            //    {
            //        var brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);
            //        brands = brands.Select(b => new ProductBrand() { Name = b.Name }).ToList();
            //        foreach (var brand in brands)
            //        {
            //            await _context.Brands.AddAsync(brand);
            //        }
            //        await _context.SaveChangesAsync();
            //    }
            //}
            
        }
    }
}
