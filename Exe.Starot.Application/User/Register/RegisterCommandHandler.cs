using AutoMapper;
using Exe.Starot.Application.Common.Interfaces;
using Exe.Starot.Application.User.Authenticate;
using Exe.Starot.Domain.Common.Exceptions;
using Exe.Starot.Domain.Entities.Base;
using Exe.Starot.Domain.Entities.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.User.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;
        private readonly IReaderRepository _readerRepository;

        public RegisterCommandHandler(IUserRepository userRepository, IJwtService jwtService, IMapper mapper, ICustomerRepository customerRepository, IReaderRepository readerRepository)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _mapper = mapper;
            _customerRepository = customerRepository;
            _readerRepository = readerRepository;
        }

        public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            // Check if password and re-password match
            if (request.Password != request.Repassword)
            {
                throw new ArgumentException("Passwords do not match.");
            }

            // Check if a user with the same email already exists
            var existingUser = await _userRepository.FindAsync(u => u.Email == request.Email && !u.DeletedDay.HasValue, cancellationToken);

            if (existingUser != null)
            {
                throw new DuplicationException("Account with this Email is already registered.");
            }

            if (request.Role != "Customer" && request.Role != "Reader")
            {
                throw new ArgumentException("Role accepted: Customer, Reader");
            }

            // Create a new user entity
            var newUser = new UserEntity
            {
                Email = request.Email,
                PasswordHash = HashPassword(request.Password),
                Role = request.Role ?? ""
            };

            if (request.Role == "Customer")
            {
                var customer = new CustomerEntity
                {
                    User = newUser
                };
                _customerRepository.Add(customer);
            }
            else if (request.Role == "Reader")
            {
                var reader = new ReaderEntity
                {
                    User = newUser
                };
                _readerRepository.Add(reader);
            }

            await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            // Return success message
            return "Registration successful!";
        }

        private string HashPassword(string password)
        {
            // Implement your password hashing logic here, e.g., using BCrypt:
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
