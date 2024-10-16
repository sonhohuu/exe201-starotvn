using AutoMapper;
using Exe.Starot.Application.Common.Interfaces;
using Exe.Starot.Domain.Common.Exceptions;
using Exe.Starot.Domain.Entities.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.Reader.Queries.GetById
{
    public class GetReaderByIdQuery : IRequest<ReaderWithInfoDTO>
    {
    }

    public class GetReaderByIdQueryHandler : IRequestHandler<GetReaderByIdQuery, ReaderWithInfoDTO>
    {
        private readonly IReaderRepository _readerRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public GetReaderByIdQueryHandler(IReaderRepository readerRepository, IMapper mapper, ICurrentUserService currentUserService)
        {
            _readerRepository = readerRepository;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<ReaderWithInfoDTO> Handle(GetReaderByIdQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            if (userId == null)
            {
                throw new UnauthorizedException("User not login");
            }
            // Find the reader by Id
            var reader = await _readerRepository.FindAsync(r => r.User.ID == userId && r.DeletedDay == null, cancellationToken);

            if (reader == null)
            {
                throw new NotFoundException("Reader not found.");
            }

            // Map the reader entity to ReaderDTO
            var dto = reader.MapToReaderWithInfoDTO(_mapper);

            return dto;
        }
    }

}
