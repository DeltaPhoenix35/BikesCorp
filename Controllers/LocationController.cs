using BikesTest.Interfaces;
using BikesTest.Models;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.Controllers
{
    public class LocationController : Controller
    {

        private readonly IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "iCprAJzqdlLz0mou7g8j6D2eOJFzU041F0dfbdI5",
            BasePath = "https://maps2018-415d9-default-rtdb.europe-west1.firebasedatabase.app"
        };
        IFirebaseClient _client;

        private readonly ILocationService _lService;

        public LocationController(ILocationService lService)
        {
            _client = new FireSharp.FirebaseClient(config);
            _lService = lService;
        }


        public ActionResult Index()
        {
            FirebaseResponse response = _client.Get("locations/tests");

            dynamic collection = JsonConvert.DeserializeObject<dynamic>(response.Body);

            var list = new List<Location>();
            if (collection != null)
            {
                foreach (var item in collection)
                {
                    list.Add(JsonConvert.DeserializeObject<Location>(((JProperty)item).Value.ToString()));
                }
            }

            return View(list);
        }

        public ActionResult SetActive(int bicycleId)
        {
            _lService.SetActive(bicycleId);
            
            return RedirectToAction(nameof(Index));
        }

        public ActionResult ResetActive(int bicycleId)
        {
            _lService.ResetActive(bicycleId);

            return RedirectToAction(nameof(Index));
        }



    }
}
