using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace UpdateSpreadsheets.Models
{
    public class DoppleAPI
    {
        // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
        [JsonPropertyName("dopple_price_usd")]
        public double DopplePriceUsd { get; set; }

        [JsonPropertyName("staking_apy")]
        public StakingApyModel StakingApy { get; set; }

        [JsonPropertyName("pool")]
        public PoolModel Pool { get; set; }

        [JsonPropertyName("dopple_data")]
        public DoppleDataModel DoppleData { get; set; }

        [JsonPropertyName("total_value_lock")]
        public TotalValueLockModel TotalValueLock { get; set; }

        [JsonPropertyName("latest_update")]
        public DateTime LatestUpdate { get; set; }
        public class StakingApyModel
        {
            [JsonPropertyName("cake_lp_apy")]
            public double CakeLpApy { get; set; }

            [JsonPropertyName("dop_lp_apy")]
            public double DopLpApy { get; set; }

            [JsonPropertyName("busd_lp_apy")]
            public double BusdLpApy { get; set; }

            [JsonPropertyName("dop_apy")]
            public double DopApy { get; set; }

            [JsonPropertyName("two_pool_lp_apy")]
            public double TwoPoolLpApy { get; set; }

            [JsonPropertyName("ust_lp_apy")]
            public double UstLpApy { get; set; }

            [JsonPropertyName("dolly_lp_apy")]
            public double DollyLpApy { get; set; }

            [JsonPropertyName("dolly_cake_lp_apy")]
            public double DollyCakeLpApy { get; set; }
        }

        public class _0x5162f992EDF7101637446ecCcD5943A9dcC63A8A
        {
            [JsonPropertyName("apy")]
            public double Apy { get; set; }

            [JsonPropertyName("balance")]
            public double Balance { get; set; }

            [JsonPropertyName("trading_volume")]
            public double TradingVolume { get; set; }
        }

        public class _0x830e287ac5947B1C0DA865dfB3Afd7CdF7900464
        {
            [JsonPropertyName("apy")]
            public double Apy { get; set; }

            [JsonPropertyName("balance")]
            public double Balance { get; set; }

            [JsonPropertyName("trading_volume")]
            public double TradingVolume { get; set; }
        }

        public class _0x449256e20ac3ed7F9AE81c2583068f7508d15c02
        {
            [JsonPropertyName("apy")]
            public double Apy { get; set; }

            [JsonPropertyName("balance")]
            public double Balance { get; set; }

            [JsonPropertyName("trading_volume")]
            public double TradingVolume { get; set; }
        }

        public class _0x61f864a7dFE66Cc818a4Fd0baabe845323D70454
        {
            [JsonPropertyName("apy")]
            public double Apy { get; set; }

            [JsonPropertyName("balance")]
            public double Balance { get; set; }

            [JsonPropertyName("trading_volume")]
            public double TradingVolume { get; set; }
        }

        public class PoolModel
        {
            [JsonPropertyName("0x5162f992EDF7101637446ecCcD5943A9dcC63A8A")]
            public _0x5162f992EDF7101637446ecCcD5943A9dcC63A8A _0x5162f992EDF7101637446ecCcD5943A9dcC63A8A { get; set; }

            [JsonPropertyName("0x830e287ac5947B1C0DA865dfB3Afd7CdF7900464")]
            public _0x830e287ac5947B1C0DA865dfB3Afd7CdF7900464 _0x830e287ac5947B1C0DA865dfB3Afd7CdF7900464 { get; set; }

            [JsonPropertyName("0x449256e20ac3ed7F9AE81c2583068f7508d15c02")]
            public _0x449256e20ac3ed7F9AE81c2583068f7508d15c02 _0x449256e20ac3ed7F9AE81c2583068f7508d15c02 { get; set; }

            [JsonPropertyName("0x61f864a7dFE66Cc818a4Fd0baabe845323D70454")]
            public _0x61f864a7dFE66Cc818a4Fd0baabe845323D70454 _0x61f864a7dFE66Cc818a4Fd0baabe845323D70454 { get; set; }
        }

        public class DoppleDataModel
        {
            [JsonPropertyName("dopple_price_usd")]
            public double DopplePriceUsd { get; set; }

            [JsonPropertyName("total_trading_volume")]
            public double TotalTradingVolume { get; set; }

            [JsonPropertyName("dopple_total_supply")]
            public double DoppleTotalSupply { get; set; }

            [JsonPropertyName("dopple_max_supply")]
            public double DoppleMaxSupply { get; set; }

            [JsonPropertyName("dopple_per_block")]
            public double DopplePerBlock { get; set; }

            [JsonPropertyName("market_cap")]
            public double MarketCap { get; set; }
        }

        public class TvlListModel
        {
            [JsonPropertyName("dopple_bnb")]
            public double DoppleBnb { get; set; }

            [JsonPropertyName("dopple_busd")]
            public double DoppleBusd { get; set; }

            [JsonPropertyName("dopply_dolly")]
            public double DopplyDolly { get; set; }

            [JsonPropertyName("dop")]
            public double Dop { get; set; }

            [JsonPropertyName("stable_coin_pool")]
            public double StableCoinPool { get; set; }

            [JsonPropertyName("ust_pool")]
            public double UstPool { get; set; }

            [JsonPropertyName("two_pool")]
            public double TwoPool { get; set; }

            [JsonPropertyName("dollpy_pool")]
            public double DollpyPool { get; set; }
        }

        public class TotalValueLockModel
        {
            [JsonPropertyName("tvl_list")]
            public TvlListModel TvlList { get; set; }

            [JsonPropertyName("tvl_sum")]
            public double TvlSum { get; set; }
        }
    }
}
