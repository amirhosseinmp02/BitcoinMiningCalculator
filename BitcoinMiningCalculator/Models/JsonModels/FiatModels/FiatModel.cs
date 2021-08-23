using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitcoinMiningCalculator.Models.JsonModels.FiatModels
{
    public class FiatModel
    {
        public string Currency { get; set; }
        public string Price { get; set; }
        public string Changes { get; set; }
        public bool Ok { get; set; }
        public string Source { get; set; }
    }
}
