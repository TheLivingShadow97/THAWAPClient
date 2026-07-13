using Serilog;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace THAWAPClient.Models
{
    public class TonyHawkOptions
    {
        public uint ChosenGoal { get; set; }

        public TonyHawkOptions(Dictionary<string, object> optionsDict, Dictionary<string, object> slotData)
        {
            if (App.Client.Options.ContainsKey("end_goal"))
                ChosenGoal = ((JsonElement)App.Client.Options["end_goal"]).GetUInt32();
            else
                ChosenGoal = 0;
        }
    }
}