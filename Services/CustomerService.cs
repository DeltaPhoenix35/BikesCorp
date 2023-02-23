using BikesTest.Exceptions;
using BikesTest.Interfaces;
using BikesTest.Models;
using BikesTest.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.Services
{
    public class CustomerService : IUserService<Customer>
    {
        private readonly Context _db;
        private readonly IUserService<User> _uService;

        public CustomerService(Context db, IUserService<User> uService)
        {
            _db = db;
            _uService = uService;
        }

        public Customer Create(Customer row)
        {
            row.isCurrentlyBiking = false;
            row.numberOfBikesRented = 0;
            row.timeBiked = 0;

            row.user = _uService.Create(row.user);

            _db.Add(row);
            _db.SaveChanges();

            return row;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(Customer row)
        {
            _db.Customers.Remove(row);
            _db.SaveChanges();
        }

        public Customer Update(Customer row)
        {

            Customer dbCustomer = GetById(row.id);

            if (dbCustomer.user.username != row.user.username)
            {
                if (IsUsernameExist(row.user.username))
                    throw new ExistingUsernameException("This username already exists, try somehting else");
            }

            dbCustomer.user.username = row.user.username;
            dbCustomer.user.firstName = row.user.firstName;
            dbCustomer.user.lastName = row.user.lastName;
            dbCustomer.user.email = row.user.email;

            User user = dbCustomer.user;
            dbCustomer.user = null;

            _db.Customers.Update(dbCustomer);
            _db.Users.Update(user);
            _db.SaveChanges();
            return row;
        }

        public List<Customer> GetAll()
        {
            return _db.Customers.AsNoTracking()
                                .Include(o => o.user)
                                .ToList<Customer>(); 
        }

        public Customer GetById(int? id)
        {
            return _db.Customers.AsNoTracking()
                                .Include(o => o.user)
                                .Where(o => o.id == id)//.Include(o => o.transactions)
                                .Include(o => o.coupons.Where(p => p.isDeleted == false && p.isExpired == false && p.isUsed == false))
                                .ThenInclude(t => t.couponType)
                                .SingleOrDefault();
        }

        public Customer GetById(int? id, bool includeTransactions, bool includeReservations)
        {
            if(includeTransactions == true && includeReservations == false)
                return _db.Customers.AsNoTracking()
                                    .Include(o => o.user)
                                    .Where(o => o.id == id)
                                    .Include(o => o.transactions)
                                    .Include(o => o.coupons.Where(p => p.isDeleted == false && p.isExpired == false && p.isUsed == false))
                                    .ThenInclude(t => t.couponType)
                                    .SingleOrDefault();
            else if (includeTransactions == false && includeReservations == true)
                return _db.Customers.AsNoTracking()
                                    .Include(o => o.user)
                                    .Where(o => o.id == id)
                                    .Include(o => o.reservations)
                                    .Include(o => o.coupons.Where(p => p.isDeleted == false && p.isExpired == false && p.isUsed == false))
                                    .ThenInclude(t => t.couponType)
                                    .SingleOrDefault();
            else if (includeTransactions == true && includeReservations == true)
                return _db.Customers.AsNoTracking()
                                    .Include(o => o.user)
                                    .Where(o => o.id == id)
                                    .Include(o => o.reservations)
                                    .Include(o => o.transactions)
                                    .Include(o => o.coupons.Where(p => p.isDeleted == false && p.isExpired == false && p.isUsed == false))
                                    .ThenInclude(t => t.couponType)
                                    .SingleOrDefault();
            else
                return _db.Customers.AsNoTracking()
                                    .Include(o => o.user)
                                    .Where(o => o.id == id)
                                    .Include(o => o.coupons.Where(p => p.isDeleted == false && p.isExpired == false && p.isUsed == false))
                                    .ThenInclude(t => t.couponType)
                                    .SingleOrDefault();
        }


        public Customer GetByUsername(string username)
        {
            return _db.Customers.AsNoTracking()
                                .Include(o => o.user)
                                .Where(o => o.user.username == username)
                                .Include(o => o.transactions).AsNoTracking()
                                .Include(o => o.coupons.Where(p => p.isDeleted == false && p.isExpired == false && p.isUsed == false))
                                .ThenInclude(t => t.couponType)
                                .SingleOrDefault();
                    
        }

        public bool IsUsernameExist(string username)
        {
            return _db.Users.AsNoTracking().Any(o => o.username == username);
        }

        public Customer MockLogin(Customer row)
        {
           if (row.user.username == "" || row.user.username == null)
            {
                throw new InvalidUsernameException("Username Invalid");
            }
            else if (row.user.password == "" || row.user.password == null)
            {
                throw new InvalidPasswordException("Password Invalid");
            }

            Customer currentUser = GetByUsername(row.user.username);

            if (currentUser == null)
            {
                throw new CustomerDoesntExistException("User doesn't exist in database");
            }

            if (currentUser == null)
            {
                throw new InvalidUsernameException("Username Invalid");
            }

            if (LoginServices.HashPassword(row.user.password, currentUser.user.birthday.ToString("MM/dd/yyyy")) != currentUser.user.password)
            {
                throw new InvalidPasswordException("Password Invalid");
            }

            return currentUser;
        }

        public List<Customer> Search()
        {
            throw new NotImplementedException();
        }

        public Customer GetByUserId(int id)
        {
            return _db.Customers
                    .AsNoTracking()
                    .Include(o => o.user)
                    .Where(o => o.user.id == id)
                    .Include(o => o.coupons.Where(p => p.isDeleted == false && p.isExpired == false && p.isUsed == false && p.isReserved == false))
                    .ThenInclude(t => t.couponType)
                    .SingleOrDefault();
        }
    }
}
