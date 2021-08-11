using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitcoinMiningCalculator.Models.JsonModels.LastBitcoinBlockModels
{
    public class Data
    {
        public int Height { get; set; }
        public int Version { get; set; }
        public string MrklRoot { get; set; }
        public int Timestamp { get; set; }
        public int Bits { get; set; }
        public long Nonce { get; set; }
        public string Hash { get; set; }
        public string PrevBlockHash { get; set; }
        public string NextBlockHash { get; set; }
        public int Size { get; set; }
        public long PoolDifficulty { get; set; }
        public long Difficulty { get; set; }
        public double DifficultyDouble { get; set; }
        public int TxCount { get; set; }
        public int RewardBlock { get; set; }
        public int RewardFees { get; set; }
        public int Confirmations { get; set; }
        public bool IsOrphan { get; set; }
        public int CurrMaxTimestamp { get; set; }
        public bool IsSwBlock { get; set; }
        public int StrippedSize { get; set; }
        public int Sigops { get; set; }
        public int Weight { get; set; }
        public Extras Extras { get; set; }
    }
}
