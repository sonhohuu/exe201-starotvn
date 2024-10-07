using AutoMapper;
using Exe.Starot.Application.Common.Mappings;
using Exe.Starot.Application.Customer;
using Exe.Starot.Domain.Entities.Base;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.Reader
{
    public class ReaderDTO : IMapFrom<ReaderEntity>
    {
        public string ReaderId { get; set; }
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public string? Image { get; init; }
        public string? Phone { get; init; }
        public DateTime? DateOfBirth { get; init; }
        public int MemberShip { get; init; } = 0;
        public string? Expertise { get; init; }
        public string? Quote { get; init; }
        public string? Experience { get; init; }
        public decimal? Rating { get; init; }
        public string? LinkUrl { get; init; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ReaderEntity, ReaderDTO>()
                .ForMember(dto => dto.ReaderId, opt => opt.MapFrom(entity => entity.User.ID))
                .ForMember(dto => dto.FirstName, opt => opt.MapFrom(entity => entity.User.FirstName))
                .ForMember(dto => dto.LastName, opt => opt.MapFrom(entity => entity.User.LastName))
                .ForMember(dto => dto.Image, opt => opt.MapFrom(entity => entity.User.Image))
                .ForMember(dto => dto.Phone, opt => opt.MapFrom(entity => entity.User.Phone))
                .ForMember(dto => dto.DateOfBirth, opt => opt.MapFrom(entity => entity.User.DateOfBirth));
                
        }
    }
}
