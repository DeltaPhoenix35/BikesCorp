using BikesTest.Interfaces;
using BikesTest.Models;
using BikesTest.ServiceExtentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.Controllers
{
    public class SubscriptionController : Controller
    {
        private readonly ISubscriptionService<Subscription> _sService;
        private readonly ISubscriptionPlanService<SubscriptionPlan> _spService;
        private readonly IUserService<Customer> _cService;
        private readonly IBicycleTypeService<BicycleType> _btService;

        public SubscriptionController(ISubscriptionService<Subscription> sService,
                                      ISubscriptionPlanService<SubscriptionPlan> spService,
                                      IBicycleTypeService<BicycleType> btService,
                                      IUserService<Customer> cService)
        {
            _sService = sService;
            _spService = spService;
            _cService = cService;
            _btService = btService;
        }


        public ActionResult Index()
        {
            return View(_sService.GetAll(true));
        }

        public ActionResult InactiveIndex()
        {
            return View(_sService.GetAll(false));
        }

        public ActionResult CustomerIndex(int id)
        {
            return View(_sService.GetByCustomerId(id, true));
        }

       
        public ActionResult Details(int id)
        {
            return View(_sService.GetById(id));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.usernames = _cService.GetUsernamesAndIds();
            ViewBag.bicycleTypes = _btService.GetIdName();
            ViewBag.subscriptionPlans = _spService.GetIdName();

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Subscription row)
        {
            try
            {
                _sService.Create(row);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception e)
            {
                return View();
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Renew(int id)
        {
            return View(_sService.GetById(id));
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Renew(int id, Subscription row)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SubscriptionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
