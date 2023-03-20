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
        public long Id { get; set; }

        public string location { get; set; }

        public DateTime dateTime { get; set; }

    }
}
