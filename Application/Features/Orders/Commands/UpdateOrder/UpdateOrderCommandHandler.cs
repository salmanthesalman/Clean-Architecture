using Application.Exceptions;
using Application.Features.Infrastructure;
using Application.Features.Persistence;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IEmailServices _emailServices;
        private readonly ILogger<UpdateOrderCommandHandler> _logger;

        public UpdateOrderCommandHandler(
            IOrderRepository orderRepository,
            IMapper mapper,
            IEmailServices emailServices,
            ILogger<UpdateOrderCommandHandler> logger
            )
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _emailServices = emailServices;
            _logger = logger;
        }
        public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
           var orderFromDB = await _orderRepository.GetByIdAsync(request.Id);
            if (orderFromDB ==null)
            {
                _logger.LogInformation("Order not exist in Database");
                throw new NotFoundException(nameof(Order), request.Id);
            }
            _mapper.Map(request, orderFromDB, typeof(UpdateOrderCommand), typeof(Order));
            await _orderRepository.UpdateAsync(orderFromDB);
            return Unit.Value;

        }
    }
}
