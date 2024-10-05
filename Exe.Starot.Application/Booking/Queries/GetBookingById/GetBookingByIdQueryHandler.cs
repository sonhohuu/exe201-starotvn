using AutoMapper;
using Exe.Starot.Domain.Common.Exceptions;
using Exe.Starot.Domain.Entities.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.Booking.Queries.GetBookingById
{
    public class GetBookingByIdQuery : IRequest<BookingDTO>
    {
        public string BookingId { get; set; }
    }

    public class GetBookingByIdQueryHandler : IRequestHandler<GetBookingByIdQuery, BookingDTO>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;

        public GetBookingByIdQueryHandler(IBookingRepository bookingRepository, IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }

        public async Task<BookingDTO> Handle(GetBookingByIdQuery request, CancellationToken cancellationToken)
        {
            // Find the booking by its ID
            var booking = await _bookingRepository.FindAsync(b => b.ID == request.BookingId && b.DeletedDay == null, cancellationToken);

            // If booking is not found, throw a NotFoundException
            if (booking == null)
            {
                throw new NotFoundException($"Booking with ID {request.BookingId} not found.");
            }

            // Map the booking entity to BookingDto and return
            return booking.MapToBookingDTO(_mapper);
        }
    }

}
