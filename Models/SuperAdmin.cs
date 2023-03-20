using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.Models
{
    public class SuperAdmin
    {
        public int id { get; set; }

        public int? user_id { get; set; }
        public User user { get; set; }


        public List<Transaction> transactions { get; set; }

    }
}
