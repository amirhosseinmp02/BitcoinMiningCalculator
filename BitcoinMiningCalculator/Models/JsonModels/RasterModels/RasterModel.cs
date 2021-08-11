using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitcoinMiningCalculator.Models.JsonModels.RasterModels
{
    public class RasterModel
    {
        public string Message { get; set; }
        public int Status { get; set; }
        public Data Data { get; set; }
    }
}
