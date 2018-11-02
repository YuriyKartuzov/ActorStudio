using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Studio.Controllers
{
    public class ActingAdd
    {
        public string Title {get; set;}
    }

    public class ActingBase : ActingAdd
    {
        public int Id { get; set; }
    }


}