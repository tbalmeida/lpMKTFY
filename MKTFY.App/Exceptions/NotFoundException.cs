﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MKTFY.App.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }
}
