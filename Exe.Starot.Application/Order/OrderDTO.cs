using AutoMapper;
using Exe.Starot.Application.Common.Mappings;
using Exe.Starot.Domain.Entities.Base;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.Order
{
    public class OrderDTO : IMapFrom<OrderEntity>
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }
        public string OrderDate { get; set; }
        public string OrderTime { get; set; }
        public string Address { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }

        public List<ResponseItem> Products { get; set; } = new List<ResponseItem>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<OrderEntity, OrderDTO>()
                 .ForMember(dto => dto.UserName, opt => opt.MapFrom(entity => entity.User.FirstName + " " + entity.User.LastName))
                 .ForMember(dto => dto.Phone, opt => opt.MapFrom(entity => entity.User.Phone))
                 .ForMember(dto => dto.Products, opt => opt.MapFrom(entity => entity.OrderDetails));


            profile.CreateMap<OrderDetailEntity, ResponseItem>()
                .ForMember(dto => dto.Name, opt => opt.MapFrom(entity => entity.Product.Name))
                .ForMember(dto => dto.UnitPrice, opt => opt.MapFrom(entity => entity.UnitPrice))
                .ForMember(dto => dto.Amount, opt => opt.MapFrom(entity => entity.Amount))
                .ForMember(dto => dto.Price, opt => opt.MapFrom(entity => entity.UnitPrice))
                .ForMember(dto => dto.Image, opt => opt.MapFrom(entity => entity.Product.Image))
                .ForMember(dto => dto.ProductID, opt => opt.MapFrom(entity => entity.ProductId));
        

        }

    
        public class ResponseItem
        {
            public string ProductID { get; set; }

            public string Name { get; set; }
            public string Image {  get; set; }

            public decimal UnitPrice { get; set; }

            public int Amount { get; set; }

            public decimal Price { get; set; }

        }
    }
}
