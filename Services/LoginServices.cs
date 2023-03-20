using BikesTest.Exceptions;
using BikesTest.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BikesTest.Service
{
    public class LoginServices
    {
        private readonly Context _db;
        public LoginServices(Context db)
        {
            _db = db;
        }

        public static string HashPassword(string password, string salt)
        {
            byte[] hashedPassword = Encoding.UTF8.GetBytes(string.Concat(password, salt));
            SHA256 shaM = new SHA256Managed();
            string result = Convert.ToBase64String(shaM.ComputeHash(hashedPassword));
            shaM.Clear();
            return result;
        }

        public User LoginVerifications(User user)
        {
            if (user.username == "" || user.username == null)
            {
                throw new InvalidUsernameException("Username Invalid");
            }
            else if (user.password == "" || user.password == null)
            {
                throw new InvalidPasswordException("Password Invalid");
            }

            User currentUser = _db.Users.AsNoTracking()
                                        .Where(a => a.username == user.username)
                                        .Include(a => a.superAdmin)
                                        .Include(a => a.admin).ThenInclude(o => o.roles)
                                        .Include(a => a.customer)
                                        .SingleOrDefault();

            if (currentUser == null)
            {
                throw new CustomerDoesntExistException("User doesn't exist in database");
            }

            if (currentUser.admin == null && currentUser.customer == null && currentUser.superAdmin == null)
            {
                throw new InvalidUsernameException("Username Invalid");
            }

            if (LoginServices.HashPassword(user.password, currentUser.birthday.ToString("MM/dd/yyyy")) != currentUser.password)
            {
                throw new InvalidPasswordException("Password Invalid");
            }

            return currentUser;
        }

        public ClaimsPrincipal Login(User user)
        { 
            User currentUser = LoginVerifications(user);

            var claims = new List<Claim>();
            claims.Add(new Claim("Id", (currentUser.id).ToString()));

            if(currentUser.superAdmin != null)
                claims.Add(new Claim(ClaimTypes.Role, "SuperAdmin"));
            

            else if (currentUser.admin != null)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                foreach(var role in currentUser.admin.roles)
                    claims.Add(new Claim(ClaimTypes.Role, role.role.ToString()));

                currentUser.admin.isCurrentlyLogged = true;
                currentUser.admin.roles = null;
                _db.Update(currentUser);
                _db.SaveChanges();
            }

            else if (currentUser.customer != null)
                claims.Add(new Claim(ClaimTypes.Role, "Customer"));

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            return claimsPrincipal;
        }

        public User Logout(int id)
        {
            User currentUser = _db.Users.AsNoTracking().Where(o => o.id == id)
                                                       .Include(o => o.admin)
                                                       .Include(o => o.customer)
                                                       .SingleOrDefault();
            if (currentUser.admin != null)
            {
                currentUser.admin.isCurrentlyLogged = false;
                _db.Update(currentUser);
                _db.SaveChanges();
            }
            return currentUser;
        }

    }
}
