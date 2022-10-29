using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atom.Entities
{
    public class RequestMetric
    {
        public string NameListener { get; set; }
        public List<Metric> Metrics { get; set; }
    }
}
