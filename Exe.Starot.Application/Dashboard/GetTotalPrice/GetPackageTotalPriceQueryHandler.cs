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
            // Set default month and year to the current month and year if not provided
            int month = request.Month > 0 ? request.Month : DateTime.Now.Month;
            int year = request.Year > 0 ? request.Year : DateTime.Now.Year;

            var packages = await _packageQuestionRepository.FindAllAsync(t => t.DeletedDay == null, cancellationToken);

            // Filter bookings by month and year based on CreatedDate
            var filteredPackages = packages
                .Select(p => new
                {
                    Package = p,
                    Bookings = p.Bookings.Where(b => b.CreatedDate != null &&
                                                      b.CreatedDate.Value.Month == month &&
                                                      b.CreatedDate.Value.Year == year).ToList()
                })
                .Where(p => p.Bookings.Any()) // Only keep packages with bookings in the specified period
                .ToList();

            // Calculate overall total price
            var overallTotalPrice = filteredPackages.Sum(p => p.Bookings.Count * p.Package.Price);

            // Prepare the result list with the percentage calculation
            var packageTotals = filteredPackages
                .Select(p => new PackageTotalPriceDTO
                {
                    PackageId = p.Package.ID,
                    Name = p.Package.Name,
                    BookingCount = p.Bookings.Count(),
                    TotalPrice = p.Bookings.Count() * p.Package.Price,
                    PricePercentage = (p.Bookings.Count() * p.Package.Price) / overallTotalPrice * 100
                })
                .ToList();

            return packageTotals;
        }
    }
}
