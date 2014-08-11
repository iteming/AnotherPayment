using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnotherPayment.Models
{
    public class Payment
    {
        public List<string> operate { get; set; }

        public List<string> mobile { get; set; }

        public string status { get; set; }

        //{"operate":["submit"],"mobile":["3875492013"],"status":1}
    }
}