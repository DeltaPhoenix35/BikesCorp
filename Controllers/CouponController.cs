using BikesTest.Interfaces;
using BikesTest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.Controllers
{
    public class CouponController : Controller
    {
        private readonly IUserService<Customer> _cService;
        private readonly ICouponService<Coupon> _coService;
        private readonly ICouponTypeService<CouponType> _cotService;
        public CouponController(IUserService<Customer> cService,
                                ICouponService<Coupon> coService,
                                ICouponTypeService<CouponType> cotService)
        {
            _cService = cService;
            _coService = coService;
            _cotService = cotService;
        }

        [Authorize(Roles = "SuperAdmin," + nameof(AdminRoles.Roles.Customers) +",Customer")]
        public ActionResult Index(int id)
        {
            List<Coupon> coupons = _coService.GetByCustomerId(id, false, false, false, false);
            return View(coupons);
        }

        [Authorize(Roles = "SuperAdmin," + nameof(AdminRoles.Roles.Customers) + ",Customer")]
        public ActionResult DeletedIndex(int id)
        {
            List<Coupon> coupons = _coService.GetByUserId(id, true, false, false, false);
            return View(coupons);
        }

        [Authorize(Roles = "SuperAdmin," + nameof(AdminRoles.Roles.Customers) + ",Customer")]
        public ActionResult UsedIndex(int id)
        {
            List<Coupon> coupons = _coService.GetByCustomerId(id, false, true, false, false);
            return View(coupons);
        }

        [Authorize(Roles = "SuperAdmin," + nameof(AdminRoles.Roles.Customers) + ",Customer")]
        public ActionResult ExpiredIndex(int id)
        {
            List<Coupon> coupons = _coService.GetByCustomerId(id, false, false, true, false);
            return View(coupons);
        }

        [Authorize(Roles = "Customer")]
        public ActionResult RedeemCoupon(int id)
        {
            Customer customer = _cService.GetByUserId(Int32.Parse(User.Identities.FirstOrDefault().FindFirst("Id").Value));

            CouponType couponType = _cotService.GetById(id, false);
            
            try
            {
                if (customer.points < couponType.pointsToRedeem)
                    throw new InvalidOperationException("Customer doesn't have enough points to redeem that prize");

                ViewBag.customer = customer;
                return View("Confirm", couponType);
            }
            catch (InvalidOperationException e)
            {
                return RedirectToAction(nameof(RedeemCoupon), nameof(Customer), new { id = customer.user.id });
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(RedeemCoupon), nameof(Customer), new { id = customer.user.id });
            }
        }

        [Authorize(Roles = "Customer")]
        public ActionResult Confirm(int id)
        {
            Customer customer = _cService.GetByUserId(Int32.Parse(User.Identities.FirstOrDefault().FindFirst("Id").Value));
            CouponType couponType = _cotService.GetById(id, false);
            
            try
            {
                _coService.Redeem(couponType, customer);

                return RedirectToAction("Details", nameof(Customer), new { id = customer.user.id });
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(RedeemCoupon), nameof(Customer), new { id = customer.user.id });
            }
        }
    }
}
