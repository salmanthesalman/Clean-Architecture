using Application.Features.Persistence;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Orders.Queries.GetOrdersList
{
    public class GetOrdersListQueryHandler : IRequestHandler<GetOrdersListQuery, List<OrderVM>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetOrdersListQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<List<OrderVM>> Handle(GetOrdersListQuery request, CancellationToken cancellationToken)
        {
          var OrderList = await _orderRepository.GetOrdersByUserName(request.UserName);
          return  _mapper.Map<List<OrderVM>>(OrderList);
        }
    }
}
