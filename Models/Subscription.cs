using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.Models
{
    public class Subscription
    {
        [Display(Name = "Subscription Name")]
        public int id { get; set; }

        public int subscriptionPlan_Id { get; set; }
        public SubscriptionPlan subscriptionPlan { get; set; }

        public int bicycleType_Id { get; set; }
        public BicycleType bicycleType { get; set; }

        public int customer_Id { get; set; }
        public Customer customer { get; set; }

        public List<Transaction> transactions { get; set; }
        public List<Reservation> reservations { get; set; }

        [Display(Name = "Date of Creation")]
        public DateTime creationDate { get; set; }

        [Display(Name = "Active")]
        public bool isActive { get; set; }

        [Display(Name = "Deleted")]
        public bool isDeleted { get; set; }
    }
}
