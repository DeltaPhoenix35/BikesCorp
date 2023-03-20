using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BikesTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikesTest.Services;
using BikesTest.Interfaces;
using Microsoft.AspNetCore.Authorization;
using BikesTest.ServiceExtentions;

namespace BikesTest.Controllers
{
    public class BicycleTypesController : Controller
    {

        private readonly IBicycleTypeService<BicycleType> _btService;
        private readonly IAdminService<Admin> _aService;

        public BicycleTypesController(IBicycleTypeService<BicycleType> btService,
                                      IAdminService<Admin> aService)
        {
            _btService = btService;
            _aService = aService;
        }

        public ActionResult Index()
        {
            List<BicycleType> bt = _btService.GetAll();
            return View(bt);
        }

        
        public ActionResult Details(int id)
        {
            BicycleType bt = _btService.GetById(id);
            return View(bt);
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin," + nameof(AdminRoles.Roles.Bicycles))]
        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BicycleType row)
        {
            try
            {
                if (User.IsInRole(nameof(AdminRoles.Roles.Bicycles)))
                {
                    int currentUserId = Int32.Parse(User.Identities
                                                   .FirstOrDefault().FindFirst("Id").Value);

                    _aService.CheckSuspended(currentUserId);
                }
                _btService.Create(row);
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
            if (User.IsInRole(nameof(AdminRoles.Roles.Bicycles)))
            {
                int currentUserId = Int32.Parse(User.Identities
                                               .FirstOrDefault().FindFirst("Id").Value);

                _aService.CheckSuspended(currentUserId);
            }
            return View(_btService.GetById(id));
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BicycleType row)
        {
            try
            {
                _btService.Update(row);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return View();
            }
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin," + nameof(AdminRoles.Roles.Bicycles))]
        public ActionResult Delete(int id)
        {
            if (User.IsInRole(nameof(AdminRoles.Roles.Bicycles)))
            {
                int currentUserId = Int32.Parse(User.Identities
                                               .FirstOrDefault().FindFirst("Id").Value);

                _aService.CheckSuspended(currentUserId);
            }
            return View(_btService.GetById(id));
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(BicycleType row)
        {
            try
            {
                _btService.Delete(row);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
