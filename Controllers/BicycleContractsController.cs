using BikesTest.Exceptions;
using BikesTest.Interfaces;
using BikesTest.Models;
using BikesTest.ServiceExtentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.Controllers
{
    public class BicycleContractsController : Controller
    {
        private readonly IContractService<BicycleContract> _bcService;
        private readonly IBicycleTypeService<BicycleType> _btService;
        private readonly IUserService<Customer> _cService;
        private readonly IBicycleService<Bicycle> _bService;
        public BicycleContractsController(IContractService<BicycleContract> BcService,
                                          IBicycleTypeService<BicycleType> BtService,
                                          IUserService<Customer> CService,
                                          IBicycleService<Bicycle> BService)
        {
            _bcService = BcService;
            _btService = BtService;
            _cService = CService;
            _bService = BService;
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin," + nameof(AdminRoles.Roles.Bicycles) + ",Customer")]
        public ActionResult Index()
        {
            List<BicycleContract> list;
            if (User.IsInRole(nameof(AdminRoles.Roles.Bicycles)) || User.IsInRole("SuperAdmin"))
                list = _bcService.GetAll(true, false);
            else
            {
                int id = _cService.GetByUserId(Int32.Parse(User.Identities.ToList().FirstOrDefault().FindFirst("Id").Value)).id;
                list = _bcService.GetAllByCustomerId(id, true, false);
            }
                
            return View(list);
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin," + nameof(AdminRoles.Roles.Bicycles) + ",Customer")]
        public ActionResult InactiveIndex()
        {
            List<BicycleContract> list;
            if (User.IsInRole(nameof(AdminRoles.Roles.Bicycles)) || User.IsInRole("SuperAdmin"))
                list = _bcService.GetAll(false, false);
            else
            {
                int id = _cService.GetByUserId(Int32.Parse(User.Identities.ToList().FirstOrDefault().FindFirst("Id").Value)).id;
                list = _bcService.GetAllByCustomerId(id, false, false);
            }

            return View(list);
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin," + nameof(AdminRoles.Roles.Bicycles) + ",Customer")]
        public ActionResult DeniedIndex()
        {
            List<BicycleContract> list;
            if (User.IsInRole(nameof(AdminRoles.Roles.Bicycles)) || User.IsInRole("SuperAdmin"))
                list = _bcService.GetAll(false, true);
            else
            {
                int id = _cService.GetByUserId(Int32.Parse(User.Identities.ToList().FirstOrDefault().FindFirst("Id").Value)).id;
                list = _bcService.GetAllByCustomerId(id, false, true);
            }

            return View(list);
        }


        [HttpGet]
        [Authorize(Roles = "SuperAdmin," + nameof(AdminRoles.Roles.Bicycles) + ",Customer")]
        public ActionResult Create()
        {
            ViewBag.types = _btService.GetIdName();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BicycleContract row)
        {
            try
            {
                if (row.customer_Id != Int32.Parse(User.Identities.FirstOrDefault().FindFirst("Id").Value))
                    throw new CustomerIdsMissmatchException("You must insert your own id for this operation");

                row.bicycle.isConfirmed = false;
                _bcService.Create(row);

                return Redirect("/Home");
            }
            catch (Exception e)
            {
                ViewBag.types = _btService.GetIdName();
                return View();
            }
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin," + nameof(AdminRoles.Roles.Bicycles) + ",Customer")]
        public ActionResult Details(int id)
        {
            try
            {
                if (User.IsInRole(nameof(AdminRoles.Roles.Bicycles)) || User.IsInRole("SuperAdmin"))
                    return View(_bcService.GetById(id));
                else
                {
                    Customer customer = _cService.GetByUserId(Int32.Parse(User.Identities.ToList().FirstOrDefault().FindFirst("Id").Value));
                    BicycleContract bicycleContract = _bcService.GetById(id);
                    if (customer.id != bicycleContract.customer_Id) 
                        throw new CustomerIdsMissmatchException("You can only check your own contracts");
                    return View(bicycleContract);
                }
            }catch(Exception e)
            {
                return RedirectToAction(nameof(Index));
            }

        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin," + nameof(AdminRoles.Roles.Bicycles))]
        public ActionResult Confirm(int id)
        {
            return View(_bcService.GetById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Confirm(BicycleContract row)
        {
            try
            {
                row = _bcService.GetById(row.id);
                _bcService.Confirm(row);

                return RedirectToAction(nameof(Index));
            }
            catch(Exception e)
            {
                return View(row);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Customer")]
        public ActionResult Cancel(int id)
        {
            try
            {
                Customer customer = _cService.GetByUserId(Int32.Parse(User.Identities.ToList().FirstOrDefault().FindFirst("Id").Value));
                BicycleContract bicycleContract = _bcService.GetById(id);
                if (customer.id != bicycleContract.customer_Id)
                    throw new CustomerIdsMissmatchException("You cannot cancel someones else's contracts");
                return View(bicycleContract);
            }
            catch(Exception e)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cancel(BicycleContract row)
        {
            try
            {
                row = _bcService.GetById(row.id);
                _bcService.Cancel(row);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return View(row);
            }
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin," + nameof(AdminRoles.Roles.Bicycles))]
        public ActionResult Deny(int id)
        {
            //check if admin has access to denying
            try
            {
                var a = _bcService.GetById(id);
                return View(_bcService.GetById(id));
            }catch(Exception e)
            {
                return RedirectToAction(nameof(Index));
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deny(BicycleContract row)
        {
            try
            {
                BicycleContract oldRow = _bcService.GetById(row.id);
                _bcService.Deny(oldRow, row.refusalInformation);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return View(row);
            }
        }
    }
}
