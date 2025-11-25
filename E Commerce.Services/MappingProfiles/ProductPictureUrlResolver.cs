using AutoMapper;
using E_Commerce.Domain.Entities.ProductModule;
using E_Commerce.Shared.DTOs.ProductDTOs;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.MappingProfiles
{




   
    public class ProductPictureUrlResolver : IValueResolver<Product, ProductDTO, string>
    {
        private readonly IConfiguration _configuration;

        public ProductPictureUrlResolver(IConfiguration  configuration)
        {
           _configuration = configuration;
        }



        public string Resolve(Product source, ProductDTO destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PictureUrl))
            {
                return null;
            }

            if (source.PictureUrl.StartsWith("http"))
            {
                return source.PictureUrl;
            }

            var baseurl = _configuration.GetSection("URls")["BaseUrl"];
            if (string.IsNullOrEmpty(baseurl))
            {
                return string.Empty;
            }

            var baseUrl = $"{baseurl}{source.PictureUrl}";
            return baseUrl;
        }
    }
}
