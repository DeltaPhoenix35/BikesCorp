using BikesTest.Interfaces;
using BikesTest.Models;
using BikesTest.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.ServiceExtentions
{

    public static class BicycleServiceExtensions
    {
        public static void IncrementTimesRented(this IBicycleService<Bicycle> _bService, Bicycle bike)
        {
            bike.timesRented++;
        }
        public static void DecrementTimesRented(this IBicycleService<Bicycle> _bService, Bicycle bike)
        {
            bike.timesRented--;
        }
        public static void SetIsCurrentlyRentedTrue(this IBicycleService<Bicycle> _bService, Bicycle bike)
        {
            bike.isCurrentlyRented = true;
        }
        public static void SetIsCurrentlyRentedFalse(this IBicycleService<Bicycle> _bService, Bicycle bike)
        {
            bike.isCurrentlyRented = false;
        }
        public static void IncreaseEarningsToDate(this IBicycleService<Bicycle> _bService, Bicycle bike, double transactionCost)
        {
            bike.earningsToDate += transactionCost;
        }
        
        public static Bicycle UpdateIsReserved(this IBicycleService<Bicycle> _bService, Bicycle bike)
        {
            bike.isReserved = false;
            if (bike.reservations.Any(o => o.isDeleted == false))
            {
                bike.isReserved = true;
            }
            return bike;
        }
        
    }
}
