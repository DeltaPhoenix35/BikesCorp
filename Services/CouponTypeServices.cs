using BikesTest.Interfaces;
using BikesTest.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.Services
{
    public class CouponTypeServices : ICouponTypeService<CouponType>
    {
        private readonly Context _db;
        public CouponTypeServices(Context db)
        {
            _db = db;

        }

        public CouponType Create(CouponType row)
        {
            _db.Add(row);
            _db.SaveChanges();

            return row;
        }

        public void Delete(int id)
        {
            CouponType couponType = _db.CouponTypes.AsNoTracking()
                                    .Where(o => o.isDeleted == false && o.id == id).FirstOrDefault();
            couponType.isDeleted = true;
            Update(couponType);
        }

        public void Delete(CouponType row)
        {
            row.isDeleted = true;
            Update(row);
        }

        public List<CouponType> GetAll()
        {
            return _db.CouponTypes.AsNoTracking()
                      .ToList();
        }

        public List<CouponType> GetAll(bool deleted)
        {
            return _db.CouponTypes.AsNoTracking()
                      .Where(o => o.isDeleted == deleted).ToList();
        }

        public CouponType GetById(int? id)
        {
            return _db.CouponTypes.AsNoTracking()
                      .Where(o => o.isDeleted == false && o.id == id).FirstOrDefault();
        }

        public CouponType GetById(int id, bool deleted)
        {
            return _db.CouponTypes.AsNoTracking()
                      .Where(o => o.isDeleted == deleted && o.id == id).FirstOrDefault();
        }

        public CouponType GetByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public List<CouponType> Search()
        {
            throw new NotImplementedException();
        }

        public CouponType Update(CouponType row)
        {
            _db.Update(row);
            _db.SaveChanges();
            return row;
        }
    }
}
