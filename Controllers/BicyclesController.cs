using BikesTest.Exceptions;
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
    public class BicyclesController : Controller
    {

        private readonly IBicycleService<Bicycle> _bSrvc;
        private readonly IAdminService<Admin> _aSrvc;
        private readonly IUserService<Customer> _cSrvc;
        private readonly IBicycleTypeService<BicycleType> _btService;
        
        public BicyclesController(IBicycleService<Bicycle> Bservice,
                                  IUserService<Customer> Cservice,
                                  IAdminService<Admin> Aservice,
                                  IBicycleTypeService<BicycleType> BTserice)
        {
            _bSrvc = Bservice;
            _cSrvc = Cservice;
            _aSrvc = Aservice;
            _btService = BTserice;
        }

        [Authorize(Roles = "SuperAdmin," + nameof(AdminRoles.Roles.Bicycles))]
        public ActionResult Index()
        {
            return View(_bSrvc.GetAll());
        }

        [Authorize(Roles = "SuperAdmin," + nameof(AdminRoles.Roles.Bicycles))]
        public ActionResult Details(int id)
        {
            return View(_bSrvc.GetById(id));
        }



        [HttpGet]
        [Authorize(Roles = "SuperAdmin," + nameof(AdminRoles.Roles.Bicycles))]
        public ActionResult Create()
        {
            try
            {
                if (User.IsInRole(nameof(AdminRoles.Roles.Bicycles)))
                {
                    int currentUserId = Int32.Parse(User.Identities
                                                   .FirstOrDefault().FindFirst("Id").Value);

                    _aSrvc.CheckSuspended(currentUserId);

                }
                ViewBag.types = _btService.GetIdName();
                return View();
            }
            catch (SuspendedAdminException e)
            {
                TempData["AdminSuspendedError"] = e.Message;
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Bicycle bicycle)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new InvalidOperationException("Bicycle model invalid at creation");

                _bSrvc.Create(bicycle);
                
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            } 
        }


        [HttpGet]
        [Authorize(Roles = "SuperAdmin," + nameof(AdminRoles.Roles.Bicycles))]
        public ActionResult Edit(int id)
        {
            try
            {
                if (User.IsInRole(nameof(AdminRoles.Roles.Bicycles)))
                {
                    int currentUserId = Int32.Parse(User.Identities
                                                   .FirstOrDefault().FindFirst("Id").Value);

                    _aSrvc.CheckSuspended(currentUserId);
                }

                ViewBag.types = _btService.GetIdName();
                return View(_bSrvc.GetById(id));
            }
            catch (SuspendedAdminException e)
            {
                TempData["AdminSuspendedError"] = e.Message;
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id,  Bicycle bicycle)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new InvalidOperationException("Bicycle model invalid at edition");

                bicycle = _bSrvc.Update(bicycle);

                return RedirectToAction(nameof(Index));
            }
            catch(Exception e)
            {
                return View();
            }

        }
        [Authorize(Roles = "SuperAdmin," + nameof(AdminRoles.Roles.Bicycles))]
        public ActionResult Delete(int id)
        {
            try
            {
                if (User.IsInRole(nameof(AdminRoles.Roles.Bicycles)))
                {
                    int currentUserId = Int32.Parse(User.Identities
                                                   .FirstOrDefault().FindFirst("Id").Value);

                    _aSrvc.CheckSuspended(currentUserId);
                }

                return View(_bSrvc.GetById(id));
            }
            catch (SuspendedAdminException e)
            {
                TempData["AdminSuspendedError"] = e.Message;
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Bicycle bicycle)
        {
            _bSrvc.Delete(bicycle);
            return RedirectToAction(nameof(Index));
        }
    }
}
