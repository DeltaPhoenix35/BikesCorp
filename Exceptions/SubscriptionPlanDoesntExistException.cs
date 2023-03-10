using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.Exceptions
{
    public class SubscriptionPlanDoesntExistException : InvalidOperationException
    {
        public SubscriptionPlanDoesntExistException()
        {

        }
        public SubscriptionPlanDoesntExistException(string message) : base(message)
        {

        }
        public SubscriptionPlanDoesntExistException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
