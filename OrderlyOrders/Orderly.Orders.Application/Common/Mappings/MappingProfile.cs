using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Orderly.Orders.Application.CommandsAndQueries.ViewModels;
using Orderly.Orders.Domain.Interfaces;

namespace Orderly.Orders.Application.Common.Mappings
{
    public class MappingProfile:Profile
    {
        public MappingProfile() {

            CreateMap<Orderly.Orders.Domain.Entities.Order, OrderListViewModel>().ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
                    .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice))
                    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                    .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt));

            CreateMap<Orderly.Orders.Domain.Entities.Order, OrderViewModel>().ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt));

            CreateMap<Orderly.Orders.Domain.Entities.OrderItem, OrderItemViewModel>().ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId))
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));


        }
    }
}
