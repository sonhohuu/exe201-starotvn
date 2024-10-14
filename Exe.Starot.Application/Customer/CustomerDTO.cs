using AutoMapper;
using Exe.Starot.Application.Common.Mappings;
using Exe.Starot.Application.PackageQuestion;
using Exe.Starot.Domain.Entities.Base;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.Customer
{
    public class CustomerDTO : IMapFrom<CustomerEntity>
    {
        public string CustomerId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Image { get; set; }
        public string? Phone { get; set; }
        public string? Gender { get; set; }
        public string? DateOfBirth { get; set; }
        public int MemberShip { get; set; } = 0;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CustomerEntity, CustomerDTO>()
                .ForMember(dto => dto.CustomerId, opt => opt.MapFrom(entity => entity.User.ID))
                .ForMember(dto => dto.FirstName, opt => opt.MapFrom(entity => entity.User.FirstName))
                .ForMember(dto => dto.LastName, opt => opt.MapFrom(entity => entity.User.LastName))
                .ForMember(dto => dto.Image, opt => opt.MapFrom(entity => entity.User.Image))
                .ForMember(dto => dto.Phone, opt => opt.MapFrom(entity => entity.User.Phone))
                .ForMember(dto => dto.Gender, opt => opt.MapFrom(entity => entity.User.Gender))
                .ForMember(dto => dto.DateOfBirth, opt => opt.MapFrom(entity => entity.User.DateOfBirth));
                
        }
    }
}
