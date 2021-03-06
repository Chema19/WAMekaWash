﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WAMekaWash.Entities
{
    public class ServiceEntities
    {
        public Int32? ServiceId { set; get; }
        public String Name { set; get; }
        public String Detail { set; get; }
        public Int32? LocalId { set; get; }
        public String UrlPhoto { set; get; }
        public Decimal? Cost { set; get; }
        public String Status { set; get; }
    }
}