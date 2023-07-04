using X.Web.Models.Orders;

namespace X.Web.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderCreatedViewModel> CreateOrder(CheckOutInfoInput checkOutInfoInput);

        Task<OrderSuspendViewModel> SuspendOrder(CheckOutInfoInput checkOutInfoInput);

        Task<List<OrderViewModel>> GetOrder();
    }
}
