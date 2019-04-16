using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WAMekaWash.Entities
{
    public class ResultRequest
    {
        public Object Data { set; get; }
        public Boolean Error { set; get; }
        public String Message { set; get; }
    }
}