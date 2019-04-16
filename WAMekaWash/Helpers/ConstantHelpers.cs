using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WAMekaWash.Helpers
{
    public class ConstantHelpers
    {
        public const string TOKEN_HEADER_NAME = "Authorization";

        public const int TOKEN_TIMEOUT = 24;

        public static class Status
        {
            public const string ACTIVE = "ACT";
            public const string INACTIVE = "INA";
            public const string FINISH = "FIN";
            public const string CANCEL = "CAN";
        }

    }
}