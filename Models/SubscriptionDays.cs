using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.Models
{
    public class SubscriptionDays
    {
        public int id { get; set; }

        public int subscription_Id { get; set; }

        public SubscriptionPlan subscription { get; set; }

        [Display(Name = "Day")]
        public DayOfWeek day { get; set; }

        [Display(Name = "Starting Time")]
        public DateTime startTime { get; set; }

        [Display(Name = "Ending Time")]
        public DateTime endTime { get; set; }
    }
}
