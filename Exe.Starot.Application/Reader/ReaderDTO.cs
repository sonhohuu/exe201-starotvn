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
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Image { get; set; }
        public string? Phone { get; set; }
        public string? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int MemberShip { get; set; } = 0;
        public string? Expertise { get; set; }
        public string? Quote { get; set; }
        public string? Experience { get; set; }
        public decimal? Rating { get; set; }
        public string? LinkUrl { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ReaderEntity, ReaderDTO>()
                .ForMember(dto => dto.ReaderId, opt => opt.MapFrom(entity => entity.User.ID))
                .ForMember(dto => dto.FirstName, opt => opt.MapFrom(entity => entity.User.FirstName))
                .ForMember(dto => dto.LastName, opt => opt.MapFrom(entity => entity.User.LastName))
                .ForMember(dto => dto.Image, opt => opt.MapFrom(entity => entity.User.Image))
                .ForMember(dto => dto.Phone, opt => opt.MapFrom(entity => entity.User.Phone))
                .ForMember(dto => dto.Gender, opt => opt.MapFrom(entity => entity.User.Gender))
                .ForMember(dto => dto.DateOfBirth, opt => opt.MapFrom(entity => entity.User.DateOfBirth));
                
        }
    }
}
