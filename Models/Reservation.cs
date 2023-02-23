using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.Models
{
    public class Reservation
    {
        [Display(Name = "Id")]
        public int id { get; set; }

        [Display(Name = "Bicycle Id")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid Bicycle Id")]
        public int bicycle_Id { get; set; }
        public Bicycle bicycle { get; set; }

        [Display(Name = "Bicycle Type")]
        [NotMapped]
        public int bicycleType_Id { get; set; }
        [NotMapped]
        public BicycleType bicycleType { get; set; }

        [Display(Name = "Customer Id")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid Customer Id")]
        public int customer_Id { get; set; }
        public Customer customer { get; set; }

        public int? coupon_Id { get; set; }
        public Coupon coupon { get; set; }

        [Display(Name = "Reservation Date")]
        public DateTime reservationDate { get; set; }

        [Display(Name = "Expected Return Date")]
        public DateTime expectedReturnDate { get; set; }

        public int? subscription_Id { get; set; }
        public Subscription subscription { get; set; }

        public bool isDeleted { get; set; } = false;
    }
}
