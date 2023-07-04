namespace X.Services.Order.Application.DTOs
{
    public class OrderItemDto
    {
        public string ProdcutId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
        public Decimal Price { get; set; }
    }
}
