using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WAMekaWash.Entities
{
    public class ReservationEntities
    {
        public Int32? ReservationId { set; get; }
        public Int32? CustomerId { set; get; }
        public Int32? LocalId { set; get; }
        public Int32? ServiceId { set; get; }
        public TimeSpan Schedule { set; get; }
        public String Detail { set; get; }
        public String Status { set; get; }
    }
}