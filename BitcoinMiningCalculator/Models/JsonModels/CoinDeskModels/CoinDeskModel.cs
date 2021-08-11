using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitcoinMiningCalculator.Models.JsonModels.CoinDeskModels
{
    public class CoinDeskModel
    {
        public Time Time { get; set; }
        public string Disclaimer { get; set; }
        public Bpi Bpi { get; set; }
    }
}
