using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace POSApp.Core.ViewModels
{
    public class DashBoardViewModel
    {
        [DefaultValue(0)]
        public decimal Sales { get; set; }
        [DefaultValue(0)]
        public double Expenses { get; set; }
        [DefaultValue(0)]
        public int Orders { get; set; }
        [DefaultValue(0)]
        public decimal Refunds { get; set; }

    }

    public class GraphViewModel
    {
        public List<MorrisGraphViewModel> Morris { get; set; }
        public List<LineGraphViewModel> Line { get; set; }

    }

    public class MorrisGraphViewModel
    {
        public string y { get; set; }
        public decimal a { get; set; }
        public double b { get; set; }
    }

    public class LineGraphViewModel
    {
        public string x { get; set; }
        public decimal y { get; set; }
    }
}