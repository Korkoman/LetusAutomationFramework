using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaywrightTests.Models
{
    public  class AppSettings
    {
        
        public string BaseUrl { get; set; }
        public string Browser { get; set; }
        public bool Headless { get; set; }
        public int Timeout { get; set; }
        public int SlowMo { get; set; }
        public bool IgnoreHttpsErrors { get; set; }        
    }

   
}

