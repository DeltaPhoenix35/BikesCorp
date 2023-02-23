using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.Models
{
    public class BicycleType
    {
        public int id { get; set; }

        public string typeName { get; set; }

        public string description { get; set; }

        //pictures

        [Display(Name = "Lease Price")]
        public int pricingScheme_Id { get; set; }
        public PricingScheme pricing { get; set; }

        public int reduction { get; set; } = 0;

        public List<Bicycle> bicycles { get; set; }

        public List<BicycleContract> bicycleContracts { get; set; }
    }
}
