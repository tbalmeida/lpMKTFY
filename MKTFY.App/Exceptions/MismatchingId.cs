using System;

namespace MKTFY.App.Exceptions
{

    public class MismatchingId : Exception
    {
        private readonly string _id;
        
        public MismatchingId(string id, string message = "The id provided does not match, please check the URL and the form data.") : base(message)
        {
            _id = id;
        }

        public string Id
        {
            get
            {
                return _id.ToString();
            }
        }

    }
}
