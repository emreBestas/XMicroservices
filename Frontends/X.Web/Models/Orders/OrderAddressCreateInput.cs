namespace X.Web.Models.Orders
{
    public class OrderAddressCreateInput
    {
        public string Provice { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string Line { get; set; }
    }
}
