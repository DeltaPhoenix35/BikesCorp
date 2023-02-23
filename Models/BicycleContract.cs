using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static BikesTest.Models.Bicycle;

namespace BikesTest.Models
{
    public class BicycleContract
    {
        [Display(Name = "Id")]
        public int id { get; set; }

        [Display(Name = "Customer Id")]
        public int customer_Id { get; set; }
        public Customer customer { get; set; }

        public int? bicycle_Id { get; set; }
        public Bicycle bicycle { get; set; }

        [Display(Name = "Additional Information")]
        public string moreInfo { get; set; }

        [Display(Name = "Denial Information")]
        public string refusalInformation { get; set; }

        [Display(Name = "Active")]
        public bool isActive { get; set; } = false;

        [Display(Name = "Denied")]
        public bool isDenied { get; set; } = false;
    }
}
