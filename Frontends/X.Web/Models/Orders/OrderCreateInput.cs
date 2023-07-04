namespace X.Web.Models.Orders
{
    public class OrderCreateInput
    {
        public OrderCreateInput()
        {
            OrderItesms = new List<OrderItemViewModel>();
        }
        public string BuyerId { get; set; }
        public List<OrderItemViewModel> OrderItesms { get; set; }
        public OrderAddressCreateInput Address { get; set; }
    }
}
