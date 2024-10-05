using AutoMapper;
using Exe.Starot.Application.Common.Pagination;
using Exe.Starot.Domain.Entities.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.PackageQuestion.Queries.Filter
{
    public class PackageQuestionFilterQuery : IRequest<PagedResult<PackageQuestionDTO>>
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class PackageQuestionFilterQueryHandler : IRequestHandler<PackageQuestionFilterQuery, PagedResult<PackageQuestionDTO>>
    {
        private readonly IPackageQuestionRepository _packageQuestionRepository;
        private readonly IMapper _mapper;

        public PackageQuestionFilterQueryHandler(IPackageQuestionRepository packageQuestionRepository, IMapper mapper)
        {
            _packageQuestionRepository = packageQuestionRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<PackageQuestionDTO>> Handle(PackageQuestionFilterQuery request, CancellationToken cancellationToken)
        {
            // Fetch all package questions that are not soft-deleted
            var packageQuestions = await _packageQuestionRepository.FindAllAsync(pq => pq.DeletedDay == null, cancellationToken);

            // Apply filters
            if (!string.IsNullOrEmpty(request.Name))
            {
                packageQuestions = packageQuestions.Where(pq => pq.Name.ToLower().Contains(request.Name.ToLower())).ToList();
            }

            if (!string.IsNullOrEmpty(request.Description))
            {
                packageQuestions = packageQuestions.Where(pq => pq.Description.ToLower().Contains(request.Description.ToLower())).ToList();
            }

            if (request.MinPrice.HasValue)
            {
                packageQuestions = packageQuestions.Where(pq => pq.Price >= request.MinPrice.Value).ToList();
            }

            if (request.MaxPrice.HasValue)
            {
                packageQuestions = packageQuestions.Where(pq => pq.Price <= request.MaxPrice.Value).ToList();
            }

            // If no records are found, return an empty paged result
            if (!packageQuestions.Any())
            {
                return new PagedResult<PackageQuestionDTO>
                {
                    Data = new List<PackageQuestionDTO>(),
                    TotalCount = 0,
                    PageCount = 0,
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize
                };
            }

            // Handle pagination
            var query = packageQuestions.AsQueryable();
            var totalCount = query.Count();
            var items = query.Skip((request.PageNumber - 1) * request.PageSize)
                             .Take(request.PageSize)
                             .ToList();

            var pageCount = (int)Math.Ceiling((double)totalCount / request.PageSize);
            var dtos = _mapper.Map<List<PackageQuestionDTO>>(items);

            // Return paged result
            return new PagedResult<PackageQuestionDTO>
            {
                Data = dtos,
                TotalCount = totalCount,
                PageCount = pageCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }

}
