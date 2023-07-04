using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace X.Web.Models.Catalogs
{
    public class FeatureViewModel
    {
        [Display(Name = "Course Duration")]
        public int Duration { get; set; }
    }
}
