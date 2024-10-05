using Exe.Starot.Application.Common.Interfaces;
using Exe.Starot.Application.FileUpload;
using Exe.Starot.Domain.Common.Exceptions;
using Exe.Starot.Domain.Entities.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.Customer.Commands.UpdateCustomer
{
    public record class UpdateCustomerCommand() : IRequest<string>
    {
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public IFormFile? Image { get; init; }
        public string? Phone {  get; init; }
        public DateTime? DateOfBirth { get; init; }
        public int MemberShip { get; init; } = 0;
    }
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, string>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly FileUploadService _fileUploadService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserRepository _userRepository;

        public UpdateCustomerCommandHandler(ICustomerRepository customerRepository, FileUploadService fileUploadService, ICurrentUserService currentUserService, IUserRepository userRepository)
        {
            _customerRepository = customerRepository;
            _fileUploadService = fileUploadService;
            _currentUserService = currentUserService;
            _userRepository = userRepository;
        }

        public async Task<string> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            // Find the existing customer by Id or another unique identifier
            var user = await _userRepository.FindAsync(x => x.ID == userId && !x.DeletedDay.HasValue, cancellationToken);

            if (user == null)
            {
                throw new NotFoundException("Customer not found.");
            }
            var customer = await _customerRepository.FindAsync(x => x.UserId == userId && !x.DeletedDay.HasValue, cancellationToken);

            if (customer == null)
            {
                throw new NotFoundException("Customer not found.");
            }

            // Update fields if they are provided
            if (!string.IsNullOrEmpty(request.FirstName)) user.FirstName = request.FirstName;
            if (!string.IsNullOrEmpty(request.LastName)) user.LastName = request.LastName;
            if (!string.IsNullOrEmpty(request.Phone)) user.Phone = request.Phone;
            if (request.DateOfBirth.HasValue) user.DateOfBirth = request.DateOfBirth.Value;
            if (request.MemberShip != 0 ) customer.Membership += request.MemberShip;

            // Handle image upload if a new image is provided
            if (request.Image != null)
            {
                string imageUrl = string.Empty;
                using (var stream = request.Image.OpenReadStream())
                {
                    imageUrl = await _fileUploadService.UploadFileAsync(stream, $"{Guid.NewGuid()}.jpg");
                }
                user.Image = imageUrl;
            }

            // Update the modified by field
            user.UpdatedBy = _currentUserService.UserId;
            user.LastUpdated = DateTime.UtcNow;

            // Save the changes
            _customerRepository.Update(customer);
            _userRepository.Update(user);
            return await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Update Successfully!" : "Update Failed!";
        }
    }

}
