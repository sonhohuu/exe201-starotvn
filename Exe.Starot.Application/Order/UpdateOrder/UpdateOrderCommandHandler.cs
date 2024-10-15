using Exe.Starot.Application.Common.Interfaces;
using Exe.Starot.Domain.Common.Exceptions;
using Exe.Starot.Domain.Entities.Repositories;
using Exe.Starot.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.Order.UpdateOrder
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, string>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserRepository _userRepository;
        public UpdateOrderCommandHandler(IOrderRepository orderRepository, ICurrentUserService currentUserService, IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _currentUserService = currentUserService;
            _userRepository = userRepository;
        }

        public async Task<string> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var user = _currentUserService.UserId;
            if (user == null)
            {
                throw new UnauthorizedException("User not login");
            }

            var userExist = await _userRepository.FindAsync(x => x.ID == user && x.DeletedDay == null, cancellationToken);

            if (userExist is null)
            {
                throw new NotFoundException("User does not exist");
            }

            var orderExist = await _orderRepository.FindAsync(x => x.ID == request.ID && !x.DeletedDay.HasValue, cancellationToken);

            if (orderExist is null)
            {
                throw new NotFoundException("Order does not exist");
            }

            if (orderExist.Status == "Đã hủy")
            {
                throw new ArgumentException("Không thể hủy đơn hàng đã hủy");
            }

            if (orderExist.Status == "Đã giao" && request.Status == "Đã hủy")
            {
                throw new ArgumentException("Không thể hủy đơn hàng đã giao");
            } else if(request.Status == "Đã Hủy")
            {
                var userOrder = await _userRepository.FindAsync(x => x.ID == orderExist.UserId && !x.DeletedDay.HasValue, cancellationToken);
                if (userOrder != null)
                {
                    userOrder.Balance += orderExist.Total;
                    userOrder.LastUpdated = DateTime.UtcNow;
                    _userRepository.Update(userOrder);
                    await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
                }
            }

            orderExist.Status = request.Status;
            orderExist.UpdatedBy = _currentUserService.UserId;
            orderExist.LastUpdated = DateTime.UtcNow;
            _orderRepository.Update(orderExist);

            return await _orderRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Update Success!" : "Update Fail!";
        }
    }
}
