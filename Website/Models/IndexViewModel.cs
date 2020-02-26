using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL.Model;

namespace Website.Models
{
    public class IndexViewModel
    {
        public List<Agreement> Agreements { get; set; }
        public string BaseCode { get; set; }

        public List<string> BaseRateCodes
        {
            get { return new List<string> {"VILIBOR1m", "VILIBOR3m", "VILIBOR6m", "VILIBOR1y"}; }
        }
    }
}