using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.Models
{
    public class SubscriptionPlan
    {
        [Display(Name = "Subscription Plan Id")]
        public int id { get; set; }


        [Display(Name = "Name")]
        public string name { get; set; }

        public List<SubscriptionDays> subscriptionDays { get; set; }

        [NotMapped]
        public List<bool> subscriptionDaysBool { get; set; }


        [Display(Name = "Price")]
        public decimal price { get; set; }

        [Display(Name = "Premium")]
        public bool isPremium { get; set; }

        [Display(Name = "Is Deleted")]
        public bool isDeleted { get; set; }

        [Display(Name = "Is Active")]
        public bool isActive { get; set; }

    }
}
