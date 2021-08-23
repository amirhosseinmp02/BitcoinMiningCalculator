using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BitcoinMiningCalculator.Models;
using BitcoinMiningCalculator.Models.JsonModels.CoinDeskModels;
using BitcoinMiningCalculator.Models.JsonModels.FiatModels;
using BitcoinMiningCalculator.Models.JsonModels.LastBitcoinBlockModels;
using BitcoinMiningCalculator.Models.JsonModels.RasterModels;
using Newtonsoft.Json;

namespace BitcoinMiningCalculator.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public BitcoinCalculatorViewModel BitcoinCalculatorViewModel { get; set; }
        private readonly HttpClient _httpClient;

        public IndexModel()
        {
            _httpClient = new HttpClient();
        }

        public async Task OnGet()
        {
            var coinDeskResult =
                _httpClient
                    .GetStringAsync("https://api.coindesk.com/v1/bpi/currentprice/btc.json");

            var fiatResult =
                _httpClient
                    .GetStringAsync("https://dapi.p3p.repl.co/api/?currency=usd");

            var lastBitcoinBlockResult =
                _httpClient
                    .GetStringAsync("https://chain.api.btc.com/v3/block/latest");

            var coinDeskModel =
                JsonConvert
                    .DeserializeObject<CoinDeskModel>(await coinDeskResult);

            var fiatModel =
                JsonConvert
                    .DeserializeObject<FiatModel>(await fiatResult);

            var lastBitcoinModel =
                JsonConvert
                    .DeserializeObject<LastBitcoinBlockModel>(await lastBitcoinBlockResult);

            var usdToTomanPrice =
                Convert.ToInt32(fiatModel.Price) / 10;

            var bitcoinToUsdPriceInteger =
                Convert.ToInt32(coinDeskModel.Bpi.USD.Rate_Float);

            var bitcoinToTomanPriceInteger =
                (bitcoinToUsdPriceInteger * usdToTomanPrice);

            BitcoinCalculatorViewModel = new BitcoinCalculatorViewModel()
            {
                BlockReward = 6.25,
                NetworkDifficulty = lastBitcoinModel.Data.Difficulty,
                UsdToTomanPrice = usdToTomanPrice,
                BitcoinToUsdPrice = bitcoinToUsdPriceInteger,
                BitcoinToTomanPrice = bitcoinToTomanPriceInteger
            };
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var electricityConsumedCostForADay =
                (ElectricityCostCalculation(1) / 1000);

                var electricityConsumedCostForAWeek =
                    electricityConsumedCostForADay * 7;

                var electricityConsumedCostForAMonth =
                    electricityConsumedCostForADay * 30;

                var electricityConsumedCostForAYear =
                    electricityConsumedCostForADay * 365;

                var bitcoinADayIncome =
                    BitcoinIncomeCalculation(1);

                var bitcoinAWeekIncome =
                    bitcoinADayIncome * 7;

                var bitcoinAMonthIncome =
                    bitcoinADayIncome * 30;

                var bitcoinAYearIncome =
                    bitcoinADayIncome * 365;

                var model = new
                {
                    ElectricityConsumedCostForADay = electricityConsumedCostForADay.ToString("##,###"),
                    ElectricityConsumedCostForAWeek = electricityConsumedCostForAWeek.ToString("##,###"),
                    ElectricityConsumedCostForAMonth = electricityConsumedCostForAMonth.ToString("##,###"),
                    ElectricityConsumedCostForAYear = electricityConsumedCostForAYear.ToString("##,###"),
                    BitcoinADayIncome = bitcoinADayIncome > 0 ? bitcoinADayIncome.ToString().Substring(0, 8) : "0",
                    BitcoinAWeekIncome = bitcoinAWeekIncome > 0 ? bitcoinAWeekIncome.ToString().Substring(0, 8) : "0",
                    BitcoinAMonthIncome =
                        bitcoinAMonthIncome > 0 ? bitcoinAMonthIncome.ToString().Substring(0, 8) : "0",
                    BitcoinAYearIncome = bitcoinAYearIncome > 0 ? bitcoinAYearIncome.ToString().Substring(0, 8) : "0",
                    UsdADayIncome = (BitcoinCalculatorViewModel.BitcoinToUsdPrice * bitcoinADayIncome).ToString("F"),
                    UsdAWeekIncome = (BitcoinCalculatorViewModel.BitcoinToUsdPrice * bitcoinAWeekIncome).ToString("F"),
                    UsdAMonthIncome = (BitcoinCalculatorViewModel.BitcoinToUsdPrice * bitcoinAMonthIncome).ToString("F"),
                    UsdAYearIncome = (BitcoinCalculatorViewModel.BitcoinToUsdPrice * bitcoinAYearIncome).ToString("F"),
                    FinalADayIncomeInToman =
                        ((BitcoinCalculatorViewModel.BitcoinToTomanPrice * bitcoinADayIncome) -
                                    electricityConsumedCostForADay).ToString("##,###"),
                    FinalAWeekIncomeInToman =
                       ((BitcoinCalculatorViewModel.BitcoinToTomanPrice * bitcoinAWeekIncome) -
                                    electricityConsumedCostForAWeek).ToString("##,###"),
                    FinalAMonthIncomeInToman =
                        ((BitcoinCalculatorViewModel.BitcoinToTomanPrice * bitcoinAMonthIncome) -
                                    electricityConsumedCostForAMonth).ToString("##,###"),
                    FinalAYearIncomeInToman =
                        ((BitcoinCalculatorViewModel.BitcoinToTomanPrice * bitcoinAYearIncome) -
                                    electricityConsumedCostForAYear).ToString("##,###"),
                };

                return new JsonResult(model);
            }

            return BadRequest();
        }

        //Utilities 

        private decimal ElectricityCostCalculation(int day)
        {
            var electricityCost =
                ((BitcoinCalculatorViewModel.ElectricityConsumed *
                  BitcoinCalculatorViewModel.CostPerKilowattOfElectricity) * (24 * day));

            return electricityCost ?? 0;
        }

        private decimal BitcoinIncomeCalculation(int day)
        {
            var income =
                ((day) * ((BitcoinCalculatorViewModel.HashRate * Math.Pow(10, 12)) * BitcoinCalculatorViewModel.BlockReward * 86400) /
                 (BitcoinCalculatorViewModel.NetworkDifficulty * Math.Pow(2, 32)));

            var poolCommission =
                ((income * BitcoinCalculatorViewModel.PoolFee) / 100);

            var incomeIncludingCommissions =
                income - poolCommission;

            decimal finalIncome;

            decimal
                .TryParse(incomeIncludingCommissions.ToString(), NumberStyles.Float, null, out finalIncome);

            return finalIncome;
        }
    }
}
