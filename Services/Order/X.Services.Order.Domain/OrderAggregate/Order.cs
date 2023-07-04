using X.Services.Order.Domain.Core;

namespace X.Services.Order.Domain.OrderAggregate
{
    public class Order : Entity, IAggragateRoot
    {
        public DateTime CreatedDate { get; private set; }
        public Address Adress { get; private set; }
        public string BuyerId { get; private set; }
        private readonly List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;
        public Order()
        {

        }
        public Order(string buyerID, Address address)
        {
            _orderItems = new List<OrderItem>();
            CreatedDate = DateTime.Now;
            BuyerId = buyerID;
            Adress = address;
        }
        public void AddOrderItem(string productId, string productName, string pictureUrl, decimal price)
        {
            var existProduct = _orderItems.Any(x => x.ProdcutId == productId);
            if (!existProduct) { var newOrderItem = new OrderItem(productId, productName, pictureUrl, price); _orderItems.Add(newOrderItem); }

        }

        public decimal GetTotalPrize => _orderItems.Sum(x => x.Price);
    }
}
