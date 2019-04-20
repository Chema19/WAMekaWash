using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WAMekaWash.Entities
{
    public class CarEntities
    {
        public Int32? CarId { set; get; }
        public String Description { set; get; }
        public Int32? BrandId { set; get; }
        public Int32? CustomerId { set; get; }
        public String Placa { set; get; }
    }
}