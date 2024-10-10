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
        public int PackageId { get; set; }
        public string CustomerId { get; set; }
        public string ReaderId { get; set; }
        public string Date { get; set; }
        public string StartHour { get; set; }
        public string Status { get; set; }
        public string LinkUrl { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<BookingEntity, BookingDTO>();
        }
    }
}
