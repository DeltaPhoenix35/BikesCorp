using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.Exceptions
{
    public class SubscriptionPlanIsDeleted : InvalidOperationException
    {
        public SubscriptionPlanIsDeleted()
        {

        }
        public SubscriptionPlanIsDeleted(string message) : base(message)
        {

        }
        public SubscriptionPlanIsDeleted(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
