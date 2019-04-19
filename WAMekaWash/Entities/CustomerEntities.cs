using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WAMekaWash.Entities
{
    public class CustomerEntities
    {
        public Int32? CustomerId { set; get; }
        public String Names { set; get; }
        public String LastNames { set; get; }
        public String DocumentIdentity { set; get; }
        public String Password { set; get; }
        public DateTime Birthday { set; get; }
        public String Username { set; get; }
        public String Status { set; get; }
        public Int32? DepartmentId { set; get; }
        public Int32? ProvinceId { set; get; }
        public Int32? DistrictId { set; get; }
        public String Phone { set; get; }
    }
}