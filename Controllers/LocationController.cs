using BikesTest.Interfaces;
using BikesTest.Models;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.Controllers
{
    public class LocationController : Controller
    {

        public LocationController()
        {
            
        }

        //string path = AppDomain.CurrentDomain.BaseDirectory + @"maps2018.json";

        FirestoreDb db = FirestoreDb.Create("maps2018-415d9");


        // GET: LocationController
        public async Task<ActionResult> Index()
        {
            var collection = await db.Collection("locations").Document("0").Collection("records").OrderBy("recordTime").GetSnapshotAsync();
            List<Location> locations = new List<Location>();

            foreach (DocumentSnapshot documentSnapshot in collection.Documents)
            {
                if (documentSnapshot.Exists)
                {
                    Dictionary<string, object> city = documentSnapshot.ToDictionary();
                    
                    Location loc = new Location();

                    loc.Id = (long)city["transaction_id"];
                    loc.timestamp = ((Timestamp)city["recordTime"]).ToDateTime();
                    loc.geoPoint = (GeoPoint)city["location"];

                    locations.Add(loc);
                }
            }

            return View(locations);
        }

        // GET: LocationController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LocationController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LocationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: LocationController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LocationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: LocationController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LocationController/Delete/5
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
