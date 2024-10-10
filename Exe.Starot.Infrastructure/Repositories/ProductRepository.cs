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
    public class ProductRepository : RepositoryBase<ProductEntity, ProductEntity, ApplicationDbContext>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {

        }

        public async Task<Dictionary<string, int>> GetTotalAmountForProducts(List<string> productIds, CancellationToken cancellationToken)
        {
            if (productIds == null || !productIds.Any())
            {
                return new Dictionary<string, int>();
            }

            return await _dbContext.OrderDetails
                .Where(od => productIds.Contains(od.ProductId))
                .GroupBy(od => od.ProductId)
                .Select(g => new { ProductId = g.Key, TotalAmount = g.Sum(od => od.Amount) }) // Sum up the Amount for each product
                .ToDictionaryAsync(x => x.ProductId, x => x.TotalAmount, cancellationToken);
        }
    }
}
