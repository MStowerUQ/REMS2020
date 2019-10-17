﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Functions
{
    public class WeightedTemperatureFunction : ApsimNode
    {
        public double MaximumTemperatureWeighting { get; set; } = default;

        public WeightedTemperatureFunction()
        { }
    }
}
