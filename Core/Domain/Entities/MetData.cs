﻿using Rems.Domain.Attributes;
using Rems.Domain.Interfaces;
using System;

namespace Rems.Domain.Entities
{
    [ExcelFormat("Data", 1, false, "MetData", "Met")]
    public class MetData : IEntity, IValue
    {
        public int MetStationId { get; set; }

        public int TraitId { get; set; }

        [Expected("Date")]
        public DateTime Date { get; set; }

        public double Value { get; set; }

        [Expected("MetStation", "MetStation Name")]
        public virtual MetStation MetStation { get; set; }
        public virtual Trait Trait { get; set; }        
    }
}
