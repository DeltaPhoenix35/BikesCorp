using BikesTest.Exceptions;
using BikesTest.Interfaces;
using BikesTest.Models;
using BikesTest.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BikesTest.Models.AdminRoles;

namespace BikesTest.Services
{
    public class AdminService : IAdminService<Admin>
    {
        private readonly Context _db;
        private IUserService<User> _uService;

        public AdminService(Context db,
                            IUserService<User> uService)
        {
            _db = db;
            _uService = uService;
        }

        public Admin Create(Admin row)
        {
            _uService.Create(row.user);

            row.isSuspended = false;
            row.isCurrentlyLogged = false;

            _db.Add(row);
            _db.SaveChanges();

            return row;
        }

        public void Delete(Admin row)
        {
            _db.Remove(row);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            _db.Remove(_db.Admins.AsNoTracking().Where(o => o.user.id == id).SingleOrDefault());
            _db.SaveChanges();
        }

        public List<Admin> GetAll()
        {
            return _db.Admins.AsNoTracking()
                             .Include(o => o.user)   
                             .ToList();
        }

        public Admin GetById(int? id)
        {
            return _db.Admins
                    .AsNoTracking()
                    .Include(o => o.user)
                    .Include(o => o.roles)
                    .Where(o => o.id == id)
                    .SingleOrDefault();
        }

        public Admin GetById(int? id, bool includeTransactions, bool includeReservations)
        {
            if (includeTransactions == true && includeReservations == false)
                return _db.Admins.AsNoTracking()
                                    .Include(o => o.user)
                                    .Include(o => o.roles)
                                    .Where(o => o.user.id == id)
                                    .Include(o => o.transactions)
                                    .SingleOrDefault();
            else if (includeTransactions == false && includeReservations == true)
                return _db.Admins.AsNoTracking()
                                    .Include(o => o.user)
                                    .Include(o => o.roles)
                                    .Where(o => o.user.id == id)
                                    .SingleOrDefault();
            else if (includeTransactions == true && includeReservations == true)
                return _db.Admins.AsNoTracking()
                                    .Include(o => o.user)
                                    .Include(o => o.roles)
                                    .Where(o => o.user.id == id)
                                    .Include(o => o.transactions)
                                    .SingleOrDefault();
            else
                return _db.Admins.AsNoTracking()
                                .Include(o => o.user)
                                .Include(o => o.roles)
                                .Where(o => o.id == id)
                                .SingleOrDefault();
        }

        public Admin GetByUserId(int id)
        {
            return _db.Admins
                    .AsNoTracking()
                    .Include(o => o.user)
                    .Include(o => o.roles)
                    .Where(o => o.user.id == id)
                    .SingleOrDefault();
        }

        public Admin GetByUsername(string username)
        {
            return _db.Admins
                    .AsNoTracking()
                    .Include(o => o.user)
                    .Include(o => o.roles)
                    .Where(o => o.user.username == username)
                    .SingleOrDefault();
        }

        public List<Admin> GetByRoles(List<Roles> roles)
        {
            List<Admin> admins = new List<Admin>();
            foreach(var role in roles)
                admins.AddRange(_db.Admins
                                    .AsNoTracking()
                                    .Include(o => o.user)
                                    .Include(o => o.roles)
                                    .Where(o => o.roles.Any(o => o.role == role))
                                    .ToList());

            return admins;
        }


        public bool IsUsernameExist(string username)
        {
            return _db.Admins.AsNoTracking()
                            .Include(o => o.user)
                            .Any(o => o.user.username == username);
        }

        public Admin MockLogin(Admin row)
        {
            if (row.user.username == "" || row.user.username == null)
            {
                throw new InvalidUsernameException("Username Invalid");
            }
            else if (row.user.password == "" || row.user.password == null)
            {
                throw new InvalidPasswordException("Password Invalid");
            }

            Admin currentUser = GetByUsername(row.user.username);

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

        public List<Admin> Search()
        {
            throw new NotImplementedException();
        }

        public Admin Update(Admin row)
        {
            Admin dbAdmin = GetById(row.id);

            if (dbAdmin.user.username != row.user.username)
            {
                if (this.IsUsernameExist(row.user.username))
                    throw new ExistingUsernameException("This username already exists, try somehting else");
            }

            dbAdmin.user.username = row.user.username;
            dbAdmin.user.firstName = row.user.firstName;
            dbAdmin.user.lastName = row.user.lastName;
            dbAdmin.user.email = row.user.email;

            User user = dbAdmin.user;

            dbAdmin.user = null;
            dbAdmin.roles = null;

            _db.Admins.Update(dbAdmin);
            _db.Users.Update(user);
            _db.SaveChanges();
            return row;
        }        
    }
}
