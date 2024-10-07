using AutoMapper;
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
    public class GetReaderByIdQuery : IRequest<ReaderDTO>
    {
        [Required]
        public required string Id { get; set; }
    }

    public class GetReaderByIdQueryHandler : IRequestHandler<GetReaderByIdQuery, ReaderDTO>
    {
        private readonly IReaderRepository _readerRepository;
        private readonly IMapper _mapper;

        public GetReaderByIdQueryHandler(IReaderRepository readerRepository, IMapper mapper)
        {
            _readerRepository = readerRepository;
            _mapper = mapper;
        }

        public async Task<ReaderDTO> Handle(GetReaderByIdQuery request, CancellationToken cancellationToken)
        {
            // Find the reader by Id
            var reader = await _readerRepository.FindAsync(r => r.User.ID == request.Id && r.DeletedDay == null, cancellationToken);

            if (reader == null)
            {
                throw new NotFoundException("Reader not found.");
            }

            // Map the reader entity to ReaderDTO
            var dto = reader.MapToReaderDTO(_mapper);

            return dto;
        }
    }

}
