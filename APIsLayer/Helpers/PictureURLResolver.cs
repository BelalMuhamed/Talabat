using APIsLayer.DTOs;
using AutoMapper;
using AutoMapper.Execution;
using CoreLayer.Entities;

namespace APIsLayer.Helpers
{
    public class PictureURLResolver : IValueResolver<Product, ProductToReturnDTOcs, string>
    {
        private readonly IConfiguration config;

        public PictureURLResolver(IConfiguration Config)
        {
            config = Config;
        }
        public string Resolve(Product source, ProductToReturnDTOcs destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
                return $"{config["BaseURL"]}/{source.PictureUrl}";
            return string.Empty;
        }
    }
}
