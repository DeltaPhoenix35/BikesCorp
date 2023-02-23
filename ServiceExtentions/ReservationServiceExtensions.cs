using BikesTest.Exceptions;
using BikesTest.Interfaces;
using BikesTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.ServiceExtentions
{
    public static class ReservationServiceExtensions
    {
        public static void ReservationVerifications(this IReservationService<Reservation> _rService,
                                                    Reservation row, Customer customer, Bicycle bike)
        {
            //check if bike exists
            if (bike == null)
            {
                throw new BikeDoesntExistException("This Bicycle doesn't exist");
            }
            //check if customer exists
            else if (customer == null)
            {
                throw new CustomerDoesntExistException("This Customer doesn't exist");
            }
            //all the dates must be after "now"
            if (row.reservationDate < DateTime.Now)
            {
                throw new InvalidDateException("Reservation must be taken sometime in the future");
            }
            else if (row.expectedReturnDate < row.reservationDate)
            {
                throw new InvalidDateException("Reservation cannot have a return date before the start date");
            }
            //check if the bike is available during the period of the reservation (no reservation overlapping)
            
            if (bike.isCurrentlyRented == true)
            {
                if (bike.transactions.Last().expectedReturnDate > row.reservationDate)
                {
                    throw new CurrentlyRentException("Bicycle is expected to be rented untill " + bike.transactions.Last().expectedReturnDate.ToString("G"));
                }
            }
            else if (bike.isReserved == true)
            {
                Reservation reservation = _rService.GetByBicycleId(bike.id);

                if ((row.reservationDate < reservation.reservationDate && row.expectedReturnDate > reservation.reservationDate) ||
                    (row.reservationDate > reservation.reservationDate && row.expectedReturnDate < reservation.expectedReturnDate) ||
                    (row.reservationDate < reservation.expectedReturnDate && row.expectedReturnDate > reservation.expectedReturnDate))
                {
                    throw new CurrentlyReservedException("Bicycle is reserved from " + reservation.reservationDate.ToString("G") +
                                                        " untill " + reservation.expectedReturnDate.ToString("G"));
                }
            }
            //check if the customer is has no other ***reservation*** / transaction going in the duration of the reservation
            if (customer.isCurrentlyBiking)
            {
                if (customer.transactions.Last().expectedReturnDate > row.reservationDate)
                {
                    throw new CurrentlyBikingException("Customer is already biking untill " + customer.transactions.Last().expectedReturnDate.ToString("G"));
                }
            }
        }

        public static void CheckCustomerReservationMissmatch(this IReservationService<Reservation> _rService,
                                                             Reservation reservation, int customerId)
        {
            if (customerId != reservation.customer.user_id)
            {
                throw new ReservationCustomerMissmatchException("Customer did not make this reservation");
            }
        }

    }
}
