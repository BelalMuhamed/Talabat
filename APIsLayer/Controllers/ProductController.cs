using APIsLayer.DTOs;
using APIsLayer.Errors;
using AutoMapper;
using CoreLayer.Entities;
using CoreLayer.RepoContract;
using CoreLayer.Specifications;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepoLayer.UnitOfWork;

namespace APIsLayer.Controllers
{
    
    public class ProductController : BaseController
    {
        private readonly UnitOfWork unit;

       
        private readonly IMapper mapper;

        public ProductController(UnitOfWork unit,IMapper mapper)
        {
            this.unit = unit;
            
            this.mapper = mapper;
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductResponseForPagination<ProductToReturnDTOcs>>>> GetAll([FromQuery]SpecProductParams Params)
        {
            ProductIncludeCategoryAndBrandSpecfication spec = new ProductIncludeCategoryAndBrandSpecfication(Params);
            var products = await unit.Repository<Product>().GetAllAsyncBySpec(spec);
            var ProductsDto = mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDTOcs>>(products);
            var countSpec = new SpecPaginationCountForSpec(Params);
            var count = await unit.Repository<Product>().GetCountAsync(countSpec);

            return Ok(new ProductResponseForPagination<ProductToReturnDTOcs>(Params.PageSize, Params.PageIndex, count, ProductsDto));

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDTOcs>> Get(int id)
        {
            ProductIncludeCategoryAndBrandSpecfication spec = new ProductIncludeCategoryAndBrandSpecfication( p=>p.Id== id);
            var product = await unit.Repository<Product>().GetAsyncBySpec(spec);
            if (product != null)
                return Ok(mapper.Map<Product,ProductToReturnDTOcs>(product));
            return NotFound(new APIResponse(404));

        }
        [HttpGet("categories")]
        public async Task<ActionResult<IReadOnlyList<CategoryDto>>> GetCategories()
        {
            var categories = await unit.Repository<ProductCategory>().GetAllAsync();
            var CategoriesDto = mapper.Map<IReadOnlyList<ProductCategory>, IReadOnlyList<CategoryDto>>(categories);
            return Ok(CategoriesDto);
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<BrandsDto>>> GetBrands()
        {
            var brands = await unit.Repository<ProductBrand>().GetAllAsync();
            var brandsDto = mapper.Map<IReadOnlyList<ProductBrand>, IReadOnlyList<BrandsDto>>(brands);
            return Ok(brandsDto);
        }
    }
}
