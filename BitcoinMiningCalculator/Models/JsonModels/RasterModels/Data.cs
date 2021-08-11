using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitcoinMiningCalculator.Models.JsonModels.RasterModels
{
    public class Data
    {
        public string Title { get; set; }
        public IList<Code> Codes { get; set; }
        public string Country { get; set; }
        public IList<Price> Prices { get; set; }
        public string Time { get; set; }
    }
}
