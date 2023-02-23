﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.Exceptions
{
    public class CurrentlyUsedException : InvalidOperationException
    {
        public CurrentlyUsedException()
        {

        }
        public CurrentlyUsedException(string message) : base(message)
        {

        }
        public CurrentlyUsedException(string message, Exception inner) : base(message, inner)
        {

        }

    }
}