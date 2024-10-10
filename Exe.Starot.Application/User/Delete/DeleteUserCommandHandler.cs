using Exe.Starot.Application.Booking.Commands.DeleteBooking;
using Exe.Starot.Application.Common.Interfaces;
using Exe.Starot.Domain.Common.Exceptions;
using Exe.Starot.Domain.Entities.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.User.Delete
{
    public record DeleteUserCommand : IRequest<string>
    {
        public string Id { get; init; }
    }
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, string>
    {
        private readonly IReaderRepository _readerRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly ICustomerRepository _customerRepository;
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandHandler(IReaderRepository readerRepository, ICurrentUserService currentUserService, ICustomerRepository customerRepository, IUserRepository userRepository)
        {
            _readerRepository = readerRepository;
            _currentUserService = currentUserService;
            _customerRepository = customerRepository;
            _userRepository = userRepository;
        }

        public async Task<string> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            // Find the customer based on the current user ID
            var user = await _userRepository.FindAsync(c => c.ID == request.Id && c.DeletedDay == null, cancellationToken);

            if (user == null)
            {
                throw new NotFoundException("User not found.");
            }

            if (user.Role == "Reader")
            {
                var existReader = await _readerRepository.FindAsync(x => x.User.ID == user.ID, cancellationToken);
                existReader.DeletedBy = userId;
                existReader.DeletedDay = DateTime.UtcNow;
                _readerRepository.Update(existReader);
            } else if (user.Role == "Customer")
            {
                var existCustomer = await _customerRepository.FindAsync(x => x.User.ID == user.ID, cancellationToken);
                existCustomer.DeletedBy = userId;
                existCustomer.DeletedDay = DateTime.UtcNow;
                _customerRepository.Update(existCustomer);
            }

            // Soft-delete the booking
            user.DeletedDay = DateTime.UtcNow;
            user.DeletedBy = _currentUserService.UserId;
            _userRepository.Update(user);

            // Save the changes to the database
            var result = await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return result > 0 ? "User Deleted Successfully!" : "Failed to delete User!";
        }
    }
}
