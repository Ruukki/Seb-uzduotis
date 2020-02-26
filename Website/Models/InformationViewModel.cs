using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL.Model;

namespace Website.Models
{
    public class InformationViewModel
    {
        public Agreement Agreement { get; set; }
        public Customer Customer { get; set; }
        public decimal OldIntrestRate { get; set; }
        public decimal NewInteresRate { get; set; }
        public decimal IntrestRateDiff { get; set; }

        public string Error { get; set; }
    }
}