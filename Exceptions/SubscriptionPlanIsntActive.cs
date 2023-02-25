using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.Exceptions
{
    public class SubscriptionPlanIsntActive : InvalidOperationException
    {
        public SubscriptionPlanIsntActive()
        {

        }
        public SubscriptionPlanIsntActive(string message) : base(message)
        {

        }
        public SubscriptionPlanIsntActive(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
