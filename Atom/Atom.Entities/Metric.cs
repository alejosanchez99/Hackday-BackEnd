﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atom.Entities
{
    public class Metric
    {
        public string title { get; set; }
        public int count { get; set; }
        public int sum { get; set; }
        public int min { get; set; }
        public int max { get; set; }
    }
}
