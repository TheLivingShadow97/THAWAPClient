using Archipelago.Core.Util;
using THAWAPClient.Helpers;
using Archipelago.Core.Models;
using Archipelago.Core;
using Serilog;

namespace THAWAPClient.Models
{
    public class MissionHandling
    {
        public static HashSet<uint> alreadyseenresults = new HashSet<uint>();

        public static void AddMission(string Name, int ApId, uint baseaddress, ArchipelagoClient client)
        {
            Location newlocation = new Location()
            {
                Id = ApId,
                Name = Name,
                Address = baseaddress,
                AddressBit = 2,
                CheckType = LocationCheckType.Bit,
                Category = "Missions"
            };

            client.LocationManager.AddLocation(newlocation);
            //debug Log.Logger.Warning("New Mission Location Added, " + Name);
        }

        public static uint GetCurrentMissionResultsAddress()
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

        private static CancellationTokenSource? _mfcancel;

        public static void StartMissionFinderLoop(ArchipelagoClient client)
        {
            if (_mfcancel != null)
                return; // already running

            _mfcancel = new CancellationTokenSource();
            _ = StartMFLoopAsync(_mfcancel.Token, client);
        }

        public static void StopMFLoop()
        {
            _mfcancel?.Cancel();
            _mfcancel = null;
        }

        public static async Task StartMFLoopAsync(CancellationToken token, ArchipelagoClient client)
        {
            try
            {
                while (!token.IsCancellationRequested)
                {
                    await MissionScan(client);

                    await Task.Delay(TimeSpan.FromSeconds(1), token);
                }
            }
            // catch (TaskCanceledException)
            // {
            //     // Expected when stopping — no action needed
            // }
            catch (Exception ex)
            {   string exstring = ex.ToString();
                Log.Error(exstring);
            }
        }

        public static async Task MissionScan(ArchipelagoClient client)
        { 
            uint currentmissionaddress = GetCurrentMissionAddress();
            uint currentmissionresultsaddress = GetCurrentMissionResultsAddress(); 
            uint scanresult = Memory.ReadUInt(currentmissionaddress);
            if (!alreadyseenresults.Contains(scanresult))
            {   //debug Log.Logger.Warning("New Mission Result Found," + scanresult.ToString("X"));
                alreadyseenresults.Add(scanresult);
                if (MissionData.Missions.TryGetValue(scanresult, out var mission))
                {   //debug Log.Logger.Warning("Mission Value Matched");
                    AddMission(mission.Name, mission.ApId, currentmissionresultsaddress, client);
                }
            }
        }
    }
}
