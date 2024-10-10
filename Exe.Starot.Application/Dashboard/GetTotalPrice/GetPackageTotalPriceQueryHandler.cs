using AutoMapper;
using Exe.Starot.Domain.Entities.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.Dashboard.GetTotalPrice
{
    public class GetPackageTotalPriceQueryHandler : IRequestHandler<GetPackageTotalPriceQuery, List<PackageTotalPriceDTO>>
    {
        private readonly IPackageQuestionRepository _packageQuestionRepository;
        private readonly IMapper _mapper;

        public GetPackageTotalPriceQueryHandler(IPackageQuestionRepository packageQuestionRepository, IMapper mapper)
        {
            _packageQuestionRepository = packageQuestionRepository;
            _mapper = mapper;
        }

        public async Task<List<PackageTotalPriceDTO>> Handle(GetPackageTotalPriceQuery request, CancellationToken cancellationToken)
        {
            // Get all packages that are not deleted
            var packages = await _packageQuestionRepository.FindAllAsync(t => t.DeletedDay == null, cancellationToken);

            // Calculate total price across all packages
            var overallTotalPrice = packages.Sum(p => p.Bookings.Count() * p.Price);

            // Prepare the list of PackageTotalPriceDTO with the percentage calculation
            var packageTotals = packages.Select(p => new PackageTotalPriceDTO
            {
                PackageId = p.ID,
                Name = p.Name,
                BookingCount = p.Bookings.Count(),
                TotalPrice = p.Bookings.Count() * p.Price,
                PricePercentage = (p.Bookings.Count() * p.Price) / overallTotalPrice * 100  // Calculate percentage
            }).ToList();
              
            return packageTotals;
        }
    }
}
