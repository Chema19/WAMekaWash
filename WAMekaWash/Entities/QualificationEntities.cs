using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WAMekaWash.Entities
{
    public class QualificationEntities
    {
        public Int32 QualificationId { set; get; }
        public Int32 Punctuation { set; get; }
        public String Detail { set; get; }
        public Int32 CustomerId { set; get; }
        public Int32? LocalId { set; get; }
    }
}