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

namespace BikesTest.Controllers
{
    public class BicycleTypesController : Controller
    {

        private readonly IBicycleTypeService<BicycleType> _btService;

        public BicycleTypesController(IBicycleTypeService<BicycleType> btService)
        {
            _btService = btService;
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
        [Authorize(Roles = "Admin")]
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
                _btService.Create(row);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            
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
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
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
