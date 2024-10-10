﻿using Exe.Starot.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Domain.Entities.Repositories
{
    public interface  IPackageQuestionRepository : IEFRepository<PackageQuestionEntity,PackageQuestionEntity>
    {
        Task<Dictionary<int, int>> GetBookingCountsForPackages(List<int> packageIds, CancellationToken cancellationToken);
    }
}
