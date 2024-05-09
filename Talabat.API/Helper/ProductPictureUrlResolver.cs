using AutoMapper;
using AutoMapper.Execution;
using Microsoft.IdentityModel.Tokens;
using Talabat.API.Dtos;
using Talabat.Core.Enities;

namespace Talabat.API.Helper
{
    public class ProductPictureUrlResolver : IValueResolver<Product, ProductDto, string>
    {
        private readonly IConfiguration _configration;

        // inject the confiration from AppSettings
        public ProductPictureUrlResolver(IConfiguration Configration)
        {
            _configration = Configration;
        }

        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
                return $"{_configration["ApIBaseURl"]}/{source.PictureUrl}";

            return string.Empty;

        }
    }
}
