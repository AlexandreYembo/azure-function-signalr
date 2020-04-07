using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerlessPoc.Tests
{
    public static class Stub
    {
        public static ServerlessPoc.Function.Model.Resquest GetMessageRequest()
        {
            return new Function.Model.Resquest
            {
                Type = "message",
                User = "Test User",
                Candidate = "",
                Message = "Hello World"
            };
        }

        public static ServerlessPoc.Function.Model.Resquest GetVoteRequest()
        {
            return new Function.Model.Resquest
            {
                Type = "vote",
                User = "Test User",
                Candidate = "jair",
                Message = "Hello World"
            };
        }
    }
}