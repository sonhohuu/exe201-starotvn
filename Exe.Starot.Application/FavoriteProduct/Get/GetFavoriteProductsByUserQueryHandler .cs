using AutoMapper;
using Exe.Starot.Application.Common.Interfaces;
using Exe.Starot.Application.Common.Mappings;
using Exe.Starot.Domain.Entities.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Exe.Starot.Application.FavoriteProduct.Get
{
    public class GetFavoriteProductsByUserQueryHandler : IRequestHandler<GetFavoriteProductsByUserQuery, IEnumerable<FavoriteProductDto>>
    {
        private readonly IFavoriteProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetFavoriteProductsByUserQueryHandler(IFavoriteProductRepository repository, IMapper mapper, ICurrentUserService currentUserService)
        {
            _repository = repository;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<IEnumerable<FavoriteProductDto>> Handle(GetFavoriteProductsByUserQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            if (userId == null)
            {
                throw new UnauthorizedAccessException("User not login");
            }

            // Fetch favorite products for the provided UserId
            var favoriteProducts = await _repository.FindAllAsync(f => f.UserId == _currentUserService.UserId && f.IsFavorite == true && !f.DeletedDay.HasValue, cancellationToken);

            // If no favorite products found, return empty list
            if (favoriteProducts == null || !favoriteProducts.Any())
            {
                return new List<FavoriteProductDto>();
            }

            return favoriteProducts.MapToFavoriteProductDtoList(_mapper);
        }
    }
}
