using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.Models
{
    public class SubscriptionPlan
    {
        public int id { get; set; }

        public string name { get; set; }

        public List<SubscriptionDays> subscriptionDays { get; set; }

        [NotMapped]
        public List<bool> subscriptionDaysBool { get; set; }

        public decimal price { get; set; }

        public bool isDeleted { get; set; }

        public bool isActive { get; set; }

    }
}
