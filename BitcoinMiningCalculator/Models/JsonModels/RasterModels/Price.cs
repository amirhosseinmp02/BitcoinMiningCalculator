using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitcoinMiningCalculator.Models.JsonModels.RasterModels
{
    public class Price
    {
        public string Live { get; set; }
        public string Change { get; set; }
        public string Min { get; set; }
        public string Max { get; set; }
    }
}
