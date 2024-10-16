using Exe.Starot.Application.Common.Interfaces;
using Exe.Starot.Domain.Common.Exceptions;
using Exe.Starot.Domain.Entities.Base;
using Exe.Starot.Domain.Entities.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.FavoriteProduct.Create
{
    public class AddFavoriteProductCommandHandler : IRequestHandler<AddFavoriteProductCommand, string>
    {
        private readonly IFavoriteProductRepository _favoriteProductRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICurrentUserService _currentUserService;
        public AddFavoriteProductCommandHandler(IFavoriteProductRepository favoriteProductRepository, IProductRepository productRepository, IUserRepository userRepository, ICurrentUserService currentUserService)
        {
            _favoriteProductRepository = favoriteProductRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
            _currentUserService = currentUserService;
        }

        public async Task<string> Handle(AddFavoriteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.FindAsync(p => p.ID == request.ProductId && !p.DeletedDay.HasValue, cancellationToken);
            if (product == null)
            {
                throw new NotFoundException("Product not found.");
            }
            var user = await _userRepository.FindAsync(u => u.ID == _currentUserService.UserId && !u.DeletedDay.HasValue, cancellationToken);
            if (user == null)
            {
                throw new UnauthorizedException("User not login.");
            }

            //find favorite product
            var favoriteProduct = await _favoriteProductRepository.FindAsync(fp =>
                fp.ProductId == request.ProductId && fp.UserId == user.ID, cancellationToken);

            if (favoriteProduct == null)
            {
                var newFavorite = new FavoriteProductEntity
                {
                    ProductId = request.ProductId,
                    UserId = user.ID,
                    IsFavorite = true
                };

                _favoriteProductRepository.Add(newFavorite);
            }
            else
            {
                favoriteProduct.IsFavorite = !favoriteProduct.IsFavorite;
            }
            return await _favoriteProductRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Update success" : "Update failed";
        }
    }
}
