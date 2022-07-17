using Application.Features.Infrastructure;
using Application.Features.Models;
using Application.Features.Persistence;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Orders.Commands
{
    public class CheckOutOrderCommandHandler : IRequestHandler<CheckOutOrderCommand, int>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IEmailServices _emailServices;
        private readonly ILogger<CheckOutOrderCommandHandler> _logger;

        public CheckOutOrderCommandHandler(
            IOrderRepository orderRepository,
            IMapper mapper,
            IEmailServices emailServices,
            ILogger<CheckOutOrderCommandHandler> logger
            )
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _emailServices = emailServices;
            _logger = logger;
        }

        public async Task<int> Handle(CheckOutOrderCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = _mapper.Map<Order>(request);
           var newOrder =  await _orderRepository.AddAsync(orderEntity);
            _logger.LogInformation("Order created Successfull with id" + newOrder.Id);
            await SendMail(newOrder);
            return request.Id;
        }
        private async Task SendMail(Order order)
        {
            var email = new Email() { To = order.EmailAddress, Body = $"Order was created.", Subject = "Order was created" };

            try
            {
                await _emailServices.SendEmail(email);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Order {order.Id} failed due to an error with the mail service: {ex.Message}");
            }
        }
    }
}
