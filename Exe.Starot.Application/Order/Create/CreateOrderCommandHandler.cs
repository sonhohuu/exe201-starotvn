using AutoMapper;
using Exe.Starot.Application.Common.Interfaces;
using Exe.Starot.Domain.Common.Exceptions;
using Exe.Starot.Domain.Entities.Base;
using Exe.Starot.Domain.Entities.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.Order.Create
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, string>
    {
        private readonly OrderService _orderService;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserRepository _userRepository;

        public CreateOrderCommandHandler(OrderService orderService,IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository, IProductRepository productRepository, IMapper mapper, ICurrentUserService currentUserService, IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _productRepository = productRepository;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _userRepository = userRepository;
            _orderService = orderService;   
        }

        public async Task<string> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
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

            // Calculate the total order price before creating the order
            decimal orderTotal = 0;

            foreach (var item in request.Products)
            {
                var product = await _productRepository.FindAsync(x => x.ID == item.ProductID && !x.DeletedDay.HasValue, cancellationToken);

                if (product == null)
                {
                    throw new NotFoundException($"Product with ID {item.ProductID} is not found or deleted");
                }

                orderTotal += item.Quantity * product.Price;
            }

            // Handle PaymentMethod logic before creating the order
            if (request.PaymentMethod == "Ví")
            {
                // If payment method is 'Ví', check if the user's balance is sufficient
                if (userExist.Balance < orderTotal)
                {
                    // If balance is insufficient, return a failure message
                    throw new ArgumentException("Insufficient balance.");
                }

                // Deduct the user's balance
                userExist.Balance -= orderTotal;
                await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            }
            else if (request.PaymentMethod != "Tiền Mặt")
            {
                throw new ValidationException("Invalid Payment Method.");
            }


            // Now that balance is checked and sufficient, create the order
            var order = new OrderEntity
            {
                UserId = userExist.ID,
                Code = GenerateOrderCode(),
                CreatedBy = _currentUserService.UserId,
                CreatedDate = DateTime.UtcNow,
                Address = request.Address,  
                OrderDate = DateTime.UtcNow.AddHours(7).ToString("dd/MM/yyyy"),
                OrderTime = DateTime.UtcNow.AddHours(7).ToString("HH:mm"),
                Total = orderTotal,
                Status = "Đang xác nhận đơn hàng", // Set status based on payment method
                PaymentMethod = request.PaymentMethod
            };

            // Add the order to the repository
            _orderRepository.Add(order);
            await _orderRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            var orderID = order.ID;

            // Create order details after creating the order
            foreach (var item in request.Products)
            {
                var product = await _productRepository.FindAsync(x => x.ID == item.ProductID && !x.DeletedDay.HasValue, cancellationToken);

                decimal productPrice = item.Quantity * product.Price;

                var orderDetail = new OrderDetailEntity
                {
                    OrderId = order.ID,
                    ProductId = item.ProductID,
                    Amount = item.Quantity,
                    UnitPrice = product.Price,
                    Price = productPrice,
                    CreatedDate = DateTime.UtcNow,
                    Status = true
                };

                _orderDetailRepository.Add(orderDetail);
                await _orderDetailRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            }

            // Notify about the new order
            await _orderService.NotifyNewOrder(orderID.ToString());

            return "Create Order Successful!";
        }


        private string GenerateOrderCode()
        {
            // Example format: ORD-20240925-XXXX
            // ORD: Fixed prefix indicating the order.
            // 20240925: Current date in YYYYMMDD format.
            // XXXX: Random alphanumeric string to ensure uniqueness.

            var datePart = DateTime.UtcNow.ToString("yyyyMMdd");
            var randomPart = Guid.NewGuid().ToString().Substring(0, 4).ToUpper(); // You can customize the length if needed.

            return $"ORD-{datePart}-{randomPart}";
        }
    }
}
