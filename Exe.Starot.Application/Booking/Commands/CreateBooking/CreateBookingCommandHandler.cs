using Exe.Starot.Application.Common.Interfaces;
using Exe.Starot.Domain.Common.Exceptions;
using Exe.Starot.Domain.Entities.Base;
using Exe.Starot.Domain.Entities.Repositories;
using Exe.Starot.Infrastructure.Repositories;
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
        private readonly IUserRepository _userRepository;

        public CreateBookingCommandHandler(IBookingRepository bookingRepository, ICurrentUserService currentUserService, IReaderRepository readerRepository, IPackageQuestionRepository packageQuestionRepository, ICustomerRepository customerRepository, IUserRepository userRepository)
        {
            _bookingRepository = bookingRepository;
            _currentUserService = currentUserService;
            _readerRepository = readerRepository;
            _packageQuestionRepository = packageQuestionRepository;
            _customerRepository = customerRepository;
            _userRepository = userRepository;
        }

        public async Task<string> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            // Find the customer based on the current user ID
            var customer = await _customerRepository.FindAsync(c => c.UserId == userId && c.DeletedDay == null, cancellationToken);

            if (customer == null)
            {
                throw new NotFoundException("User not login.");
            }

            var customerId = customer.ID;

            var existingPackage = await _packageQuestionRepository.FindAsync(x => x.ID == request.PackageId && !x.DeletedDay.HasValue, cancellationToken); ;

            if (existingPackage == null)
            {
                throw new NotFoundException("Package does not exist");
            }

            var existingReader = await _readerRepository.FindAsync(x => x.User.ID == request.ReaderId && !x.DeletedDay.HasValue, cancellationToken);

            if (existingReader == null)
            {
                throw new NotFoundException("Reader does not exist");
            }

            // Extract StartHour and Date from StartDate
            var startHour = request.StartDate.ToString("HH:00");
            var date = request.StartDate.ToString("dd/MM/yyyy");

            // Validate if the booking already exists with the same reader or another reader at the same time, but allow booking with a different package
            var existingBooking = await _bookingRepository.FindAsync(
                x => x.Date == date &&
                     x.StartHour == startHour &&
                     !x.DeletedDay.HasValue &&
                     x.Status != "Đã hủy" &&
                     (
                         (x.Reader.UserId == request.ReaderId) || // Same reader and same package
                         (x.CustomerId == customerId && x.Reader.UserId != request.ReaderId) // Same customer with different reader
                     ),
                cancellationToken);

            if (existingBooking != null)
            {   
                if (existingBooking.Reader.UserId == request.ReaderId)
                {
                    // Booking already exists with the same reader and same package at the same time
                    throw new DuplicationException($"This booking with {existingBooking.Reader.User.LastName} at time {existingBooking.StartHour} already ordered.");
                }
                else if (existingBooking.CustomerId == customerId && existingBooking.Reader.UserId != request.ReaderId)
                {
                    // Customer already has a booking with another reader at the same time
                    throw new DuplicationException($"You already have a booking with {existingBooking.Reader.User.LastName} at time {existingBooking.StartHour}.");
                }
            }

            if (customer.User.Balance < existingPackage.Price)
            {
                // If balance is insufficient, return a failure message
                throw new ArgumentException("Insufficient balance.");
            }

            // Deduct the user's balance
            customer.User.Balance -= existingPackage.Price;
            await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            // Create new booking entity
            var booking = new BookingEntity
            {
                PackageId = request.PackageId,
                CustomerId = customerId,
                ReaderId = existingReader.ID,
                StartHour = startHour,
                Date = date,
                EndHour = startHour,
                Status = "Sắp diễn ra", // Default status to Pending 
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
