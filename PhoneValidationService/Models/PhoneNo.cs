using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneValidationService.Models
{
    public class PhoNo
    {
        public string phone { get; set; }
        public  ErrorStatus err { get; set;}
    }

    public class ErrorStatus
    {
        public string Statuscode { get; set; }
        public string message { get; set; }
        public bool status { get; set; }
    }

    //public class PhonoGet
    //{
    //    public string phone { get; set; }
    //    public string ErrorStatus eer{g
    //}
}