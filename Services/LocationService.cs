using BikesTest.Interfaces;
using BikesTest.Models;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Google.Cloud.Firestore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.Services
{
    public class LocationService : ILocationService
    {
        private readonly IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "iCprAJzqdlLz0mou7g8j6D2eOJFzU041F0dfbdI5",
            BasePath = "https://maps2018-415d9-default-rtdb.europe-west1.firebasedatabase.app"
        };
        private IFirebaseClient db;


        public List<Location> GetAll(int transactionId, int bicycleId)
        {
            db = new FireSharp.FirebaseClient(config);

            FirebaseResponse response = db.Get(bicycleId + "/" + transactionId);

            dynamic collection = JsonConvert.DeserializeObject<dynamic>(response.Body);

            var list = new List<Location>();
            if (collection != null)
            {
                foreach (var item in collection)
                {
                    list.Add(JsonConvert.DeserializeObject<Location>(((JProperty)item).Value.ToString()));
                }
            }

            return list;
        }

        public Location SetActive(int bicycleId)
        {
            db = new FireSharp.FirebaseClient(config);
            SetResponse response = db.Set(bicycleId + "/active", "1");
            return new Location();
        }

        public Location UpdateLastTransactionId(int bicycleId, int transactionId)
        {
            db = new FireSharp.FirebaseClient(config);
            SetResponse response = db.Set(bicycleId + "/lastTransaction", transactionId.ToString());
            return new Location();
        }

        public Location ResetActive(int bicycleId)
        {
            db = new FireSharp.FirebaseClient(config);
            SetResponse response = db.Set(bicycleId + "/active", "0");
            return new Location();
        }
    }
}
