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
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, UserLoginDTO>
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

        public async Task<UserLoginDTO> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            // Check if password and re-password match
            if (request.Password != request.Repassword)
            {
                throw new ArgumentException("Passwords do not match."); // You can customize this exception
            }

            // Check if a user with the same email already exists
            var existingUser = await _userRepository.FindAsync(u => u.Email == request.Email && !u.DeletedDay.HasValue, cancellationToken);

            if (existingUser != null)
            {
                throw new DuplicationException("Account with this Email is already registered.");
            }

            if (request.Role != "Customer" && request.Role != "Reader")
            {
                throw new ArgumentException("Role accepted: Customer, Reader"); // Handle appropriately
            }

            // Create a new user entity
            var newUser = new UserEntity
            {
                Email = request.Email,
                // Assume you have additional properties like Password, Name, etc. from RegisterCommand
                PasswordHash = HashPassword(request.Password), // Ensure to hash the password
                Role = request.Role ?? "" // Set default role or whatever your logic requires
                                    // Set other properties as necessary
            };

            if (request.Role == "Customer")
            {
                var customer = new CustomerEntity
                {
                    User = newUser
                };
                _customerRepository.Add(customer);
            } else if (request.Role == "Reader")
            {
                var reader = new ReaderEntity
                {
                    User = newUser
                };
                _readerRepository.Add(reader);
            }

            await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            // Create JWT tokens for the new user
            var accessToken = _jwtService.CreateToken(newUser.ID, newUser.Role, newUser.Email);
            var refreshToken = _jwtService.GenerateRefreshToken();

            // Update the refresh token in the user repository
            await _userRepository.UpdateRefreshTokenAsync(newUser, refreshToken, DateTime.UtcNow.AddDays(30));

            // Map the new user to UserLoginDTO
            var userLoginDto = _mapper.Map<UserLoginDTO>(newUser);
            userLoginDto.Token = accessToken;
            userLoginDto.RefreshToken = refreshToken;

            return userLoginDto;
        }

        private string HashPassword(string password)
        {
            // Implement your password hashing logic here
            // For example, using BCrypt:
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }

}
