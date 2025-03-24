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
            if (_context.Brands.Count() == 0)
            {
                var BrandsData = File.ReadAllText("../RepoLayer/Data/DataSeed/brands.json");
                if (BrandsData?.Count() > 0)
                {
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);
                    brands = brands.Select(b => new ProductBrand() { Name = b.Name }).ToList();
                    foreach (var brand in brands)
                    {
                        await _context.Brands.AddAsync(brand);
                    }
                    await _context.SaveChangesAsync();
                }
            }
            if (_context.Categories.Count() == 0)
            {
                var CategoriesData = File.ReadAllText("../RepoLayer/Data/DataSeed/categories.json");
                if (CategoriesData?.Count() > 0)
                {
                    var categories = JsonSerializer.Deserialize<List<ProductCategory>>(CategoriesData);
                    categories = categories.Select(b => new ProductCategory() { Name = b.Name }).ToList();
                    foreach (var category in categories)
                    {
                        await _context.Categories.AddAsync(category);
                    }
                    await _context.SaveChangesAsync();
                }
            }
            if (_context.Products.Count() == 0)
            {
                var ProductsData = File.ReadAllText("../RepoLayer/Data/DataSeed/products.json");
                if (ProductsData?.Count() > 0)
                {
                    var products = JsonSerializer.Deserialize<List<Product>>(ProductsData);
                    products = products.Select(b => new Product() { Name = b.Name, Description = b.Description, PictureUrl = b.PictureUrl, Price = b.Price, CategoryId = b.CategoryId, BrandId = b.BrandId }).ToList();
                    foreach (var product in products)
                    {
                        await _context.Products.AddAsync(product);
                    }
                    await _context.SaveChangesAsync();
                }
            }

        }
    }
}
