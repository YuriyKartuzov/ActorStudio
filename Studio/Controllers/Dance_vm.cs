using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Studio.Controllers
{
    public class DanceAdd
    {
        public string Title { get; set; }
    }

    public class DanceBase : DanceAdd
    {
        public int Id { get; set; }
    }
}