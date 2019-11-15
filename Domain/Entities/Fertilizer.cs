﻿using System;
using System.Collections.Generic;

namespace Rems.Domain.Entities
{
    public class Fertilizer
    {
        public Fertilizer()
        {
            Fertilization = new HashSet<Fertilization>();
        }

        public int FertilizerId { get; set; }

        public string Name { get; set; }

        public double? Nitrogen { get; set; }

        public double? Phosphorus { get; set; }

        public double? Potassium { get; set; }

        public double? Calcium { get; set; }

        public double? Sulfur { get; set; }

        public double? Other { get; set; }

        public string OtherElements { get; set; }

        public string Notes { get; set; }


        public virtual ICollection<Fertilization> Fertilization { get; set; }

    }
}
