using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WAMekaWash.Entities
{
    public class ProviderEntities
    {
        public Int32? ProviderId { set; get; }
        public String BusinessName { set; get; }
        public String Ruc { set; get; }
        public String Telephone { set; get; }
        public String Email { set; get; }
        public Int32? CategoryId { set; get; }
        public String Password { set; get; }
        public String Logo { set; get; }
        public String Description { set; get; }
    }
}

