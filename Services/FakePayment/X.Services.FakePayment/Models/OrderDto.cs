namespace X.Services.FakePayment.Models
{
    public class OrderDto
    {
        public OrderDto()
        {
            OrderItesms = new List<OrderItemDto>();
        }
        public string BuyerId { get; set; }
        public List<OrderItemDto> OrderItesms { get; set; }
        public AddressDto Address { get; set; }
    }
    public class AddressDto
    {
        public string Provice { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string Line { get; set; }
    }
    public class OrderItemDto
    {
        public string ProdcutId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
        public Decimal Price { get; set; }
    }
}
