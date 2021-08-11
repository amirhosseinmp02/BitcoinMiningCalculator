using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitcoinMiningCalculator.Models.JsonModels.CoinDeskModels
{
    public class USD
    {
        public string Code { get; set; }
        public string Rate { get; set; }
        public string Description { get; set; }
        public double Rate_Float { get; set; }  
    }
}
