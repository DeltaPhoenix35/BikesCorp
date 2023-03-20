using BikesTest.Models;
using BikesTest.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using BikesTest.ServiceExtentions;
using BikesTest.Exceptions;

namespace BikesTest.Controllers
{
    public class CouponTypesController : Controller
    {
        private readonly ICouponTypeService<CouponType> _cotServices;
        private readonly IAdminService<Admin> _aServices;

        public CouponTypesController(ICouponTypeService<CouponType> cotServices,
                                     IAdminService<Admin> aServices)
        {
            _cotServices = cotServices;
            _aServices = aServices;
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin," + nameof(AdminRoles.Roles.Customers))]
        public ActionResult Index()
        {
            List<CouponType> couponTypes = _cotServices.GetAll(false);
            return View(couponTypes);
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin," + nameof(AdminRoles.Roles.Customers))]
        public ActionResult IndexDeleted()
        {
            List<CouponType> couponTypes = _cotServices.GetAll(true);
            return View(couponTypes);
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin," + nameof(AdminRoles.Roles.Customers))]
        public ActionResult Details(int id)
        {
            CouponType couponType = _cotServices.GetById(id);
            return View(couponType);
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin," + nameof(AdminRoles.Roles.Customers))]
        public ActionResult Create()
        {
            try
            {
                if (User.IsInRole(nameof(AdminRoles.Roles.Customers)))
                {
                    int currentAdminId = Int32.Parse(User.Identities.FirstOrDefault().FindFirst("Id").Value);
                    _aServices.CheckSuspended(currentAdminId);
                }
                return View();
            }
            catch (SuspendedAdminException e)
            {
                TempData["AdminSuspendedError"] = e.Message;
                return View("/Home");
            }
            catch (Exception e)
            {
                return View("/Home");
            }

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
            
            catch (Exception e)
            {
                return View(row);
            }
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin," + nameof(AdminRoles.Roles.Customers))]
        public ActionResult Delete(int id)
        {
            try
            {
                if (User.IsInRole(nameof(AdminRoles.Roles.Customers)))
                {
                    int currentAdminId = Int32.Parse(User.Identities.FirstOrDefault().FindFirst("Id").Value);
                    _aServices.CheckSuspended(currentAdminId);
                }

                CouponType couponType = _cotServices.GetById(id);
                return View(couponType);
            }
            catch (SuspendedAdminException e)
            {
                TempData["AdminSuspendedError"] = e.Message;
                return View("/Home");
            }
            catch (Exception e)
            {
                return View("/Home");
            }


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
