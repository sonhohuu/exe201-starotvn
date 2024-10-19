using AutoMapper;
using Exe.Starot.Application.Common.Interfaces;
using Exe.Starot.Domain.Common.Exceptions;
using Exe.Starot.Domain.Entities.Base;
using Exe.Starot.Domain.Entities.Repositories;
using Exe.Starot.Infrastructure.Repositories;
using MediatR;
using System.Linq;

namespace Exe.Starot.Application.User.Authenticate
{
    public class LoginGoogleQueryHandler : IRequestHandler<LoginGoogleQuery, UserLoginDTO>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;
        private readonly ICustomerRepository _customerRepository;
        public LoginGoogleQueryHandler(IUserRepository userRepository, IMapper mapper, IJwtService jwtService, ICustomerRepository customerRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _jwtService = jwtService;
            _customerRepository = customerRepository;
        }

        public async Task<UserLoginDTO> Handle(LoginGoogleQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindAsync(x => x.Email == request.Email);

            if (user == null)
            {
                user = new UserEntity
                {
                    Email = request.Email,
                    FirstName = string.Empty,
                    LastName = string.Empty,
                    Role = "Customer", // Default role as Customer
                    Balance = 0,
                    Phone = string.Empty,
                    DateOfBirth = null,
                    Gender = string.Empty,
                    Image = string.Empty
                };
                _userRepository.Add(user);

                var customer = new CustomerEntity
                {
                    UserId = user.ID,
                    Membership = 0 // Default membership level
                };

                _customerRepository.Add(customer);

                // Save the changes
                await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

               
            }
            var accessToken = _jwtService.CreateToken(user.ID, user.Role, user.Email, user.LastName);
            var refreshToken = _jwtService.GenerateRefreshToken();

            await _userRepository.UpdateRefreshTokenAsync(user, refreshToken, DateTime.UtcNow.AddDays(30));

            var userLoginDto = _mapper.Map<UserLoginDTO>(user);
            userLoginDto.Token = accessToken;
            userLoginDto.RefreshToken = refreshToken;
            return userLoginDto;
        }
    }
}
