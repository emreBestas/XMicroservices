using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace X.Web.Models.Orders
{
    public class CheckOutInfoInput
    {
        [Display(Name = "Province")]
        public string Province { get; set; }

        [Display(Name = "District")]
        public string District { get; set; }

        [Display(Name = "Street")]
        public string Street { get; set; }

        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }

        [Display(Name = "address")]
        public string Line { get; set; }

        [Display(Name = "Card Name")]
        public string CardName { get; set; }

        [Display(Name = "Card Number")]
        public string CardNumber { get; set; }

        [Display(Name = "Expiration date (month/year)")]
        public string Expiration { get; set; }

        [Display(Name = "CVV/CVC2 numarası")]
        public string CVV { get; set; }
    }
}
