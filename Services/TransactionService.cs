using BikesTest.Interfaces;
using BikesTest.Models;
using BikesTest.Exceptions;
using BikesTest.ServiceExtentions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BikesTest.Services
{
    public class TransactionService : ITransactionService<Transaction>
    {
        
        private readonly Context _db;
        private readonly IUserService<Admin> _aService;
        private readonly IUserService<Customer> _cService;
        private readonly ICouponService<Coupon> _coService;
        private readonly IBicycleService<Bicycle> _bService;
        private readonly IBicycleTypeService<BicycleType> _btService;
        private readonly ILocationService _lService;
        public TransactionService(Context db,
                                  IUserService<Admin> aService,
                                  IUserService<Customer> cService,
                                  ICouponService<Coupon> coService,
                                  IBicycleService<Bicycle> bService,
                                  IBicycleTypeService<BicycleType> btService,
                                  ILocationService lService)
        {
            _db = db;
            _aService = aService;
            _cService = cService;
            _coService = coService;
            _bService = bService;
            _lService = lService;
            _btService = btService;
        }

        public Transaction BuildTransaction(Transaction row, Transaction lastTransaction ,Bicycle bicycle, Customer customer, Admin admin)
        {
            if(row.returnDate == null)
            {
                row.customer_Id = customer.id;
                row.admin_Id = admin.id;
                row.bicycle_Id = bicycle.id;
                row.bicycleType = _btService.GetById(row.bicycleType_Id);

                if (row.coupon_Id == 0 || row.coupon_Id == null)
                    row.coupon_Id = null;
                else
                {
                    _coService.Apply(row, customer);
                    //customer.coupons = null;
                }  

                _cService.SetIsCurrentlyBikingTrue(customer);
                _bService.SetIsCurrentlyRentedTrue(bicycle);

                if(row.subscription_Id == null || row.subscription_Id == 0)
                    this.CalculateTransactionCost(row, bicycle);

                row.admin = null;
                row.bicycle = null;
                row.customer = null;
                //row.coupon = null;
                row.bicycleType = null;

                return row;
            }
            else
            {
                lastTransaction.returnDate = row.returnDate;

                _cService.SetIsCurrentlyBikingFalse(customer);
                _bService.SetIsCurrentlyRentedFalse(bicycle);
                this.SetTransactionDuration(lastTransaction);

                _bService.IncrementTimesRented(bicycle);
                _bService.IncreaseEarningsToDate(bicycle, (double)lastTransaction.costOfTransaction);
                _cService.IncreaseTimeBiked(customer, (decimal)lastTransaction.durationOfTransaction);
                _cService.IncrementBikesRented(customer);

                if (lastTransaction.rentalDate < lastTransaction.expectedReturnDate)
                    _cService.AddPoints(customer, (decimal)(lastTransaction.expectedReturnDate - lastTransaction.rentalDate).TotalMinutes);

                lastTransaction.bicycle = null;
                lastTransaction.customer = null;
                lastTransaction.admin = null;
                lastTransaction.coupon = null;

                return lastTransaction;
            }
        }

        public Transaction Create(Transaction row)
        {
            Customer customer = _cService.GetById(row.customer_Id, false, true);
            if (customer == null)
                throw new CustomerDoesntExistException("Customer doesn't exist in data base");

            Bicycle bicycle;
            if (row.bicycle != null)
                bicycle = row.bicycle;
            else if (row.bicycle_Id == 0)
                bicycle = _bService.GetFirstAvailableByTypeId(row.bicycleType_Id, row.rentalDate, row.expectedReturnDate);
            else
                bicycle = _bService.GetById(row.bicycle_Id);

            if (bicycle == null)
                throw new BikeDoesntExistException("Bicycle doesn't exist in data base"); //change to bike not available
            row.bicycle_Id = bicycle.id;

            Admin admin = _aService.GetByUserId(row.admin_Id);
            if (admin == null)
                throw new AdminDoesntExistException("Admin doesn't exist in data base");

            this.SetTransactionNotDeleted(row);

            if (row.returnDate != null)
                throw new InvalidOperationException("Cannot have a return date at creation");

            if (customer.isCurrentlyBiking)
                throw new CurrentlyBikingException("This customer is currently biking");
            else if (bicycle.isCurrentlyRented)
                throw new CurrentlyRentException("This bicycle is currenty rented");
            else if (bicycle.isReserved)
            {
                Reservation reservation = bicycle.reservations.Where(o => ((row.rentalDate < o.reservationDate && row.expectedReturnDate > o.reservationDate) ||
                                                (row.rentalDate > o.reservationDate && row.expectedReturnDate < o.expectedReturnDate) ||
                                                (row.rentalDate < o.expectedReturnDate && row.expectedReturnDate > o.expectedReturnDate)) &&
                                                (o.isDeleted == false)).FirstOrDefault();
                if(reservation != null)
                {
                    throw new CurrentlyReservedException("bike is currently reserved from " + reservation.reservationDate
                                                            + " untill " + reservation.expectedReturnDate);
                }
                reservation = null;
            }
            else if (admin.isSuspended)
                throw new SuspendedAdminException("This admin is suspended and cannot perform this operation");


            //long lastTransactionId = _lService.GetLastTransactionId(bicycle.id).Result;

            //if (lastTransactionId <= bicycle.transactions.Count)
            //    throw new Exception("please start tracking before creating the transaction");

            //row.transactionNum = lastTransactionId.ToString() + "_" +  bicycle.id;

            row = BuildTransaction(row, null, bicycle, customer, admin);
            customer.reservations = null;
            bicycle.reservations = null;
            customer.coupons = null;

            _db.Customers.Update(customer);
            _db.Bicycles.Update(bicycle);
            _db.Transactions.Update(row);
            //_db.Admins.Update(admin);

            _db.SaveChanges();

            _db.ChangeTracker.Clear();

            return row;       
        }
 
        public Transaction ReturnBicycle(Transaction row)
        {
            Customer customer = _cService.GetById(row.customer_Id, false, true);
            if (customer == null)
                throw new CustomerDoesntExistException("Customer doesn't exist in data base");
            Bicycle bicycle = _bService.GetById(row.bicycle_Id);
            if (bicycle == null)
                throw new BikeDoesntExistException("Bicycle doesn't exist in data base");
            Admin admin = _aService.GetByUserId(row.admin_Id);
            if (admin == null)
                throw new AdminDoesntExistException("Admin doesn't exist in data base");

            this.SetTransactionNotDeleted(row);

            Transaction lastTransaction = bicycle.transactions.LastOrDefault();
            if (!customer.isCurrentlyBiking)
                throw new CustomerDidntRentException("This customer didn't rent any bicycle");
            else if (!bicycle.isCurrentlyRented)
                throw new UnrentBikeExcpeiton("This bicycle has not been rented recently");
            else if (admin.isSuspended)
                throw new SuspendedAdminException("This admin is suspended and cannot perform this operation");
            else if (lastTransaction != null)
            {
                if (lastTransaction.customer_Id != customer.id)
                    throw new BikeMissmatchCustomerException("This Customer did not rent this bicycle");
            }


            lastTransaction = BuildTransaction(row, lastTransaction, bicycle, customer, admin);

            foreach (var res in bicycle.reservations)
            {
                _db.Entry(res).State = EntityState.Detached;
            }
            customer.reservations = null;
            customer.coupons = null;
            bicycle.reservations = null;

            _db.Transactions.Update(lastTransaction);
            _db.Customers.Update(customer);
            _db.Bicycles.Update(bicycle);
            _db.Admins.Update(admin);
            _db.SaveChanges();

            return lastTransaction;

        }

        public Transaction Update(Transaction row)
        {
            Transaction dbTransaction = this.GetById(row.id);

            if (row.returnDate != null && dbTransaction.returnDate == null)
                throw new CurrentlyRentException("This transaction is not over, go through the return bike process if you want to complete this transaction");
            else if (row.returnDate != null)
            {
                if(row.returnDate != dbTransaction.returnDate || row.rentalDate != dbTransaction.rentalDate)
                {
                    dbTransaction.rentalDate = row.rentalDate;
                    dbTransaction.returnDate = row.returnDate;
                }

                if (row.bicycle_Id != dbTransaction.bicycle_Id)
                {
                    Bicycle oldBicycle = dbTransaction.bicycle;
                    _bService.IncreaseEarningsToDate(oldBicycle, -(double)dbTransaction.costOfTransaction); //old bike - old transaction cost
                    _bService.DecrementTimesRented(oldBicycle);

                    _db.Bicycles.Update(oldBicycle);

                    Bicycle newBicycle = _bService.GetById(row.bicycle_Id);
                    if (newBicycle == null)
                        throw new BikeDoesntExistException("Bicycle doesn't exist in data base");
                    else if (newBicycle.isCurrentlyRented)
                        throw new CurrentlyRentException("Bicycle is currently rented");

                    this.SetTransactionDuration(row);
                    this.CalculateTransactionCost(row, newBicycle);

                    _bService.IncreaseEarningsToDate(newBicycle, (double)dbTransaction.costOfTransaction);
                    _bService.IncrementTimesRented(newBicycle);

                    _db.Bicycles.Update(newBicycle); //maybe tracking problem

                    if(dbTransaction.customer_Id != row.customer_Id)
                    {
                        Customer oldCustomer = dbTransaction.customer;
                        _cService.IncreaseTimeBiked(oldCustomer, -(decimal)dbTransaction.durationOfTransaction);
                        _cService.DecrementBikesRented(oldCustomer);
                        _db.Customers.Update(oldCustomer);

                        Customer newCustomer = _cService.GetById(row.customer_Id);
                        if (newCustomer == null)
                            throw new CustomerDoesntExistException("Customer doesn't exist in data base");
                        else if (newCustomer.isCurrentlyBiking && newCustomer.id != dbTransaction.customer_Id)
                            throw new CurrentlyBikingException("Customer is currently biking");

                        _cService.IncreaseTimeBiked(newCustomer, (decimal)row.durationOfTransaction);
                        _cService.IncrementBikesRented(newCustomer);
                        _db.Customers.Update(newCustomer);
                    }
                    else
                    {
                        Customer oldCustomer = dbTransaction.customer;
                        _cService.IncreaseTimeBiked(oldCustomer, -(decimal)dbTransaction.durationOfTransaction);
                        _cService.IncreaseTimeBiked(oldCustomer, (decimal)row.durationOfTransaction);
                        _db.Customers.Update(oldCustomer);
                    }
                    this.SetTransactionDuration(dbTransaction);
                    this.CalculateTransactionCost(dbTransaction, newBicycle);
                }
                else
                {
                    Bicycle oldBicycle = dbTransaction.bicycle;
                    _bService.IncreaseEarningsToDate(oldBicycle, -(double)dbTransaction.costOfTransaction);

                    this.SetTransactionDuration(row);
                    this.CalculateTransactionCost(row, oldBicycle);

                    if (dbTransaction.customer_Id != row.customer_Id)
                    {
                        Customer oldCustomer = dbTransaction.customer;
                        _cService.IncreaseTimeBiked(oldCustomer, -(decimal)dbTransaction.durationOfTransaction);
                        _cService.DecrementBikesRented(oldCustomer);
                        _db.Customers.Update(oldCustomer);

                        Customer newCustomer = _cService.GetById(row.customer_Id);
                        if (newCustomer == null)
                            throw new CustomerDoesntExistException("Customer doesn't exist in data base");
                        else if (newCustomer.isCurrentlyBiking && newCustomer.id != dbTransaction.customer_Id)
                            throw new CurrentlyBikingException("Customer is currently biking");

                        _cService.IncreaseTimeBiked(newCustomer, (decimal)row.durationOfTransaction);
                        _cService.IncrementBikesRented(newCustomer);
                        _db.Customers.Update(newCustomer);
                    }
                    else
                    {
                        Customer oldCustomer = dbTransaction.customer;
                        _cService.IncreaseTimeBiked(oldCustomer, -(decimal)dbTransaction.durationOfTransaction);
                        _cService.IncreaseTimeBiked(oldCustomer, (decimal)row.durationOfTransaction);
                        _db.Customers.Update(oldCustomer);
                    }

                    this.SetTransactionDuration(dbTransaction);
                    this.CalculateTransactionCost(dbTransaction, oldBicycle);

                    _bService.IncreaseEarningsToDate(oldBicycle, (double)row.costOfTransaction);

                    _db.Bicycles.Update(oldBicycle);
                }
            }
            else if (row.returnDate == null)
            {
                if(row.rentalDate != dbTransaction.rentalDate)
                {
                    dbTransaction.rentalDate = row.rentalDate;
                    if (dbTransaction.customer_Id != row.customer_Id)
                    {
                        Customer oldCustomer = _cService.GetById(dbTransaction.customer_Id);
                        _cService.SetIsCurrentlyBikingFalse(oldCustomer);
                        _db.Customers.Update(oldCustomer);
                        Customer newCustomer = _cService.GetById(row.customer_Id);
                        _cService.SetIsCurrentlyBikingTrue(newCustomer);
                        _db.Customers.Update(newCustomer);
                    }

                    if (dbTransaction.bicycle_Id != row.bicycle_Id)
                    {
                        Bicycle oldBicycle = _bService.GetById(dbTransaction.bicycle_Id);
                        _bService.SetIsCurrentlyRentedFalse(oldBicycle);
                        _db.Bicycles.Update(oldBicycle);
                        Bicycle newBicycle = _bService.GetById(row.bicycle_Id);
                        _bService.SetIsCurrentlyRentedTrue(newBicycle);
                        _db.Bicycles.Update(newBicycle);
                    }
                } 
            }

            Admin admin = _aService.GetByUserId(row.admin_Id);

            dbTransaction.customer_Id = row.customer_Id;
            dbTransaction.bicycle_Id = row.bicycle_Id;
            dbTransaction.admin_Id = admin.id;
            
            _db.Transactions.Update(dbTransaction);  
            _db.SaveChanges();

            return row;
        } //to be updated too much stuff

        public void Delete(int id)
        {
            _db.Remove(GetById(id));
            _db.SaveChanges();
        }

        public void Delete(Transaction row)
        {
            row = _db.Transactions.AsNoTracking()
                                  .Where(o => o.id == row.id)
                                  .Include(o => o.customer).ThenInclude(m => m.user)
                                  .Include(m => m.admin).ThenInclude(m => m.user)
                                  .Include(m => m.bicycle).AsNoTracking()
                                  .SingleOrDefault();

            //_db.DeletedTransactions.Add(row);

            this.SetTransactionDeleted(row);
            row = NegateTransaction(row);

            _db.Update(row);
            _db.SaveChanges();
        }

        public List<Transaction> GetAll()
        {
            return _db.Transactions.AsNoTracking()
                                   .Where(o => o.isDeleted == false)
                                   .Include(o => o.customer).ThenInclude(m => m.user)
                                   .Include(m => m.admin).ThenInclude(m => m.user)
                                   .Include(m => m.bicycle)
                                   .ToList<Transaction>();
        }

        public List<Transaction> GetAllDeleted()
        {
            return _db.Transactions.AsNoTracking()
                                   .Where(o => o.isDeleted == true)
                                   .Include(o => o.customer).ThenInclude(m => m.user)
                                   .Include(m => m.admin).ThenInclude(m => m.user)
                                   .Include(m => m.bicycle)
                                   .ToList<Transaction>();
        }

        public List<Transaction> GetAllByCustomerId(int id)
        {
            return _db.Transactions.AsNoTracking()
                                   .Where(o => o.customer_Id == id && o.isDeleted == false)
                                   .Include(o => o.customer).ThenInclude(m => m.user)
                                   .Include(m => m.admin).ThenInclude(m => m.user)
                                   .Include(m => m.bicycle)
                                   .ToList<Transaction>();
        }

        public Transaction GetById(int? id)
        {
            Transaction transaction = _db.Transactions.AsNoTracking()
                                           .Where(o => o.isDeleted == false)
                                           .Where(o => o.id == id)
                                           .Include(o => o.customer).ThenInclude(m => m.user)
                                           .Include(m => m.admin).ThenInclude(m => m.user)
                                           .Include(m => m.bicycle)
                                           .SingleOrDefault();

            return transaction;
        }

        public async Task<Transaction> GetByIdAsync(int? id)
        {
            Transaction transaction = _db.Transactions.AsNoTracking()
                                           .Where(o => o.isDeleted == false)
                                           .Where(o => o.id == id)
                                           .Include(o => o.customer).ThenInclude(m => m.user)
                                           .Include(m => m.admin).ThenInclude(m => m.user)
                                           .Include(m => m.bicycle)
                                           .SingleOrDefault();

            transaction.locations = await _lService.GetAll(Int32.Parse(transaction.transactionNum.ElementAt(0).ToString()), transaction.bicycle_Id);

            return transaction;
        }

        public Transaction GetByDeletedId(int? id)
        {
            return _db.Transactions.AsNoTracking()
                                   .Where(o => o.isDeleted == true)
                                   .Where(o => o.id == id)
                                   .Include(o => o.customer).ThenInclude(m => m.user)
                                   .Include(m => m.admin).ThenInclude(m => m.user)
                                   .Include(m => m.bicycle)
                                   .SingleOrDefault();
        }

        public ICollection<Transaction> GetByBicycleId(int id)
        {
            return _db.Transactions.AsNoTracking()
                                   .Where(o => o.isDeleted == false)
                                   .Where(o => o.bicycle_Id == id)
                                   .Include(o => o.customer).ThenInclude(m => m.user)
                                   .Include(m => m.admin).ThenInclude(m => m.user)
                                   .Include(m => m.bicycle)
                                   .ToList<Transaction>();
        }

        public Transaction GetByUsername(string username)
        {
            return _db.Transactions.AsNoTracking()
                      .Where(o => o.isDeleted == false)
                      .Where(o => o.customer.id == _cService.GetByUsername(username).id && o.returnDate == null)
                      .Include(o => o.customer).ThenInclude(m => m.user)
                      .Include(m => m.admin).ThenInclude(m => m.user)
                      .Include(m => m.bicycle)
                      .SingleOrDefault();
        }

        public List<Transaction> Search()
        {
            throw new NotImplementedException();
        }

        public Transaction NegateTransaction(Transaction row)
        {
            row.costOfTransaction = -row.costOfTransaction;
            row.durationOfTransaction = -row.durationOfTransaction;

            Customer customer = row.customer;
            if (customer == null)
                throw new CustomerDoesntExistException("Customer doesn't exist in data base");
            Bicycle bicycle = row.bicycle;
            if (bicycle == null)
                throw new BikeDoesntExistException("Bicycle doesn't exist in data base");
            Admin admin = row.admin;
            if (admin == null)
                throw new AdminDoesntExistException("Admin doesn't exist in data base");

            if(row.returnDate != null)
            {
                _bService.IncreaseEarningsToDate(bicycle, (double)row.costOfTransaction);
                _cService.IncreaseTimeBiked(customer, (decimal)row.durationOfTransaction);
                _bService.DecrementTimesRented(bicycle);
                _cService.DecrementBikesRented(customer);
            }
            else
            {
                _bService.SetIsCurrentlyRentedFalse(bicycle);
                _cService.SetIsCurrentlyBikingFalse(customer);
            }

            _db.Customers.Update(customer);
            _db.Bicycles.Update(bicycle);

            return row;
        }
    }
}
