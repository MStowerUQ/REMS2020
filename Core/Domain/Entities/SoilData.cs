﻿using System;
using Rems.Domain.Attributes;

namespace Rems.Domain.Entities
{
    public class SoilData : ITrait
    {
       public int SoilDataId { get; set; }

        public int PlotId { get; set; }

        public int TraitId { get; set; }

        public DateTime? Date { get; set; }

        public double? Value { get; set; }


        public virtual Plot Plot { get; set; }
        public virtual Trait Trait { get; set; }

    }
}
