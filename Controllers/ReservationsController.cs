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
    public class ReservationsController : Controller
    {
        private readonly IReservationService<Reservation> _rService;
        private readonly ITransactionService<Transaction> _tService;
        private readonly IBicycleService<Bicycle> _bService;
        private readonly IBicycleTypeService<BicycleType> _btService;
        private readonly IUserService<Customer> _cService;
        private readonly IAdminService<Admin> _aService;

        public ReservationsController(IReservationService<Reservation> rService,
                                      ITransactionService<Transaction> tService,
                                      IAdminService<Admin> aService,
                                      IUserService<Customer> cService,
                                      IBicycleService<Bicycle> bService,
                                      IBicycleTypeService<BicycleType> btService)
        {
            _rService = rService;
            _tService = tService;
            _aService = aService;
            _cService = cService;
            _bService = bService;
            _btService = btService;
        }

        [Authorize(Roles = "SuperAdmin," + nameof(AdminRoles.Roles.Reservations) + ",Customer")]
        [HttpGet]
        public ActionResult Index()
        {
            if (User.IsInRole("SuperAdmin") ||User.IsInRole(nameof(AdminRoles.Roles.Reservations)))
                return View(_rService.GetAll());
            else {
                int id = Int32.Parse(User.Identities.FirstOrDefault().FindFirst("Id").Value);
                return View(_rService.GetByCustomerUserId(id));
            }
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin," + nameof(AdminRoles.Roles.Reservations) + ",Customer")]
        public ActionResult Details(int id)
        {
            try
            {
                Reservation reservation = _rService.GetById(id);
                if (reservation != null)
                {
                    if (User.IsInRole("Customer"))
                        _rService.CheckCustomerReservationMissmatch(reservation, Int32.Parse(User.
                                                                                            Identities.
                                                                                            FirstOrDefault().
                                                                                            FindFirst("Id").
                                                                                            Value));
                    return View(reservation);
                }
                else
                    throw new ReservationDoesntExistException(); 
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Index));
            }
        }


        [HttpGet]
        [Authorize(Roles = "Customer")]
        
        public ActionResult Create()
        {
            ViewBag.bicycleTypes = _btService.GetIdName();
            if (User.IsInRole("Customer"))
            {
                Customer customer = _cService.GetByUserId(
                                    Int32.Parse(User.Identities.FirstOrDefault().FindFirst("Id").Value));
                ViewBag.customer = _cService.GetById(customer.id, true, true);
            }
            
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Reservation row)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new InvalidOperationException("Reservation invalid");

                row = _rService.Create(row);
                return RedirectToAction(nameof(Index));
            }
            catch(BikeDoesntExistException e)
            {
                ViewBag.customer = row.customer;
                ModelState.AddModelError("bicycle_Id", e.Message);
                return View(row);
            }
            catch(CustomerDoesntExistException e)
            {
                ViewBag.customer = row.customer;
                ModelState.AddModelError("customer_Id", e.Message);
                return View(row);
            }
            catch (InvalidDateException e)
            {
                ViewBag.customer = row.customer;
                ModelState.AddModelError("reservationDate", e.Message);
                return View(row);
            }
            catch (CurrentlyRentException e)
            {
                ViewBag.customer = row.customer;
                ModelState.AddModelError("bicycle_Id", e.Message);
                return View(row);
            }
            catch(CurrentlyReservedException e)
            {
                ViewBag.customer = row.customer;
                ModelState.AddModelError("bicycle_Id", e.Message);
                return View(row);
            }
            catch (CurrentlyUsedException e)
            {
                ViewBag.customer = row.customer;
                ModelState.AddModelError("coupon_Id", e.Message);
                return View(row);
            }
            catch (CurrentlyBikingException e)
            {
                ViewBag.customer = row.customer;
                ModelState.AddModelError("customer_Id", e.Message);
                return View(row);
            }
            catch (Exception e)
            {
                ViewBag.customer = row.customer;
                ViewBag.bicycleTypes = _btService.GetIdName();
                return View(row);
            }
        }

        
        [HttpGet]
        [Authorize(Roles = "SuperAdmin," + nameof(AdminRoles.Roles.Reservations))]
        public ActionResult Edit(int id)
        {
            try
            {
                Reservation reservation = _rService.GetById(id);
                if (reservation != null)
                {
                    if (User.IsInRole("Customer"))
                        _rService.CheckCustomerReservationMissmatch(reservation, Int32.Parse(User.
                                                                                             Identities.
                                                                                             FirstOrDefault().
                                                                                             FindFirst("Id").
                                                                                             Value));
                    return View(reservation);
                }
                else
                    throw new ReservationDoesntExistException();
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Reservation row)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new InvalidOperationException("Reservation invalid");

                _rService.Update(row);
                return RedirectToAction(nameof(Index));
            }
            catch (BikeDoesntExistException e)
            {
                ModelState.AddModelError("bicycle_Id", e.Message);
                return View(row);
            }
            catch (CustomerDoesntExistException e)
            {
                ModelState.AddModelError("customer_Id", e.Message);
                return View(row);
            }
            catch (InvalidDateException e)
            {
                ModelState.AddModelError("reservationDate", e.Message);
                return View(row);
            }
            catch (CurrentlyRentException e)
            {
                ModelState.AddModelError("bicycle_Id", e.Message);
                return View(row);
            }
            catch (CurrentlyReservedException e)
            {
                ModelState.AddModelError("bicycle_Id", e.Message);
                return View(row);
            }
            catch (CurrentlyBikingException e)
            {
                ModelState.AddModelError("customer_Id", e.Message);
                return View(row);
            }
            catch (Exception e)
            {
                return View(row);
            }
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin," + nameof(AdminRoles.Roles.Reservations))]
        public ActionResult Confirm(int id)
        {
            return View(_rService.GetById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Confirm(Reservation row)
        {
            try
            {
                int currentAdminId = Int32.Parse(User.Identities.FirstOrDefault().FindFirst("Id").Value);
                row = _rService.GetById(row.id);
                row.bicycle = _bService.GetById(row.bicycle_Id);
                _rService.ConfirmReservation(row, currentAdminId); 
                return RedirectToAction(nameof(Index));
            }
            catch (CustomerDoesntExistException e)
            {
                ModelState.AddModelError("customer_Id", e.Message);
                return View(row);
            }
            catch (BikeDoesntExistException e)
            {
                ModelState.AddModelError("bicycle_Id", e.Message);
                return View(row);
            }
            catch (AdminDoesntExistException e)
            {
                ModelState.AddModelError("admin_Id", e.Message);
                return View(row);
            }
            catch (CurrentlyBikingException e)
            {
                ModelState.AddModelError("customer_Id", e.Message);
                return View(row);
            }
            catch (CurrentlyRentException e)
            {
                ModelState.AddModelError("bicycle_Id", e.Message);
                return View(row);
            }
            catch (SuspendedAdminException e)
            {
                ModelState.AddModelError("admin_Id", e.Message);
                return View(row);
            }
            catch (Exception e)
            {
                return View(row);
            }
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin," + nameof(AdminRoles.Roles.Reservations) + ",Customer")]
        public ActionResult Cancel(int id)
        {
            try { 
                Reservation reservation = _rService.GetById(id);
                if (reservation != null)
                {
                    if (User.IsInRole("Customer"))
                        _rService.CheckCustomerReservationMissmatch(reservation, Int32.Parse(User.
                                                                                            Identities.
                                                                                            FirstOrDefault().
                                                                                            FindFirst("Id").
                                                                                            Value));
                    return View(reservation);
                }
                else
                    throw new ReservationDoesntExistException("Reservation doesn't exist or is already canceled");
            }
            catch(Exception e)
            {
                TempData["ReservationError"] = e.Message;
                return RedirectToAction(nameof(Index));
            }   
        }
  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cancel(Reservation row)
        {
            try
            {
                row = _rService.GetById(row.id);
                _rService.CancelReservation(row);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception e)
            {
                return View(row);
            }
        }
    }
}
