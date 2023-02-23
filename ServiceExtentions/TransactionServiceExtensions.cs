using BikesTest.Exceptions;
using BikesTest.Interfaces;
using BikesTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.ServiceExtentions
{
    public static class TransactionServiceExtensions
    {
        
        public static void SetTransactionDuration(this ITransactionService<Transaction> _tService, 
                                                  Transaction transaction)
        {
            transaction.durationOfTransaction = (decimal)(transaction.returnDate - transaction.rentalDate).GetValueOrDefault().TotalHours;
        }

        public static double SelectPricing(Transaction transaction, Bicycle bicycle, float totalReduction)
        {
            int duration = (int)(transaction.expectedReturnDate - transaction.rentalDate).TotalHours;

            if (duration < 24)
                return Math.Max(1, bicycle.bicycleType.pricing.perHour) * duration * totalReduction;
            else
            {
                duration = (int)(transaction.expectedReturnDate - transaction.rentalDate).TotalDays;
                if (duration == 1)
                    return bicycle.bicycleType.pricing.per1Day * totalReduction;
                else if(duration == 2)
                    return bicycle.bicycleType.pricing.per2Days * totalReduction;
                else if (duration == 3)
                    return bicycle.bicycleType.pricing.per3Days * totalReduction;
                else if (duration == 4)
                    return bicycle.bicycleType.pricing.per4Days * totalReduction;
                else if (duration == 5)
                    return bicycle.bicycleType.pricing.per5Days * totalReduction;
                else
                    return (bicycle.bicycleType.pricing.per5Days + ((int)duration - 5) * bicycle.bicycleType.pricing.perExtraDay) * totalReduction;      
            }    
        }

        public static void CalculateTransactionCost(this ITransactionService<Transaction> _tService, 
                                                    Transaction transaction, Bicycle bike)
        {
            float coupon = 1;
            if (transaction.coupon != null)
                coupon = 1 - (float)transaction.coupon.couponType.value / 100;

            float reduction = 1 - ((float)bike.bicycleType.reduction / 100);

            transaction.costOfTransaction = SelectPricing(transaction, bike, reduction * coupon);
        }

        public static void SetTransactionDeleted(this ITransactionService<Transaction> _tService,
                                                 Transaction transaction)
        {
            transaction.isDeleted = true;
        }
        public static void SetTransactionNotDeleted(this ITransactionService<Transaction> _tService,
                                                 Transaction transaction)
        {
            transaction.isDeleted = false;
        }
    }
}
