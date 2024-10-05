using Exe.Starot.Application.Common.Interfaces;
using Exe.Starot.Domain.Common.Exceptions;
using Exe.Starot.Domain.Entities.Repositories;
using Exe.Starot.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.Booking.Commands.DeleteBooking
{
    public record DeleteBookingCommand : IRequest<string>
    {
        public string BookingId { get; init; }
    }

    public class DeleteBookingCommandHandler : IRequestHandler<DeleteBookingCommand, string>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly ICustomerRepository _customerRepository;

        public DeleteBookingCommandHandler(IBookingRepository bookingRepository, ICurrentUserService currentUserService, ICustomerRepository customerRepository)
        {
            _bookingRepository = bookingRepository;
            _currentUserService = currentUserService;
            _customerRepository = customerRepository;
        }

        public async Task<string> Handle(DeleteBookingCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            // Find the customer based on the current user ID
            var customer = await _customerRepository.FindAsync(c => c.UserId == userId && c.DeletedDay == null, cancellationToken);

            if (customer == null)
            {
                throw new NotFoundException("Customer not found.");
            }

            var customerId = customer.ID;

            // Find the existing booking
            var booking = await _bookingRepository.FindAsync(x => x.ID == request.BookingId && x.CustomerId == customerId && !x.DeletedDay.HasValue, cancellationToken);

            if (booking == null)
            {
                throw new NotFoundException("Booking not found.");
            }

            // Soft-delete the booking
            booking.DeletedDay = DateTime.UtcNow;
            booking.DeletedBy = _currentUserService.UserId;
            _bookingRepository.Update(booking);

            // Save the changes to the database
            var result = await _bookingRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return result > 0 ? "Booking Deleted Successfully!" : "Failed to delete booking!";
        }
    }

}
