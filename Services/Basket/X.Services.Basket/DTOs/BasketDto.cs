namespace X.Services.Basket.DTOs
{
    public class BasketDto
    {
        public string UserId { get; set; }
        public string DiscountCode { get; set; }

        public List<BasketItemDto> basketItems { get; set; }
        public decimal TotalPrize { get => basketItems.Sum(x => x.Price * x.Quantity); }
    }
}
