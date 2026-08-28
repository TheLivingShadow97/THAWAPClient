using Serilog;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace THAWAPClient.Models
{
    public class TonyHawkOptions
    {
        public uint ChosenGoal { get; set; }
        public bool TrickCashing { get; set; }
        public bool ProgressiveWallet { get; set; }
        public bool Shopsanity { get; set; }
        //public bool ShopKeys { get; set; }
        public bool SkateboardIncludedInItemPool { get; set; }
        public uint DeathlinkSettings { get; set; }

        public TonyHawkOptions(Dictionary<string, object> optionsDict, Dictionary<string, object> slotData)
        {
            if (App.Client.Options.ContainsKey("end_goal"))
                {ChosenGoal = ((JsonElement)App.Client.Options["end_goal"]).GetUInt32();}
            else
                {ChosenGoal = 0;}

            if (App.Client.Options.ContainsKey("tricks_4_cash"))
                {TrickCashing = GetBool("tricks_4_cash");}
            else
                {TrickCashing = false;}
            if (App.Client.Options.ContainsKey("progressive_wallet"))
                {ProgressiveWallet = GetBool("progressive_wallet");}
            else
                {ProgressiveWallet = false;}
            if (App.Client.Options.ContainsKey("shopsanity"))
                {Shopsanity = GetBool("shopsanity");}
            else
                {Shopsanity = false;}
            // if (App.Client.Options.ContainsKey("shop_keys"))
            //     ShopKeys = GetBool("shop_keys");
            // else
            //     ShopKeys = false;
            if (App.Client.Options.ContainsKey("include_skateboard_in_item_pool"))
                {SkateboardIncludedInItemPool = GetBool("include_skateboard_in_item_pool");}
            else
                {SkateboardIncludedInItemPool = false;}
            if (App.Client.Options.ContainsKey("deathlink_choice"))
                {DeathlinkSettings = ((JsonElement)App.Client.Options["deathlink_choice"]).GetUInt32();}
            else
                {DeathlinkSettings = 0;}
        }

        internal bool GetBool(string str)
        {
            if (App.Client.Options.ContainsKey(str))
            {
                if (((JsonElement)App.Client.Options[str]).GetUInt32() > 0)
                    {return true;}
            }
            return false;
        }

        public enum EndGoal
        {
            option_smash_the_t_rex = 0,
            option_get_to_the_skate_ranch = 1,
            option_win_the_skate_competition = 2,
        }
        public enum DeathlinkChoice
        {
            option_off = 0,
            option_1_bail = 1,
            option_5_bails = 2,
            option_10_bails = 3,
            option_15_bails = 4,
            option_20_bails = 5,
        }
    }
}