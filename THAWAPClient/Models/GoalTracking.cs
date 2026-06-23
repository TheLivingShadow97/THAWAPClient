using Archipelago.Core;
using Archipelago.Core.Util;
using System.Text.Json;
using Serilog;

namespace THAWAPClient.Models
{
    public class GoalTracking
    {
        private static bool _goalSent = false;
        public record GoalInfo(string Name);
        public static Dictionary<uint, GoalInfo> CurrentGoal = new();
        public static HashSet<uint> seenresults = new HashSet<uint>();

        public static void DeriveGoal(ArchipelagoClient Client)
        {
            if (Client == null) return;
            if (Client?.Options.ContainsKey("end_goal") == true
                 && ((JsonElement)Client.Options["end_goal"]).Deserialize<int>() == 0)
            {
                CurrentGoal.Add(0x2E30A2B8, new GoalInfo("Smash the T-Rex"));
            }
        }

        public async static Task MonitorGoal( uint baseaddress, ArchipelagoClient client)
        {
            await Memory.MonitorAddressBitForAction(baseaddress, 2, () => CheckGoal(baseaddress, client));
        }

        async public static void CheckGoal(uint goalcompletionaddress, ArchipelagoClient client)
        {
            bool goalresult = Memory.ReadBit(goalcompletionaddress, 2);
            if (goalresult)
            {    
                SendGoal(client);
            }
            else
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
                await MonitorGoal(goalcompletionaddress, client);
            }
        }

        public static void SendGoal(ArchipelagoClient Client)
        {
            Task.Run(async () =>
            { 
                if (!_goalSent)
                {
                    Client.SendGoalCompletion();
                    Log.Logger.Warning("Goal sent.");
                    Client.AddOverlayMessage($"Goal sent.");
                }
                else
                    Log.Logger.Information("Goal already sent.");
                    _goalSent = true;
            });
        }

        public static uint GetCurrentGoalResultsAddress()
        {
            uint addy = Memory.ReadUInt(0xd2396c) + 0x6c;
            return addy;
        }
        public static uint GetCurrentMissionAddress()
        {
            uint addy = Memory.ReadUInt(0xd2396c) + 0x18;
            addy = Memory.ReadUInt(addy);
            return addy;
        }

        private static CancellationTokenSource? _gfcancel;

        public static void StartGoalFinderLoop(ArchipelagoClient client)
        {
            if (_gfcancel != null)
                return; // already running

            _gfcancel = new CancellationTokenSource();
            _ = StartGFLoopAsync(_gfcancel.Token, client);
        }

        public static void StopGFLoop()
        {
            _gfcancel?.Cancel();
            _gfcancel = null;
        }

        public async static Task StartGFLoopAsync(CancellationToken token, ArchipelagoClient client)
        {
            try
            {
                while (!token.IsCancellationRequested)
                {
                    await MissionScan(client);

                    await Task.Delay(TimeSpan.FromSeconds(1), token);
                }
            }
            catch (TaskCanceledException)
            {
                // Expected when stopping — no action needed
            }
        }

        public async static Task MissionScan(ArchipelagoClient client)
        { 
            uint currentmissionaddress = GetCurrentMissionAddress();
            uint currentmissionresultsaddress = GetCurrentGoalResultsAddress(); 
            uint scanresult = Memory.ReadUInt(currentmissionaddress);
            if (!seenresults.Contains(scanresult))
            {
                seenresults.Add(scanresult);
                if (CurrentGoal.TryGetValue(scanresult, out var mission))
                {
                    await MonitorGoal(currentmissionresultsaddress, client);
                    StopGFLoop();
                }
            }
        }
    }
}