using BikesTest.Exceptions;
using BikesTest.Interfaces;
using BikesTest.Models;
using BikesTest.ServiceExtentions;
using BikesTest.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly ITransactionService<Transaction> _tService;
        private readonly IReservationService<Reservation> _rService;
        private readonly IUserService<Admin> _aService;
        private readonly IUserService<Customer> _cService;
        private readonly IBicycleService<Bicycle> _bService;
        private readonly IBicycleTypeService<BicycleType> _btService;
        public TransactionsController(ITransactionService<Transaction> tService,
                                      IReservationService<Reservation> rService,
                                      IUserService<Admin> aService,
                                      IUserService<Customer> cService,
                                      IBicycleService<Bicycle> bService,
                                      IBicycleTypeService<BicycleType> btService)
        {
            _tService = tService;
            _rService = rService;
            _aService = aService;
            _cService = cService;
            _bService = bService;
            _btService = btService;
        }

        [Authorize(Roles = "Admin,Customer")]
        public ActionResult Index()
        {
            if (User.IsInRole("Admin"))
                return View(_tService.GetAll());
            else
                return View(_tService.GetAllByCustomerId(Int32.Parse(User.
                                                                    Identities.
                                                                    FirstOrDefault().
                                                                    FindFirst("Id").
                                                                    Value)));
        }
        [Authorize(Roles = "Admin")]
        public ActionResult DeletedIndex()
        {
            return View(_tService.GetAllDeleted());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeletedDetails(int id)
        {
            return View(_tService.GetByDeletedId(id));
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Details(int id)
        {
            //var s = _tService.GetByIdAsync(id).Result;
            return View(_tService.GetById(id));
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            try
            {
                _rService.VerifyExpiration(2);
                int currentAdminId = Int32.Parse(User.Identities.FirstOrDefault().FindFirst("Id").Value);
                _aService.CheckSuspended(currentAdminId);
                ViewBag.bicycleTypes = _btService.GetIdName();
                return View(_tService.GetById(id));
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
        public ActionResult Edit(Transaction transaction)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new InvalidOperationException("Transaction invalid");

                transaction = _tService.Update(transaction);

                return RedirectToAction("Details", "Transactions", new { id = transaction.id });
            }
            catch (CustomerDoesntExistException e)
            {
                ViewBag.usernames = _cService.GetUsernamesAndIds();
                ViewBag.bicycleTypes = _btService.GetIdName();
                ModelState.AddModelError("customer_Id", e.Message);
                return View(_tService.GetById(transaction.id));
            }
            catch (BikeDoesntExistException e)
            {
                ViewBag.usernames = _cService.GetUsernamesAndIds();
                ModelState.AddModelError("bicycle_Id", e.Message);
                return View(_tService.GetById(transaction.id));
            }
            catch (AdminDoesntExistException e)
            {
                ViewBag.usernames = _cService.GetUsernamesAndIds();
                ModelState.AddModelError("admin_Id", e.Message);
                return View(_tService.GetById(transaction.id));
            }
            catch (CurrentlyBikingException e)
            {
                ViewBag.usernames = _cService.GetUsernamesAndIds();
                ModelState.AddModelError("customer_Id", e.Message);
                return View(_tService.GetById(transaction.id));
            }
            catch (CurrentlyRentException e)
            {
                ViewBag.usernames = _cService.GetUsernamesAndIds();
                ModelState.AddModelError("bicycle_Id", e.Message);
                return View(_tService.GetById(transaction.id));
            }
            catch (SuspendedAdminException e)
            {
                ViewBag.usernames = _cService.GetUsernamesAndIds();
                ModelState.AddModelError("admin_Id", e.Message);
                return View(_tService.GetById(transaction.id));
            }
            catch (Exception e)
            {
                ViewData["error"] = e.Message;
                return View(_tService.GetById(transaction.id));
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult GetTransaction()
        {
            return View(); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetTransaction(string username)
        {
            try
            {
                if (_cService.IsUsernameExist(username))
                {
                    if (!_cService.GetById(_tService.GetByUsername(username).customer.id).isCurrentlyBiking)
                        throw new CustomerDidntRentException("This customer did not rent");
                    if (!_bService.GetById(_tService.GetByUsername(username).bicycle.id).isCurrentlyRented)
                        throw new UnrentBikeExcpeiton("This bicycle has not been rented");

                    ViewBag.bicycleTypes = _btService.GetIdName();
                    return View("ReturnBicycle", _tService.GetByUsername(username));
                }
                else
                    throw new InvalidUsernameException("This username Does not Exist");
            }
            catch (UnrentBikeExcpeiton e)
            {
                ViewData["error"] = e.Message;
                return View();
            }
            catch (CustomerDidntRentException e)
            {
                ViewData["error"] = e.Message;
                return View();
            }
            catch (InvalidUsernameException e)
            {
                ViewData["error"] = e.Message;
                return View();
            }
            catch (Exception e)
            {
                return View();
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult MockLogin()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult MockLogin(Customer row) //different than user Services MockLogin
        {
            if (!ModelState.IsValid)
                throw new InvalidOperationException("login user invalid");

            try
            {
                row = _cService.MockLogin(row);

                return RedirectToAction(nameof(Create), new { id = row.id });
            }
            catch (InvalidUsernameException e)
            {
                ModelState.AddModelError(nameof(row.user.username), e.Message);
                return View(row);
            }
            catch (InvalidPasswordException e)
            {
                ModelState.AddModelError(nameof(row.user.password), e.Message);
                return View(row);
            }
            catch (CustomerDoesntExistException e)
            {
                ModelState.AddModelError(nameof(row.user.username), e.Message);
                return View(row);
            }
            catch (Exception e)
            {
                return View(row);
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Create(int id)
        {
            try
            {
                Customer customer = new Customer();
                if (id != 0)
                    customer = _cService.GetById(id, true, true);

                _rService.VerifyExpiration(2);
                int currentAdminId = Int32.Parse(User.Identities.FirstOrDefault().FindFirst("Id").Value);
                _aService.CheckSuspended(currentAdminId);

                //ViewBag.bicycles = _bService.GetAllAvailable();
                ViewBag.usernames = _cService.GetUsernamesAndIds();
                ViewBag.bicycleTypes = _btService.GetIdName();
                ViewBag.customer = customer;

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
        public ActionResult Create([FromForm] Transaction transaction)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new InvalidOperationException("Transaction invalid");

                transaction = _tService.Create(transaction);

                return RedirectToAction(nameof(Index));
            }
            catch (CustomerDoesntExistException e)
            {
                ViewBag.bicycleTypes = _btService.GetIdName();
                ViewBag.usernames = _cService.GetUsernamesAndIds();
                ViewBag.customer = _cService.GetById(transaction.customer_Id);
                ModelState.AddModelError("customer_Id", e.Message);
                return View(transaction);
            }
            catch (BikeDoesntExistException e)
            {
                ViewBag.bicycleTypes = _btService.GetIdName();
                ViewBag.usernames = _cService.GetUsernamesAndIds();
                ViewBag.customer = _cService.GetById(transaction.customer_Id);
                ModelState.AddModelError("bicycle_Id", e.Message);
                return View(transaction);
            }
            catch (AdminDoesntExistException e)
            {
                ViewBag.bicycleTypes = _btService.GetIdName();
                ViewBag.usernames = _cService.GetUsernamesAndIds();
                ViewBag.customer = _cService.GetById(transaction.customer_Id);
                ModelState.AddModelError("admin_Id", e.Message);
                return View(transaction);
            }
            catch (CurrentlyBikingException e)
            {
                ViewBag.bicycleTypes = _btService.GetIdName();
                ViewBag.usernames = _cService.GetUsernamesAndIds();
                ViewBag.customer = _cService.GetById(transaction.customer_Id);
                ModelState.AddModelError("customer_Id", e.Message);
                ModelState.AddModelError("rentalDate", e.Message);
                return View(transaction);
            }
            catch (CurrentlyRentException e)
            {
                ViewBag.bicycleTypes = _btService.GetIdName();
                ViewBag.usernames = _cService.GetUsernamesAndIds();
                ViewBag.customer = _cService.GetById(transaction.customer_Id);
                ModelState.AddModelError("bicycle_Id", e.Message);
                return View(transaction);
            }
            catch (SuspendedAdminException e)
            {
                ViewBag.bicycleTypes = _btService.GetIdName();
                ViewBag.usernames = _cService.GetUsernamesAndIds();
                ViewBag.customer = _cService.GetById(transaction.customer_Id);
                ModelState.AddModelError("admin_Id", e.Message);
                return View(transaction);
            }
            catch (Exception e)
            {
                ViewBag.bicycleTypes = _btService.GetIdName();
                ViewBag.usernames = _cService.GetUsernamesAndIds();
                ViewBag.customer = _cService.GetById(transaction.customer_Id);
                ModelState.AddModelError("rentalDate", e.Message);
                return View(transaction);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReturnBicycle([FromForm] Transaction transaction)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new InvalidOperationException("Transaction invalid");

                transaction = _tService.ReturnBicycle(transaction);

                return RedirectToAction(nameof(Index));
            }
            catch (CustomerDoesntExistException e)
            {
                ViewBag.bicycleTypes = _btService.GetIdName();
                ViewBag.usernames = _cService.GetUsernamesAndIds();
                ModelState.AddModelError("customer_Id", e.Message);
                return View(transaction);
            }
            catch (BikeDoesntExistException e)
            {
                ViewBag.bicycleTypes = _btService.GetIdName();
                ViewBag.usernames = _cService.GetUsernamesAndIds();
                ModelState.AddModelError("bicycle_Id", e.Message);
                return View(transaction);
            }
            catch (AdminDoesntExistException e)
            {
                ViewBag.bicycleTypes = _btService.GetIdName();
                ViewBag.usernames = _cService.GetUsernamesAndIds();
                ModelState.AddModelError("admin_Id", e.Message);
                return View(transaction);
            }
            catch (CustomerDidntRentException e)
            {
                ViewBag.bicycleTypes = _btService.GetIdName();
                ViewBag.usernames = _cService.GetUsernamesAndIds();
                ModelState.AddModelError("customer_Id", e.Message);
                return View(transaction);
            }
            catch (UnrentBikeExcpeiton e)
            {
                ViewBag.bicycleTypes = _btService.GetIdName();
                ViewBag.usernames = _cService.GetUsernamesAndIds();
                ModelState.AddModelError("bicycle_Id", e.Message);
                return View(transaction);
            }
            catch (BikeMissmatchCustomerException e)
            {
                ViewBag.bicycleTypes = _btService.GetIdName();
                ViewBag.usernames = _cService.GetUsernamesAndIds();
                ModelState.AddModelError("cutomer_Id", e.Message);
                return View(transaction);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("rentalDate", e.Message);
                return View(transaction);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            try
            {
                int currentAdminId = Int32.Parse(User.Identities.FirstOrDefault().FindFirst("Id").Value);
                _aService.CheckSuspended(currentAdminId);
                return View(_tService.GetById(id));
            }
            catch(SuspendedAdminException e)
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
        public ActionResult Delete(int id, Transaction transaction)
        {
            try
            {
                _tService.Delete(transaction);
                return RedirectToAction(nameof(Index));
            }
            catch (CustomerDoesntExistException e)
            {
                ModelState.AddModelError("customerId", e.Message);
                return View(id);
            }
            catch (BikeDoesntExistException e)
            {
                ModelState.AddModelError("bicycleId", e.Message);
                return View(id);
            }
            catch (AdminDoesntExistException e)
            {
                ModelState.AddModelError("adminId", e.Message);
                return View(id);
            }
            catch (Exception e)
            {
                return View(id);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Return(Transaction row)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new InvalidOperationException("Transaction model invalid");

                return View();
            }
            catch (Exception e)
            {
                return View();
            }
            
        }
    }
}
