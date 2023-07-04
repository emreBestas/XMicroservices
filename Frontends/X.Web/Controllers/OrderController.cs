using Microsoft.AspNetCore.Mvc;
using X.Web.Models.Orders;
using X.Web.Services.Interfaces;

namespace X.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly IOrderService _orderService;

        public OrderController(IBasketService basketService, IOrderService orderService)
        {
            _basketService = basketService;
            _orderService = orderService;
        }

        public async Task<IActionResult> Checkout()
        {
            var basket = await _basketService.Get();
            ViewBag.basket = basket;
            return View(new CheckOutInfoInput());
        }
        [HttpPost]
        public async Task<IActionResult> Checkout(CheckOutInfoInput checkOutInfoInput)
        {
            //1-> synchronous communication
            //var orderStatus = await _orderService.CreateOrder(checkOutInfoInput);
            //2-> asynchronous communication
            var orderSuspend = await _orderService.SuspendOrder(checkOutInfoInput);



            var basket = await _basketService.Get();
            ViewBag.basket = basket;
           
            if (!orderSuspend.IsSuccessful) { ViewBag.error = orderSuspend.Error; return View(); }
            //1-> synchronous communication
            //return RedirectToAction(nameof(SuccessfulCheckout),new {orderId= orderSuspend.OrderId});
            //2-> asynchronous communication
            return RedirectToAction(nameof(SuccessfulCheckout), new { orderId = new Random().Next(1,1000) });
        }
        public IActionResult SuccessfulCheckout(int orderId) 
        {
            ViewBag.orderId = orderId;
            return View();
        }

        public async Task< IActionResult> CheckoutHistory()
        {
            return View( await _orderService.GetOrder());
        }
    }
}
