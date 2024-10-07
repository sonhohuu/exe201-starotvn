using Exe.Starot.Application.Common.Interfaces;
using Exe.Starot.Domain.Common.Exceptions;
using Exe.Starot.Domain.Entities.Base;
using Exe.Starot.Domain.Entities.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.Booking.Commands.CreateBooking
{
    public record CreateBookingCommand : IRequest<string>
    {
        public int PackageId { get; init; }
        public string ReaderId { get; init; }
        public DateTime StartDate { get; init; }
    }

    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, string>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IReaderRepository _readerRepository;
        private readonly IPackageQuestionRepository _packageQuestionRepository;
        private readonly ICustomerRepository _customerRepository;

        public CreateBookingCommandHandler(IBookingRepository bookingRepository, ICurrentUserService currentUserService, IReaderRepository readerRepository, IPackageQuestionRepository packageQuestionRepository, ICustomerRepository customerRepository)
        {
            _bookingRepository = bookingRepository;
            _currentUserService = currentUserService;
            _readerRepository = readerRepository;
            _packageQuestionRepository = packageQuestionRepository;
            _customerRepository = customerRepository;
        }

        public async Task<string> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            // Find the customer based on the current user ID
            var customer = await _customerRepository.FindAsync(c => c.UserId == userId && c.DeletedDay == null, cancellationToken);

            if (customer == null)
            {
                throw new NotFoundException("Customer not found.");
            }

            var customerId = customer.ID;

            var existingPackage = await _packageQuestionRepository.FindAsync(x => x.ID == request.PackageId && !x.DeletedDay.HasValue, cancellationToken); ;

            if (existingPackage != null)
            {
                throw new NotFoundException("Package does not exist");
            }

            // Validate if the booking already exists
            var existingBooking = await _bookingRepository.FindAsync(
                x => x.PackageId == request.PackageId &&
                     x.CustomerId == customerId &&
                     x.ReaderId == request.ReaderId &&
                     x.StartDate == request.StartDate &&
                     !x.DeletedDay.HasValue,
                cancellationToken);

            if (existingBooking != null)
            {
                throw new DuplicateWaitObjectException("This booking already exists.");
            }

            var existingReader = await _readerRepository.FindAsync(x => x.User.ID == request.ReaderId && !x.DeletedDay.HasValue,cancellationToken);

            if (existingBooking != null)
            {
                throw new NotFoundException("Reader does not exist");
            }

            // Create new booking entity
            var booking = new BookingEntity
            {
                PackageId = request.PackageId,
                CustomerId = customerId,
                ReaderId = request.ReaderId,
                StartDate = request.StartDate,
                EndDate = request.StartDate.AddMinutes(existingPackage.Time),
                Status = "Pending", // Default status to Pending 
                LinkUrl = existingReader?.LinkUrl,

                CreatedBy = _currentUserService.UserId, // Record who created the booking
                CreatedDate = DateTime.UtcNow
            };

            // Save to the repository
            _bookingRepository.Add(booking);

            // Save the changes to the database
            var result = await _bookingRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return result > 0 ? "Booking Created Successfully!" : "Failed to create booking!";
        }
    }

}
