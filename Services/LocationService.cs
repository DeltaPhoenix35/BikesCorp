using BikesTest.Interfaces;
using BikesTest.Models;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.Services
{
    public class LocationService : ILocationService
    {
        FirestoreDb db = FirestoreDb.Create("maps2018-415d9");

        public async Task<List<Location>> GetAll(int transactionId, int bicycleId)
        {
            var collection = await db.Collection("locations").Document(bicycleId.ToString()).Collection(transactionId.ToString()).OrderBy("recordTime").GetSnapshotAsync();
            List<Location> locations = new List<Location>();

            foreach (DocumentSnapshot documentSnapshot in collection.Documents)
            {
                if (documentSnapshot.Exists)
                {
                    Dictionary<string, object> obj = documentSnapshot.ToDictionary();

                    Location loc = new Location();

                    loc.Id = (long)obj["transaction_id"];
                    loc.timestamp = ((Timestamp)obj["recordTime"]).ToDateTime();
                    loc.geoPoint = (GeoPoint)obj["location"];

                    locations.Add(loc);
                }
            }

            return locations;
        }

        public async Task<long> GetLastTransactionId(int bicycleId)
        {
            var document = await db.Collection("locations").Document(bicycleId.ToString()).Collection("0").Document("lastTransactionId").GetSnapshotAsync();
            if (document.Exists)
            {
                var obj = document.ToDictionary();
                return (long)obj["lastTransaction"];
            }
            return 0;
        }
    }
}
