using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApp.Core.Enums
{
    public enum TimedEventType
    {
        FixedPrice,
        ReducePriceByAmount,
        ReducePriceByPercentage,
        IncreasePriceByAmount,
        IncreasePriceByPercentage
    }
}