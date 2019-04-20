using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WAMekaWash.Entities
{
    public class LocalEntities
    {
        public Int32? LocalId { set; get; }
        public String Address { set; get; }
        public Int32? DistrictId { set; get; }
        public Int32? ProvinceId { set; get; }
        public Int32? DepartmentId { set; get; }
        public Int32? ProviderId { set; get; }
        public Int32? Punctuation { set; get; }
        public String Status { set; get; }
        public String Telefono { set; get; }
    }
}