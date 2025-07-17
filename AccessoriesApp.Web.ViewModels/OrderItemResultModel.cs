using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessoriesApp.Web.ViewModels
{
    public class OrderItemResultModel
    {
        public int rownumberorderitem { get; set; }
        public int rownumberorder { get; set; }
        public decimal totalpricebgn { get; set; }
    }
}
