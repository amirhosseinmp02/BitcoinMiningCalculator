using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitcoinMiningCalculator.Models.JsonModels.LastBitcoinBlockModels
{
    public class LastBitcoinBlockModel
    {
        public Data Data { get; set; }
        public int ErrCode { get; set; }
        public int ErrNo { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
    }
}
