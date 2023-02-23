using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.Models
{
    public class CouponType
    {
        [Display(Name = "Id")]
        public int id { get; set; }

        [Display(Name = "Coupon")]
        public int value { get; set; }

        [Display(Name = "Expiring Period (weeks)")]
        public int weeksToExpire { get; set; }

        [Display(Name = "Points To Redeem")]
        public int pointsToRedeem { get; set; }

        public bool isDeleted { get; set; } = false;

        public List<Coupon> coupons { get; set; }
    }
}
