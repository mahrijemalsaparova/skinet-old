using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities.OrderAggregate;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    // user should outhorize first before seeing orders
    [Authorize]
    public class OrdersController : BaseApiController
    {
        private readonly OrderService _orderService;
        private readonly IMapper _mapper;
        public OrdersController(OrderService orderService, IMapper mapper)
        {
            _mapper = mapper;
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();

            var address =  _mapper.Map<AddressDto, Address>(orderDto.ShipToAddress);

            var order = await _orderService.CreatOrderAsync(email, orderDto.DeliveryMethodId, orderDto.BasketId, address);

            if (order == null)  return BadRequest(new ApiResponse(400, "Problem creating order"));

            return Ok(order);
        }


    }
}