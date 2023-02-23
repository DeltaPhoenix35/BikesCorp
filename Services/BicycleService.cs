using BikesTest.Interfaces;
using BikesTest.Models;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.Services
{
    public class BicycleService : IBicycleService<Bicycle>
    {
        private readonly Context _db;

        public BicycleService(Context db)
        {
            _db = db;
        }

        public List<Bicycle> GetAll()
        {
            return _db.Bicycles.AsNoTracking()
                               .Include(o => o.bicycleType)
                               .ToList<Bicycle>();
        }

        public List<Bicycle> GetAllAvailable()
        {
            return _db.Bicycles.AsNoTracking()
                               .Where(o => o.isCurrentlyRented == false && o.isConfirmed == true)
                               .ToList<Bicycle>();
        }

        public Bicycle GetById(int? id)
        {
            return _db.Bicycles.AsNoTracking()
                               .Where(o => o.id == id)
                               .Include(o => o.bicycleType).ThenInclude(t => t.pricing)
                               .Include(o => o.transactions)
                               .Include(o => o.reservations)
                               .AsNoTracking()
                               .SingleOrDefault();
        }

        public Bicycle GetFirstAvailable(DateTime start, DateTime end)
        {
            List<Bicycle> bikes = _db.Bicycles.AsNoTracking()
                                              .Include(o => o.transactions)
                                              .Include(o => o.reservations)
                                              .Where(o => o.isConfirmed == true)
                                              .ToList();

            if (bikes.Any(o => o.transactions.Count == 0 && o.reservations.Count == 0))
            {
                return bikes.Where(o => o.transactions.Count == 0 && o.reservations.Count == 0).FirstOrDefault();
            }
            else if (bikes.Any(o => o.transactions.Count == 0 && o.reservations.Count != 0))
            {
                List<Bicycle> bicycles = bikes.Where(o => o.transactions.Count == 0 && o.reservations.Count != 0).ToList();

                var bike = bicycles.Where(o => o.reservations.Any(t => ((start <= t.reservationDate && end >= t.reservationDate) ||
                                                                        (start >= t.reservationDate && end <= t.expectedReturnDate) ||
                                                                        (start <= t.expectedReturnDate && end >= t.expectedReturnDate)) &&
                                                                         t.isDeleted == false) == false)
                                   .FirstOrDefault();

                return bike;
            }
            else if (bikes.Any(o => o.transactions.Count != 0 && o.reservations.Count == 0))
            {
                List<Bicycle> bicycles = bikes.Where(o => o.transactions.Count != 0 && o.reservations.Count == 0).ToList();

                var bike = bicycles.Where(o => o.transactions.Any(t => ((start <= t.rentalDate && end >= t.rentalDate) ||
                                                                        (start >= t.rentalDate && end <= t.expectedReturnDate) ||
                                                                        (start <= t.expectedReturnDate && end >= t.expectedReturnDate)) &&
                                                                         t.isDeleted == false) == false)
                                   .FirstOrDefault();

                return bike;
            }
            else
            {
                List<Bicycle> bicycles = bikes.Where(o => o.transactions.Count != 0 && o.reservations.Count != 0).ToList();

                var bike = bicycles.Where(o => (o.transactions.Any(t => ((start <= t.rentalDate && end >= t.rentalDate) ||
                                                                        (start >= t.rentalDate && end <= t.expectedReturnDate) ||
                                                                        (start <= t.expectedReturnDate && end >= t.expectedReturnDate)) &&
                                                                         t.isDeleted == false) == false) &&
                                               (o.reservations.Any(t => ((start <= t.reservationDate && end >= t.reservationDate) ||
                                                                        (start >= t.reservationDate && end <= t.expectedReturnDate) ||
                                                                        (start <= t.expectedReturnDate && end >= t.expectedReturnDate)) &&
                                                                         t.isDeleted == false) == false))
                                   .FirstOrDefault();

                return bike;
            }
        }

        public Bicycle GetFirstAvailableByTypeId(int typeId, DateTime start, DateTime end)
        {
            List<Bicycle> bikes = _db.Bicycles.AsNoTracking()
                                              .Include(o => o.transactions)
                                              .Include(o => o.reservations)
                                              .Where(o => o.bicycleType_Id == typeId && o.isConfirmed == true)
                                              .ToList();

            if(bikes.Any(o => o.transactions.Count == 0 && o.reservations.Count == 0))
            {
                return bikes.Where(o => o.transactions.Count == 0 && o.reservations.Count == 0).FirstOrDefault();
            }
            else if(bikes.Any(o => o.transactions.Count == 0 && o.reservations.Count != 0))
            {
                List<Bicycle> bicycles = bikes.Where(o => o.transactions.Count == 0 && o.reservations.Count != 0).ToList();
                    
                var bike = bicycles.Where(o => o.reservations.Any(t => ((start <= t.reservationDate && end >= t.reservationDate) ||
                                                                        (start >= t.reservationDate && end <= t.expectedReturnDate) ||
                                                                        (start <= t.expectedReturnDate && end >= t.expectedReturnDate)) &&
                                                                         t.isDeleted == false) == false)
                                   .FirstOrDefault();

                return bike;
            }
            else if(bikes.Any(o => o.transactions.Count != 0 && o.reservations.Count == 0))
            {
                List<Bicycle> bicycles = bikes.Where(o => o.transactions.Count != 0 && o.reservations.Count == 0).ToList();

                var bike = bicycles.Where(o => o.transactions.Any(t => ((start <= t.rentalDate && end >= t.rentalDate) ||
                                                                        (start >= t.rentalDate && end <= t.expectedReturnDate) ||
                                                                        (start <= t.expectedReturnDate && end >= t.expectedReturnDate)) &&
                                                                         t.isDeleted == false) == false)
                                   .FirstOrDefault();

                return bike;
            }
            else
            {
                List<Bicycle> bicycles = bikes.Where(o => o.transactions.Count != 0 && o.reservations.Count != 0).ToList();

                var bike = bicycles.Where(o => (o.transactions.Any(t => ((start <= t.rentalDate && end >= t.rentalDate) ||
                                                                        (start >= t.rentalDate && end <= t.expectedReturnDate) ||
                                                                        (start <= t.expectedReturnDate && end >= t.expectedReturnDate)) &&
                                                                         t.isDeleted == false) == false) &&
                                               (o.reservations.Any(t => ((start <= t.reservationDate && end >= t.reservationDate) ||
                                                                        (start >= t.reservationDate && end <= t.expectedReturnDate) ||
                                                                        (start <= t.expectedReturnDate && end >= t.expectedReturnDate)) &&
                                                                         t.isDeleted == false) == false))
                                   .FirstOrDefault();

                return bike;
            }
        }

        public Bicycle Create(Bicycle row)
        {
            _db.Bicycles.Add(row);
            _db.SaveChanges();
            return row;
        }

        public void Delete(int id)
        {
            _db.Remove(GetById(id));
            _db.SaveChanges();
        }

        public void Delete(Bicycle row)
        {
            _db.Bicycles.Remove(row);
            _db.SaveChanges();
        }

        public List<Bicycle> Search()
        {
            throw new NotImplementedException();
        }

        public Bicycle Update(Bicycle row)
        {
            Bicycle oldBicycle = GetById(row.id);

            row.isCurrentlyRented = oldBicycle.isCurrentlyRented;
            row.timesRented = oldBicycle.timesRented;
            row.earningsToDate = oldBicycle.earningsToDate;

            _db.Bicycles.Update(row);
            _db.SaveChanges();

            return row;
        }

        public Bicycle GetByUsername(string username)
        {
            throw new NotImplementedException();
        }
    }
}
