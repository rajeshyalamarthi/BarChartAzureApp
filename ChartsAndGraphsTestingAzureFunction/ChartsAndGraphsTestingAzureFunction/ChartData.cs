using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartsAndGraphsTestingAzureFunction
{
    public class ChartData
    {
        public string type { get; set; }
        public ArrayList xvals { get; set; }
        public ArrayList yvals { get; set; }
    }
}
