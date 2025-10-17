using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Orderly.Catalog.Application.CommandsAndQueries.Product.ViewModels;
using Orderly.Catalog.Domain.Entities;

namespace Orderly.Catalog.Application.Common.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile() {

            CreateMap<Product, ProductViewModel>().ForMember(dest => dest.VendorName, opt => opt.MapFrom(src => src.Vendor.Name))
                 .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                 .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                 .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        }

    }
}
