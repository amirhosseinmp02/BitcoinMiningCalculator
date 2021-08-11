using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitcoinMiningCalculator.Models.JsonModels.CoinDeskModels
{
    public class BTC
    {
        public string Code { get; set; }
        public string Rate { get; set; }
        public string Description { get; set; }
        public int RateFloat { get; set; }
    }
}
