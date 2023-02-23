using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.Models
{
    public class PricingScheme
    {
        public int id { get; set; }

        public double perHour { get; set; }

        public double per1Day { get; set; }

        public double per2Days { get; set; }

        public double per3Days { get; set; }

        public double per4Days { get; set; }

        public double per5Days { get; set; }

        public double perExtraDay { get; set; }

        public int bicycleType_Id { get; set; }
        public BicycleType bicycleType { get; set; }
    }
}
