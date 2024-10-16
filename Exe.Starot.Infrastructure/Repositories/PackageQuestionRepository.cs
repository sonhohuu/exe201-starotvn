using AutoMapper;
using Exe.Starot.Domain.Entities.Base;
using Exe.Starot.Domain.Entities.Repositories;
using Exe.Starot.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Infrastructure.Repositories
{
    public class PackageQuestionRepository : RepositoryBase<PackageQuestionEntity, PackageQuestionEntity, ApplicationDbContext>, IPackageQuestionRepository
    {
        public PackageQuestionRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {

        }

        public async Task<Dictionary<int, int>> GetBookingCountsForPackages(List<int> packageIds, CancellationToken cancellationToken)
        {
            return await _dbContext.Bookings
                                 .Where(b => packageIds.Contains(b.PackageId) && b.Status != "Đã hủy")
                                 .GroupBy(b => b.PackageId)
                                 .Select(g => new { PackageId = g.Key, Count = g.Count() })
                                 .ToDictionaryAsync(x => x.PackageId, x => x.Count, cancellationToken);
        }
    }
}
