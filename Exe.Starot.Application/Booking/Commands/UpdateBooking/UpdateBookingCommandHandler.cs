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
        public string? Status { get; init; }
    }

    public class UpdateBookingCommandHandler : IRequestHandler<UpdateBookingCommand, string>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IPackageQuestionRepository _packageQuestionRepository;
        private readonly IUserRepository _userRepository;

        public UpdateBookingCommandHandler(IBookingRepository bookingRepository, ICurrentUserService currentUserService, IPackageQuestionRepository packageQuestionRepository, IUserRepository userRepository)
        {
            _bookingRepository = bookingRepository;
            _currentUserService = currentUserService;
            _packageQuestionRepository = packageQuestionRepository;
            _userRepository = userRepository;
        }

        public async Task<string> Handle(UpdateBookingCommand request, CancellationToken cancellationToken)
        {
            // Get current user
            var userid = _currentUserService.UserId;
            if (userid == null)
            {
                throw new NotFoundException("User not login");
            }

            // Find the existing booking
            var booking = await _bookingRepository.FindAsync(x => x.ID == request.BookingId  && !x.DeletedDay.HasValue, cancellationToken);

            if (booking == null)
            {
                throw new NotFoundException("Booking not found.");
            }

            if (booking.Status == "Đã hủy")
            {
                throw new ArgumentException("Không thể hủy booking đã hủy");
            }

            if (booking.Status == "Hoàn thành" && request.Status == "Đã hủy")
            {
                throw new ArgumentException("Không thể hủy booking hoàn thành");
            }
            else if (request.Status == "Đã hủy")
            {
                var userOrder = await _userRepository.FindAsync(x => x.ID == booking.Customer.UserId && !x.DeletedDay.HasValue, cancellationToken);
                if (userOrder != null)
                {
                    userOrder.Balance += booking.Package.Price;
                    userOrder.LastUpdated = DateTime.UtcNow;
                    _userRepository.Update(userOrder);
                    await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
                }
            }

            booking.Status = request.Status;

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
