using AutoMapper;
using Exe.Starot.Application.PackageQuestion;
using Exe.Starot.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.Booking
{
    public static class BookingDTOMappingExtention
    {
        public static BookingDTO MapToBookingDTO(this BookingEntity projectFrom, IMapper mapper)
          => mapper.Map<BookingDTO>(projectFrom);

        public static List<BookingDTO> MapToBookingDTOList(this IEnumerable<BookingEntity> projectFrom, IMapper mapper)
          => projectFrom.Select(x => x.MapToBookingDTO(mapper)).ToList();
    }
}
