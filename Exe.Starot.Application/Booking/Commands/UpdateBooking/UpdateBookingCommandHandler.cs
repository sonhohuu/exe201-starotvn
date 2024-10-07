using Exe.Starot.Application.Common.Interfaces;
using Exe.Starot.Domain.Common.Exceptions;
using Exe.Starot.Domain.Entities.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.Booking.Commands.UpdateBooking
{
    public record UpdateBookingCommand : IRequest<string>
    {
        public string BookingId { get; init; }
        public int? PackageId { get; init; }
        public string? ReaderId { get; init; }
        public DateTime? StartDate { get; init; }
        public string? Status { get; init; }
    }

    public class UpdateBookingCommandHandler : IRequestHandler<UpdateBookingCommand, string>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IReaderRepository _readerRepository;
        private readonly IPackageQuestionRepository _packageQuestionRepository;

        public UpdateBookingCommandHandler(IBookingRepository bookingRepository, ICurrentUserService currentUserService, IReaderRepository readerRepository, IPackageQuestionRepository packageQuestionRepository)
        {
            _bookingRepository = bookingRepository;
            _currentUserService = currentUserService;
            _readerRepository = readerRepository;
            _packageQuestionRepository = packageQuestionRepository;
        }

        public async Task<string> Handle(UpdateBookingCommand request, CancellationToken cancellationToken)
        {
            // Get current user
            var customerId = _currentUserService.UserId;

            // Find the existing booking
            var booking = await _bookingRepository.FindAsync(x => x.ID == request.BookingId && x.CustomerId == customerId && !x.DeletedDay.HasValue, cancellationToken);

            if (booking == null)
            {
                throw new NotFoundException("Booking not found.");
            }

            // Check if the new reader exists
            var existingReader = await _readerRepository.FindAsync(x => x.User.ID == request.ReaderId && !x.DeletedDay.HasValue, cancellationToken);
            if (existingReader == null)
            {
                throw new NotFoundException("Reader does not exist.");
            }

            // Update booking details
            booking.PackageId = request.PackageId ?? booking.PackageId;
            booking.ReaderId = request.ReaderId ?? booking.ReaderId;
            booking.StartDate = request.StartDate ?? booking.StartDate;
            if (request.StartDate != null || booking.PackageId != null)
            {
                var existingPackage = await _packageQuestionRepository.FindAsync(x => x.ID == request.PackageId && !x.DeletedDay.HasValue, cancellationToken);

                if (existingPackage != null)
                {
                    throw new NotFoundException("Package does not exist");
                }
                booking.EndDate = booking.StartDate.AddMinutes(existingPackage.Time);
            }

            booking.Status = request.Status ?? booking.Status;
            booking.LinkUrl = existingReader?.LinkUrl;

            // Mark as modified
            booking.UpdatedBy = _currentUserService.UserId;
            booking.LastUpdated = DateTime.UtcNow;
            _bookingRepository.Update(booking);

            // Save the changes to the database
            var result = await _bookingRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return result > 0 ? "Booking Updated Successfully!" : "Failed to update booking!";
        }
    }


}
