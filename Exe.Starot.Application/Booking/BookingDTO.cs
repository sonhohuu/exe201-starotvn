using AutoMapper;
using Exe.Starot.Application.Common.Mappings;
using Exe.Starot.Application.PackageQuestion;
using Exe.Starot.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.Booking
{
    public class BookingDTO : IMapFrom<BookingEntity>
    {
        public string Id { get; set; }
        public string? PackageName { get; set; }
        public string? CustomerName { get; set; }
        public string? ReaderName { get; set; }
        public string? Date { get; set; }
        public string? StartHour { get; set; }
        public string? Status { get; set; }
        public string? LinkUrl { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<BookingEntity, BookingDTO>()
                .ForMember(dto => dto.PackageName, opt => opt.MapFrom(entity => entity.Package.Name))
                .ForMember(dto => dto.CustomerName, opt => opt.MapFrom(entity => entity.Customer.User.FirstName + " " + entity.Customer.User.LastName))
                .ForMember(dto => dto.ReaderName, opt => opt.MapFrom(entity => entity.Reader.User.FirstName + " " + entity.Reader.User.LastName));
        }
    }
}
