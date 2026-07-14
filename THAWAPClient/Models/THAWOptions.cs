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

        public TonyHawkOptions(Dictionary<string, object> optionsDict, Dictionary<string, object> slotData)
        {
            if (App.Client.Options.ContainsKey("end_goal"))
                ChosenGoal = ((JsonElement)App.Client.Options["end_goal"]).GetUInt32();
            else
                ChosenGoal = 0;

            if (App.Client.Options.ContainsKey("tricks_4_cash"))
                TrickCashing = GetBool("tricks_4_cash");
            else
                TrickCashing = false;

        }

        internal bool GetBool(string str)
        {
            if (App.Client.Options.ContainsKey(str))
            {
                if (((JsonElement)App.Client.Options[str]).GetUInt32() > 0)
                    return true;
            }
            return false;
        }
    }
}