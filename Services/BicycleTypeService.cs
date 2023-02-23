using BikesTest.Interfaces;
using BikesTest.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.Services
{
    public class BicycleTypeService : IBicycleTypeService<BicycleType>
    {
        private readonly Context _db;

        public BicycleTypeService(Context db)
        {
            _db = db;
        }


        public BicycleType Create(BicycleType row)
        {
            _db.bicycleTypes.Add(row);
            _db.SaveChanges();
            return row;
        }

        public void Delete(int id)
        {
            _db.Remove(this.GetById(id));
            _db.SaveChanges();
        }

        public void Delete(BicycleType row)
        {
            _db.bicycleTypes.Remove(row);
            _db.SaveChanges();
        }

        public List<BicycleType> GetAll()
        {
            return _db.bicycleTypes.AsNoTracking().Include(o => o.pricing).ToList();
        }

        public BicycleType GetById(int? id)
        {
            return _db.bicycleTypes.AsNoTracking()
                                   .Include(o => o.pricing)
                                   .Where(o => o.id == id)
                                   .FirstOrDefault();
        }

        public BicycleType GetByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public BicycleType GetByName(string name)
        {
            return _db.bicycleTypes.AsNoTracking()
                                   .Include(o => o.pricing)
                                   .Where(o => o.typeName == name)
                                   .FirstOrDefault();
        }
        public List<BicycleType> Search()
        {
            throw new NotImplementedException();
        }

        public BicycleType Update(BicycleType row)
        {
            row.pricing.id = row.pricingScheme_Id;
            //_db.pricingSchemes.Update(row.pricing);
            //row.pricing = null;
            _db.Update(row);
            _db.SaveChanges();
            return row;
        }
    }
}
