﻿using System;

namespace MKTFY.App.Exceptions
{
    public class NotFoundException : Exception
    {
        private readonly string _id;

        public NotFoundException(string message, string id) : base(message) 
        {
            _id = id;
        }

        public string id
        {
            get
            {
                return _id.ToString();
            }
        }
    }
}