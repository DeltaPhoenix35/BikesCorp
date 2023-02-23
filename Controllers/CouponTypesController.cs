using BikesTest.Models;
using BikesTest.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace BikesTest.Controllers
{
    public class CouponTypesController : Controller
    {
        private readonly ICouponTypeService<CouponType> _cotServices;

        public CouponTypesController(ICouponTypeService<CouponType> cotServices)
        {
            _cotServices = cotServices;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            List<CouponType> couponTypes = _cotServices.GetAll(false);
            return View(couponTypes);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult IndexDeleted()
        {
            List<CouponType> couponTypes = _cotServices.GetAll(true);
            return View(couponTypes);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Details(int id)
        {
            CouponType couponType = _cotServices.GetById(id);
            return View(couponType);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CouponType row)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new InvalidOperationException("coupon type model is not valid at creation");

                _cotServices.Create(row);

                return RedirectToAction(nameof(Index));
            }
            catch(Exception e)
            {
                return View(row);
            }
        }


        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            CouponType couponType = _cotServices.GetById(id);
            return View(couponType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(CouponType row)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new InvalidOperationException("Coupon Type not valid at deletion");

                _cotServices.Delete(row);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
