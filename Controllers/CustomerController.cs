using BikesTest.Exceptions;
using BikesTest.Interfaces;
using BikesTest.Models;
using BikesTest.ServiceExtentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IUserService<Customer> _cService;
        private readonly IUserService<Admin> _aService;
        private readonly ICouponTypeService<CouponType> _cotService;
        public CustomerController(IUserService<Customer> cService,
                                  IUserService<Admin> aService,
                                  ICouponTypeService<CouponType> cotService)
        {
            _cService = cService;
            _aService = aService;
            _cotService = cotService;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(_cService.GetAll());
        }

        [Authorize]
        public ActionResult Details(int id)
        {
            if (User.IsInRole("Admin"))
            {
                return View(_cService.GetById(id));
            }
            try
            {
                var customer = _cService.GetByUserId(id);
                this.IsIdAndConnectedCustomerMatch(customer.user.id);
                return View(customer);
            }
            catch(LoggedIdMissmatchException)
            {
                TempData["NotSameUser"] = "Not matching id and connected user";
                return Redirect("/Home");
            }
            catch
            {
                return Redirect("/Home");
            }
        }
        
        public ActionResult Create()
        {
            if (User.IsInRole("Admin"))
            {
                try
                {
                    int currentAdminId = Int32.Parse(User.Identities.FirstOrDefault().FindFirst("Id").Value);
                    _aService.CheckSuspended(currentAdminId);
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new InvalidOperationException("Customer model invalid at creation");

                customer = _cService.Create(customer);

                return RedirectToAction("Index", "Home");
            }
            catch(ExistingUsernameException e)
            {
                ViewData["UsernameError"] = e.Message;
                return View();
            }
            catch (ExistingEmailException e)
            {
                ViewData["EmailError"] = e.Message;
                return View();
            }
            catch (Exception e)
            {
                return View();
            }

        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            if (User.IsInRole("Admin"))
            {
                try
                {
                    int currentAdminId = Int32.Parse(User.Identities.FirstOrDefault().FindFirst("Id").Value);
                    _aService.CheckSuspended(currentAdminId);
                    return View(_cService.GetById(id));
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

            try
            {
                var customer = _cService.GetById(id);
                this.IsIdAndConnectedCustomerMatch(customer.user.id);
                return View(customer);
            }
            catch (LoggedIdMissmatchException)
            {
                TempData["NotSameUser"] = "Not matching id and connected user";
                return Redirect("/Home");
            }
            catch
            {
                return Redirect("/Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Customer customer)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new InvalidOperationException("Customer model invalid at edition");

                customer = _cService.Update(customer);

                return RedirectToAction(nameof(Details), new {id = id});
            }
            catch (ExistingUsernameException e)
            {
                ModelState.AddModelError("username", e.Message);
                return View(customer);
            }
            catch
            {
                return View(customer);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Customer")]
        public ActionResult RedeemCoupon(int id)
        {
            Customer customer = _cService.GetById(id, false, false);
            try
            {
                this.IsIdAndConnectedCustomerMatch(customer.user.id);
                ViewBag.customer = customer;
                var a = _cotService.GetAll(false);

                return View(a);
            }
            catch (CustomerIdsMissmatchException e)
            {
                return RedirectToAction(nameof(Details), customer.user.id);
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Details), customer.user.id);
            }
        }

        [Authorize]
        public ActionResult ChangePassword(int id)
        {
            if (User.IsInRole("Admin"))
            {
                try
                {
                    int currentAdminId = Int32.Parse(User.Identities.FirstOrDefault().FindFirst("Id").Value);
                    _aService.CheckSuspended(currentAdminId);
                    return View(_cService.GetById(id));
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

            try
            {
                var customer = _cService.GetById(id);
                this.IsIdAndConnectedCustomerMatch(customer.user.id);
                return View(customer);
            }
            catch (LoggedIdMissmatchException)
            {
                TempData["NotSameUser"] = "Not matching id and connected user";
                return Redirect("/Home");
            }
            catch
            {
                return Redirect("/Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(int id, string password)
        {
            try
            {
                _cService.ChangePassword(_cService.GetById(id), password);
                return RedirectToAction(nameof(Details), new { id = id });
            }
            catch
            {
                return View(_cService.GetById(id));
            }
            
        }
        [Authorize]
        public ActionResult Delete(int id)
        {
            if (User.IsInRole("Admin"))
            {
                try
                {
                    int currentAdminId = Int32.Parse(User.Identities.FirstOrDefault().FindFirst("Id").Value);
                    _aService.CheckSuspended(currentAdminId);
                    return View(_cService.GetById(id));
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

            try
            {
                this.IsIdAndConnectedCustomerMatch(id);
                return View(_cService.GetById(id));
            }
            catch (LoggedIdMissmatchException)
            {
                TempData["NotSameUser"] = "Not matching id and connected user";
                return Redirect("/Home");
            }
            catch
            {
                return Redirect("/Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Customer customer)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new InvalidOperationException("Customer model invalid at deletion");

                _cService.Delete(customer);  
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
