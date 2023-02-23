using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.Models
{
    public class Transaction
    {

        [Display(Name = "Id")]
        public int id { get; set; }

        [Display(Name = "Transaction Number")]
        public string transactionNum { get; set; }

        [Display(Name = "Bicycle Id")]
        [Range(0, int.MaxValue, ErrorMessage = "Invalid Bicycle Id")]
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


        [Display(Name = "Admin Id")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid Admin Id")]
        public int admin_Id { get; set; }
        public Admin admin { get; set; }


        [Display(Name = "Rental Date")]
        public DateTime rentalDate { get; set; } = DateTime.Today;

        [Display(Name = "Expected Return Date")]
        public DateTime expectedReturnDate { get; set; }

        [Display(Name = "Return Date")]
        public DateTime? returnDate { get; set; }


        public int? coupon_Id { get; set; }
        public Coupon coupon { get; set; }


        [Display(Name = "Duration Of Transaction")]
        [Range(0, int.MaxValue)]
        [Column(TypeName = "decimal(30,12)")]

        public decimal? durationOfTransaction { get; set; } //redundency


        [Display(Name = "Price")]
        public double? costOfTransaction { get; set; } //redundency
        
        [NotMapped]
        public List<Location> locations { get; set; } //gps locations

        public int? subscription_Id { get; set; }
        public Subscription subscription { get; set; }

        public bool isDeleted { get; set; }
    }
}
