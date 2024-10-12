﻿using AutoMapper;
using Exe.Starot.Domain.Common.Exceptions;
using Exe.Starot.Domain.Entities.Repositories;
using MediatR;
using System.Linq;

namespace Exe.Starot.Application.User.Authenticate
{
    public class LoginGoogleQueryHandler : IRequestHandler<LoginGoogleQuery, UserLoginDTO>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public LoginGoogleQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserLoginDTO> Handle(LoginGoogleQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindAsync(x => x.Email == request.Email);

            if (user == null)
            {
                throw new NotFoundException("User does not exist!");
            }

            var userLoginDto = _mapper.Map<UserLoginDTO>(user);

            var customer = user.Customers?.FirstOrDefault();
            if (customer != null)
            {
                userLoginDto.EntityId = customer.ID.ToString();
                userLoginDto.Role = "Customer";
            }
            else if (user.Readers?.Any() == true)
            {
                var reader = user.Readers.FirstOrDefault();
                userLoginDto.EntityId = reader?.ID.ToString();
                userLoginDto.Role = "Reader";
            }
            else
            {
                throw new NotFoundException($"No associated entity found for user - {request.Email}");
            }

            return userLoginDto;
        }
    }
}