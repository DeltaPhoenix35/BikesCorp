using BikesTest.Interfaces;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.Models
{
    [FirestoreData]
    public class Location 
    {
        [FirestoreProperty]
        public long Id { get; set; }

        [FirestoreProperty]
        public GeoPoint geoPoint { get; set; }
        
        [FirestoreProperty]
        public DateTime timestamp { get; set; }

        public Location()
        {
        }

        public Location(string _transaction_id, GeoPoint _geoPoint, DateTime _timestamp)
        {
            Id = Int32.Parse(_transaction_id);
            geoPoint = _geoPoint;
            timestamp = _timestamp;
        }
    }
}
