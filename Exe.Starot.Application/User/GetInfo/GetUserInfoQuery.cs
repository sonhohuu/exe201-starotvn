using AutoMapper;
using Exe.Starot.Application.Customer.Queries.GetById;
using Exe.Starot.Application.Customer;
using Exe.Starot.Domain.Common.Exceptions;
using Exe.Starot.Domain.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Exe.Starot.Application.Common.Interfaces;

namespace Exe.Starot.Application.User.GetInfo
{
    public class GetUserInfo : IRequest<UserDTO>
    {

    }
    public class GetUserInfoQuery : IRequestHandler<GetUserInfo, UserDTO>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserInfoQuery(ICurrentUserService currentUserService, IMapper mapper, IUserRepository userRepository)
        {
            _currentUserService = currentUserService;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<UserDTO> Handle(GetUserInfo request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            if (userId == null)
            {
                throw new UnauthorizedException("User not login");
            }



            // Find the customer by Id
            var user = await _userRepository.FindAsync(c => c.ID == userId && c.DeletedDay == null, cancellationToken);

            if (user == null)
            {
                throw new NotFoundException("User not found.");
            }
            var dto = user.MapToUserDTO(_mapper);

            return dto;
        }
    }
}
