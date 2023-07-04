using X.Web.Models.FakePayments;
using X.Web.Services.Interfaces;

namespace X.Web.Services
{
    public class PaymentService : IPaymentService
    { private readonly HttpClient _httpClient;

        public PaymentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> ReceivePayment(PaymentInfoInput paymentInfoInput)
        {
            var response = await _httpClient.PostAsJsonAsync<PaymentInfoInput>("fakepayments",paymentInfoInput);
            return response.IsSuccessStatusCode;
        }
    }
}
