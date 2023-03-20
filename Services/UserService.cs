﻿using BikesTest.Exceptions;
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
    public class UserService : IUserService<User>
    {
        private readonly Context _db;

        public UserService(Context db)
        {
            _db = db;
        }

        public User Create(User row)
        {
            if (_db.Users.AsNoTracking()
                         .Where(a => a.username == row.username).Any())
                throw new ExistingUsernameException("Entered username already used");

            if (_db.Users.AsNoTracking()
                         .Where(a => a.email == row.email).Any())
                throw new ExistingEmailException("Entered Email already used");

            row.password = LoginServices.HashPassword(row.password, row.birthday.ToString("MM/dd/yyyy"));

            _db.Add(row);
            _db.SaveChanges();

            return row;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(User row)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetById(int? id)
        {
            return _db.Users.AsNoTracking().Where(o => o.id == id)
                            .Include(o => o.admin)
                            .Include(o => o.superAdmin)
                            .Include(o => o.customer)
                            .FirstOrDefault();
        }

        public User GetById(int? id, bool includeTransactions, bool includeReservations)
        {
            throw new NotImplementedException();
        }

        public User GetByUserId(int id)
        {
            throw new NotImplementedException();
        }

        public User GetByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public bool IsUsernameExist(string username)
        {
            throw new NotImplementedException();
        }

        public User MockLogin(User row)
        {
            throw new NotImplementedException();
        }

        public List<User> Search()
        {
            throw new NotImplementedException();
        }

        public User Update(User row)
        {
            throw new NotImplementedException();
        }
    }
}
