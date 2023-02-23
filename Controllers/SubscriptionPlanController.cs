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
    public class SubscriptionPlanController : Controller
    {
        private readonly ISubscriptionPlanService<SubscriptionPlan> _sService;
        private readonly IUserService<Admin> _aService;
        private readonly IUserService<Customer> _cService;

        public SubscriptionPlanController(ISubscriptionPlanService<SubscriptionPlan> sService,
                                      IUserService<Admin> aService,
                                      IUserService<Customer> cService)
        {
            _sService = sService;
            _aService = aService;
            _cService = cService;
        }

        public ActionResult Index()
        {
            return View(_sService.GetAll());
        }

        public ActionResult ActiveIndex()
        {
            return View(_sService.GetAll(false, true));
        }

        public ActionResult DeletedIndex()
        {
            return View(_sService.GetAll(true, false));
        }

        public ActionResult Details(int id)
        {
            return View(_sService.GetById(id));
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SubscriptionPlan row)
        {
            try
            {
                row = _sService.Create(row);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return View();
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Activate(int id)
        {
            try
            {
                _sService.Activate(_sService.GetById(id));
                return View("Details", _sService.GetById(id));
            }catch (Exception e)
            {
                return View("Details", _sService.GetById(id));
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Disable(int id)
        {
            try
            {
                _sService.Disable(_sService.GetById(id));
                return View("Details", _sService.GetById(id));
            }
            catch (Exception e)
            {
                return View("Details", _sService.GetById(id));
            }
        }


        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            return View(_sService.GetById(id));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, SubscriptionPlan row)
        {
            try
            {
                _sService.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return View();
            }
        }
    }
}
