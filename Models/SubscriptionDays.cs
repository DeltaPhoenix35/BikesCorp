using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.Models
{
    public class SubscriptionDays
    {
        public int id { get; set; }

        public int subscription_Id { get; set; }

        public SubscriptionPlan subscription { get; set; }

        public DayOfWeek day { get; set; }

        public DateTime startTime { get; set; }

        public DateTime endTime { get; set; }
    }
}
