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

        [Authorize(Roles = "SuperAdmin")]
        public ActionResult Index()
        {
            return View(_sService.GetAll(true, false));
        }

        [Authorize(Roles = "SuperAdmin")]
        public ActionResult InactiveIndex()
        {
            return View(_sService.GetAll(false, true));
        }

        [Authorize(Roles = "SuperAdmin,Customer,Admin")]
        public ActionResult CustomerIndex(int id)
        {
            return View(_sService.GetByCustomerId(id, true, false));
        }

        [Authorize(Roles = "SuperAdmin,Customer,Admin")]
        public ActionResult Details(int id)
        {
            return View(_sService.GetById(id));
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
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
            catch (SubscriptionPlanDoesntExistException e)
            {
                ViewBag.usernames = _cService.GetUsernamesAndIds();
                ViewBag.bicycleTypes = _btService.GetIdName();
                ViewBag.subscriptionPlans = _spService.GetIdName();

                ModelState.AddModelError(nameof(row.subscriptionPlan_Id), e.Message);

                return View(row);
            }
            catch (SubscriptionPlanIsntActive e)
            {
                ViewBag.usernames = _cService.GetUsernamesAndIds();
                ViewBag.bicycleTypes = _btService.GetIdName();
                ViewBag.subscriptionPlans = _spService.GetIdName();

                ModelState.AddModelError(nameof(row.subscriptionPlan_Id), e.Message);

                return View(row);
            }
            catch (SubscriptionPlanIsDeleted e)
            {
                ViewBag.usernames = _cService.GetUsernamesAndIds();
                ViewBag.bicycleTypes = _btService.GetIdName();
                ViewBag.subscriptionPlans = _spService.GetIdName();

                ModelState.AddModelError(nameof(row.subscriptionPlan_Id), e.Message);

                return View(row);
            }
            catch(CustomerDoesntExistException e)
            {
                ViewBag.usernames = _cService.GetUsernamesAndIds();
                ViewBag.bicycleTypes = _btService.GetIdName();
                ViewBag.subscriptionPlans = _spService.GetIdName();

                ModelState.AddModelError(nameof(row.customer_Id), e.Message);

                return View(row);
            }
            catch (CurrentlyBikingException e)
            {
                ViewBag.usernames = _cService.GetUsernamesAndIds();
                ViewBag.bicycleTypes = _btService.GetIdName();
                ViewBag.subscriptionPlans = _spService.GetIdName();

                ModelState.AddModelError(nameof(row.customer_Id), e.Message);

                return View(row);
            }
            catch (Exception e)
            {
                ViewBag.usernames = _cService.GetUsernamesAndIds();
                ViewBag.bicycleTypes = _btService.GetIdName();
                ViewBag.subscriptionPlans = _spService.GetIdName();
                return View(row);
            }
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin," + nameof(AdminRoles.Roles.Customers))]
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
                _sService.Renew(_sService.GetById(id));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin,Customer")]
        public ActionResult Delete(int id)
        {
            Subscription row = _sService.GetById(id);
            if (User.IsInRole("Customer"))
            {
                Customer connectedCustomer = _cService.GetByUserId(Int32.Parse(User.Identities.FirstOrDefault().FindFirst("Id").Value));
                if (connectedCustomer.id != row.customer_Id)
                    return RedirectToAction(nameof(CustomerIndex), connectedCustomer.id);
            }
            
            return View(row);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Subscription row)
        {
            try
            {
                _sService.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception e)
            {
                return View();
            }
        }
    }
}
