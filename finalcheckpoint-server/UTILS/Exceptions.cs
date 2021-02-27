using System;

namespace finalcheckpoint_server.Exceptions
{
    public class Forbidden : Exception
    {
        public Forbidden(string message) : base(message)
        {

        }
    }
}