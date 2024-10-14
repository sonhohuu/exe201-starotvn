using AutoMapper;
using Exe.Starot.Application.Common.Mappings;
using Exe.Starot.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.FavoriteProduct
{
    public class FavoriteProductDto : IMapFrom<FavoriteProductEntity>
    {
        public string? ProductId {get; set; }
        public string? ProductName { get; set; }
        public string? ProductPrice { get; set; }
        public string? ProductImage { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<FavoriteProductEntity, FavoriteProductDto>()
                .ForMember(dto => dto.ProductId, opt => opt.MapFrom(entity => entity.Product.ID))
                .ForMember(dto => dto.ProductName, opt => opt.MapFrom(entity => entity.Product.Name))
                .ForMember(dto => dto.ProductPrice, opt => opt.MapFrom(entity => entity.Product.Price))
                .ForMember(dto => dto.ProductImage, opt => opt.MapFrom(entity => entity.Product.Image));
        }
    }
}
