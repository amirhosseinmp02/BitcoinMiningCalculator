using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitcoinMiningCalculator.Models
{
    public class BitcoinCalculatorViewModel
    {
        public double HashRate { get; set; }
        public decimal ElectricityConsumed { get; set; }
        public decimal CostPerKilowattOfElectricity { get; set; }
        public double PoolFee { get; set; }
        public double BlockReward { get; set; }
        public long NetworkDifficulty { get; set; }
        public long BitcoinToUsdPrice { get; set; }
        public long BitcoinToTomanPrice { get; set; }
        public int UsdToTomanPrice { get; set; }    
    }
}
