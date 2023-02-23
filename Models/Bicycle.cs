using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.Models
{
    public class Bicycle
    {
        public enum BicycleSizes
        {
            S, M, L, XL
        }

        [Display(Name = "Id")]
        public int id { get; set; }

        public int bicycleType_Id { get; set; }

        public BicycleType bicycleType { get; set; }

        [Display(Name = "Size")]
        public BicycleSizes size { get; set; }


        [Display(Name = "Currently Rented")]
        public bool isCurrentlyRented { get; set; } = false;

        [Display(Name = "Currently Reserved")]
        public bool isReserved { get; set; } = false;

        [Display(Name = "Confirmed")]
        public bool isConfirmed { get; set; } = true;

        [DataType(DataType.Date)]
        [Display(Name = "Last CheckUp")]
        public DateTime? lastCheckupDate { get; set; }
        
        [Display(Name = "Times Rented")]
        public int timesRented { get; set; } = 0;

        [Display(Name = "Total Earnings")]
        public double earningsToDate { get; set; } = 0;

        [DataType(DataType.Date)]
        [Display(Name = "Aquisition Date")]
        public DateTime aquisutionDate { get; set; } = DateTime.Today;

        [Display(Name = "Purchase Price")]
        public double purchasePrice { get; set; }


        //[Display(Name = "Bicycle Contract Id")]
        //public int? bicycleContract_Id { get; set; }
        //public BicycleContract bicycleContract { get; set; }

        public List<Transaction> transactions { get; set; }
        public List<Reservation> reservations { get; set; }

    }
}
