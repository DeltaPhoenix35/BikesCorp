using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.Models
{
    public class Coupon
    {
        [Display(Name = "Id")]
        public int id { get; set; }

        [Display(Name = "Coupon")]
        public int couponType_Id { get; set; }      
        public CouponType couponType { get; set; }

        [Display(Name = "Expiring Date")]
        public DateTime expiringDate { get; set; } 

        [Display(Name = "Customer")]
        public int customer_Id { get; set; }
        public Customer customer { get; set; }

        [Display(Name = "Applied Transaction")]
        public int? transaction_Id { get; set; }
        public Transaction transaction { get; set; }

        [Display(Name = "Applied Reservation")]
        public int? reservation_Id { get; set; }
        public Reservation reservation { get; set; }

        [Display(Name = "Applied")]
        public bool isUsed { get; set; }

        [Display(Name = "Expired")]
        public bool isExpired { get; set; }

        [Display(Name = "Reserved")]
        public bool isReserved { get; set; }

        [Display(Name = "Deleted")]
        public bool isDeleted { get; set; }

        
    }
}
