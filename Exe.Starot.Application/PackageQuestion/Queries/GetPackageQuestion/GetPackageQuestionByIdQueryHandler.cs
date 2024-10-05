using AutoMapper;
using Exe.Starot.Domain.Common.Exceptions;
using Exe.Starot.Domain.Entities.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.PackageQuestion.Queries.GetPackageQuestion
{
    public class GetPackageQuestionByIdQuery : IRequest<PackageQuestionDTO>
    {
        public int Id { get; set; }
    }

    public class GetPackageQuestionByIdQueryHandler : IRequestHandler<GetPackageQuestionByIdQuery, PackageQuestionDTO>
    {
        private readonly IPackageQuestionRepository _packageQuestionRepository;
        private readonly IMapper _mapper;

        public GetPackageQuestionByIdQueryHandler(IPackageQuestionRepository packageQuestionRepository, IMapper mapper)
        {
            _packageQuestionRepository = packageQuestionRepository;
            _mapper = mapper;
        }

        public async Task<PackageQuestionDTO> Handle(GetPackageQuestionByIdQuery request, CancellationToken cancellationToken)
        {
            // Retrieve the package question by ID
            var packageQuestion = await _packageQuestionRepository.FindAsync(pq => pq.ID == request.Id && pq.DeletedDay == null, cancellationToken);

            // Check if it exists
            if (packageQuestion == null)
            {
                throw new NotFoundException($"PackageQuestion + {request.Id} not found");
            }

            // Map the entity to a DTO
            return packageQuestion.MapToPackageQuestionDTO(_mapper);
        }
    }

}
