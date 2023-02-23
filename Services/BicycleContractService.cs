using BikesTest.Exceptions;
using BikesTest.Interfaces;
using BikesTest.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.Services
{
    public class BicycleContractService : IContractService<BicycleContract>
    {
        private readonly Context _db;
        private readonly IUserService<Customer> _cService;

        public BicycleContractService(Context db, IUserService<Customer> cService)
        {
            _db = db;
            _cService = cService;
        }
        public BicycleContract Create(BicycleContract row)
        {
            row.customer_Id = _cService.GetByUserId(row.customer_Id).id;


            _db.Add(row);
            _db.SaveChanges();
            return row;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(BicycleContract row)
        {
            _db.Remove(row);
            _db.SaveChanges();
        }

        public List<BicycleContract> GetAll()
        {
            return _db.BicycleContracts.
                       AsNoTracking().
                       Include(o => o.customer).ThenInclude(o => o.user).
                       Include(o => o.bicycle).ThenInclude(t => t.bicycleType).
                       ToList<BicycleContract>();
        }

        public List<BicycleContract> GetAll(bool active, bool denied)
        {
            if (active && denied)
                throw new InvalidOperationException("You cannot have an active and denied contract at once");
            
            return _db.BicycleContracts.
                    AsNoTracking().
                    Include(o => o.customer).ThenInclude(o => o.user).
                    Include(o => o.bicycle).ThenInclude(t => t.bicycleType).
                    Where(o => o.isActive == active && o.isDenied == denied).
                    ToList<BicycleContract>();
        }

        public BicycleContract GetById(int? id)
        {
            return _db.BicycleContracts.
                       AsNoTracking().
                       Where(o => o.id == id).
                       Include(o => o.bicycle).ThenInclude(o => o.bicycleType).
                       Include(o => o.customer).ThenInclude(o => o.user).
                       FirstOrDefault();
        }

        public List<BicycleContract> GetAllByCustomerId(int id, bool active, bool denied)
        {
            if (active && denied)
                throw new InvalidOperationException("You cannot have an active and denied contract at once");
            
            return _db.BicycleContracts.
                    AsNoTracking().
                    Include(o => o.customer).ThenInclude(o => o.user).
                    Include(o => o.bicycle).ThenInclude(t => t.bicycleType).
                    Where(o => o.customer_Id == id && o.isActive == active && o.isDenied == denied).
                    ToList<BicycleContract>();
            
        }
        public BicycleContract GetByUsername(string username)
        {
            return _db.BicycleContracts.
                       AsNoTracking().
                       Where(o => o.customer.user.username == username && o.isActive == true && o.isDenied == false).
                       FirstOrDefault();
        }

        public List<BicycleContract> GetAllByUsername(string username, bool active, bool denied)
        {
            if (active && denied)
                throw new InvalidOperationException("You cannot have an active and denied contract at once");
            
            return _db.BicycleContracts.
                    AsNoTracking().
                    Include(o => o.customer).ThenInclude(o => o.user).
                    Include(o => o.bicycle).ThenInclude(t => t.bicycleType).
                    Where(o => o.customer.user.username == username && o.isActive == active && o.isDenied == denied).
                    ToList<BicycleContract>();
            
                
        }

        public List<BicycleContract> Search()
        {
            throw new NotImplementedException();
        }

        public BicycleContract Update(BicycleContract row)
        {
            throw new NotImplementedException();
        }
        
        public BicycleContract Confirm(BicycleContract row)
        {
            row.isActive = true;
            row.bicycle.isConfirmed = true;

            _db.Update(row);
            _db.SaveChanges();
            return row;
        }

        public BicycleContract Cancel(BicycleContract row)
        {
            row.isActive = false;
            row.bicycle.isConfirmed = false;

            _db.Update(row);
            _db.SaveChanges();
            return row;
        }

        public BicycleContract Deny(BicycleContract row, string refusalInformation)
        {
            row.isDenied = true;
            row.isActive = false;
            row.bicycle.isConfirmed = false;
            row.refusalInformation = refusalInformation;

            _db.Update(row);
            _db.SaveChanges();
            return row;
        }
    }
}
