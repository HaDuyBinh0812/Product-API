using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.API.Errors;
using Product.Core.Dto;
using Product.Core.Entities.Orders;
using Product.Core.Interface;
using System.Security.Claims;

namespace Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitWork _uow;
        private readonly IOrderServices _orderServices;
        public OrderController(IMapper mapper, IUnitWork uow, IOrderServices orderServices)
        {
            _mapper = mapper;
            _uow = uow;
            _orderServices = orderServices;
        }
        [HttpPost("create-order")]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var Email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type==ClaimTypes.Email)?.Value;
            var Address = _mapper.Map<AddressDto, ShipAddress>(orderDto.ShipToAddress);
            var order = await _orderServices.CreateOrderAsync(Email, orderDto.DeliveryMethodId, orderDto.BasketId, Address);
            if (orderDto is null) return BadRequest(new BaseCommonResponse(400, "Error While Creating Order"));
            return Ok(order);
        }

        [HttpGet("get-order-for-user")]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrderForUser()
        {
            var Email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type==ClaimTypes.Email)?.Value;
            var order = await _orderServices.GetOrdersForUserAsync(Email);
            var result = _mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDto>>(order);
            return Ok(result);
        }

        [HttpGet("get-order-by-id/{id}")]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrderById(int id)
        {
            var Email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type==ClaimTypes.Email)?.Value;
            var order = await _orderServices.GetOrderByIdAsync(id,Email);
            if (order is null) return NotFound(new BaseCommonResponse(404));
            var result = _mapper.Map<Order, OrderToReturnDto>(order);
            return Ok(result);
        }

        [HttpGet("get-delivery-method")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethod()
        {
            return Ok(await _orderServices.GetDeliveryMethodsAsync());
        }
    }
}
