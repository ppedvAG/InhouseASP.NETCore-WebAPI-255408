using BusinessLogic.Contracts;
using BusinessLogic.Models;
using Microsoft.AspNetCore.Mvc;

namespace RecipeDbApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IRecipeService _recipeService;

        public OrdersController(IOrderService orderService, IRecipeService recipeService)
        {
            _orderService = orderService;
            _recipeService = recipeService;
        }

        // GET: api/Orders?userName=Guest
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders([FromQuery] string userName)
        {
            var orders = await _orderService.GetOrders(userName);
            return Ok(orders);
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _orderService.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        // POST: api/Orders
        [HttpPost("{userName:alpha}/{recipeId:long}")]
        public async Task<ActionResult<Order>> PostOrder(string userName, long recipeId, [FromQuery] int quantity = 1)
        {
            var recipe = await _recipeService.GetById(recipeId);
            if (recipe == null)
                return NotFound();

            await _orderService.AddOrderItem(userName, recipe, quantity);
            var order = await _orderService.CurrentOrder(userName);
            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
        }

        // PUT: api/Orders/5/confirm
        [HttpPut("{userName:alpha}/confirm")]
        public async Task<IActionResult> ConfirmOrder(string userName)
        {
            var result = await _orderService.ConfirmOrder(userName);
            if (!result)
            {
                return BadRequest();
            }
            return NoContent();
        }

        // PUT: api/Orders/5/finish
        [HttpPut("{userName:alpha}/finish")]
        public async Task<IActionResult> FinishOrder([FromQuery] string userName, [FromQuery] float rating)
        {
            var result = await _orderService.FinishOrder(userName, rating);
            if (!result)
            {
                return BadRequest();
            }
            return NoContent();
        }

        // GET: api/Orders/pending?userName=Guest
        [HttpGet("pending")]
        public async Task<ActionResult<Order>> GetPendingOrder([FromQuery] string userName)
        {
            var order = await _orderService.GetPendingOrderByUserId(userName);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }
    }
}
