using X.Shared.DTOs;
using X.Shared.Services;
using X.Web.Models.FakePayments;
using X.Web.Models.Orders;
using X.Web.Services.Interfaces;

namespace X.Web.Services
{
    public class OrderService : IOrderService
    {
        private readonly IPaymentService _paymentService;
        private readonly IBasketService _basketService;
        private readonly HttpClient _httpClient;
        private readonly ISharedIdentityService _sharedIdentityService;

        public OrderService(IPaymentService paymentService, IBasketService basketService, HttpClient httpClient, ISharedIdentityService sharedIdentityService)
        {
            _paymentService = paymentService;
            _basketService = basketService;
            _httpClient = httpClient;
            _sharedIdentityService = sharedIdentityService;
        }

        public async Task<OrderCreatedViewModel> CreateOrder(CheckOutInfoInput checkOutInfoInput)
        {
            var basket = await _basketService.Get();
            var paymentInfoInput = new PaymentInfoInput()
            {
                CardName = checkOutInfoInput.CardName,
                CardNumber = checkOutInfoInput.CardNumber,
                CVV = checkOutInfoInput.CVV,
                Expiration = checkOutInfoInput.Expiration,
                TotalPrice = basket.TotalPrice
            };
            var responsePayment = await _paymentService.ReceivePayment(paymentInfoInput);
            if (!responsePayment) 
            { return new OrderCreatedViewModel() { Error = "Payment process failed", IsSuccessful = false }; }
            var orderCreateInput = new OrderCreateInput()
            {
                BuyerId = _sharedIdentityService.GetUserID,
                Address = new OrderAddressCreateInput
                {
                    Provice = checkOutInfoInput.Province,
                    District = checkOutInfoInput.District,
                    Line = checkOutInfoInput.Line,
                    Street = checkOutInfoInput.Street,
                    ZipCode = checkOutInfoInput.ZipCode
                }
            };
            basket.BasketItems.ForEach(item =>
            {
                var orderItem = new OrderItemViewModel { ProdcutId = item.CourseId, Price = item.GetCurrentPrice, PictureUrl = "", ProductName = item.CourseName };
                orderCreateInput.OrderItesms.Add(orderItem);

            });
            var response = await _httpClient.PostAsJsonAsync<OrderCreateInput>("orders", orderCreateInput);
            if (!response.IsSuccessStatusCode)
            { return new OrderCreatedViewModel() { Error = "Order could not be created", IsSuccessful = false }; }
            var orderCreatedViewModel= await response.Content.ReadFromJsonAsync<Response<OrderCreatedViewModel>>();
            orderCreatedViewModel.Data.IsSuccessful = true;
             await _basketService.Delete();
            return orderCreatedViewModel.Data;

        }

        public async Task<List<OrderViewModel>> GetOrder()
        {
            var response = await _httpClient.GetFromJsonAsync<Response<List<OrderViewModel>>>("orders");
            return response.Data;
        }

        public async Task<OrderSuspendViewModel> SuspendOrder(CheckOutInfoInput checkOutInfoInput)
        {
            var basket = await _basketService.Get();
            var orderCreateInput = new OrderCreateInput()
            {
                BuyerId = _sharedIdentityService.GetUserID,
                Address = new OrderAddressCreateInput
                {
                    Provice = checkOutInfoInput.Province,
                    District = checkOutInfoInput.District,
                    Line = checkOutInfoInput.Line,
                    Street = checkOutInfoInput.Street,
                    ZipCode = checkOutInfoInput.ZipCode
                }
            };
            basket.BasketItems.ForEach(item =>
            {
                var orderItem = new OrderItemViewModel { ProdcutId = item.CourseId, Price = item.GetCurrentPrice, PictureUrl = "", ProductName = item.CourseName };
                orderCreateInput.OrderItesms.Add(orderItem);

            });
           
            var paymentInfoInput = new PaymentInfoInput()
            {
                CardName = checkOutInfoInput.CardName,
                CardNumber = checkOutInfoInput.CardNumber,
                CVV = checkOutInfoInput.CVV,
                Expiration = checkOutInfoInput.Expiration,
                TotalPrice = basket.TotalPrice,
                Order=orderCreateInput
            };
            var responsePayment = await _paymentService.ReceivePayment(paymentInfoInput);
            if (!responsePayment)
            { return new OrderSuspendViewModel() { Error = "Payment process failed", IsSuccessful = false }; }
            await _basketService.Delete();
            return new OrderSuspendViewModel() { IsSuccessful = true };
        }
    }
}
