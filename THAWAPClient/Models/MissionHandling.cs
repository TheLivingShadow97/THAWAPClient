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

        public static async Task AddMission(string Name, int ApId, uint baseaddress, ArchipelagoClient client)
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
            DebugWriter.LogMissionDebug("New Mission Location Added, " + Name);
        }

        public static uint GetCurrentBaseMissionAddress()
        {
            uint addy = Memory.ReadUInt(0xd2396c);
            return addy;
        }
        public static uint GetCurrentMissionAddress(uint address)
        {
            uint addy = Memory.ReadUInt(address + 0x18);
            return addy;
        }

        private static CancellationTokenSource? _mfcancel;

        public static void StartMissionFinderLoop(ArchipelagoClient client)
        {
            if (_mfcancel != null)
                {return;} // already running

            _mfcancel = new CancellationTokenSource();
            DebugWriter.LogMissionDebug("Mission Finder Loop Starting");
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

                    await Task.Delay(TimeSpan.FromSeconds(0.5), token);
                }
            }
            // catch (TaskCanceledException)
            // {
            //     // Expected when stopping — no action needed
            // }
            catch (Exception ex)
            {   string exstring = ex.ToString();
                Log.Error(exstring);
                DebugWriter.LogMissionDebug("StartMFLoopAsync Exception: " + exstring);
            }
        }

        public static async Task MissionScan(ArchipelagoClient client)
        { 
            uint baseaddress = GetCurrentBaseMissionAddress();
            uint currentmissionaddress = GetCurrentMissionAddress(baseaddress);
            uint currentmissionresultsaddress = baseaddress + 0x6c; 
            uint scanresult = Memory.ReadUInt(currentmissionaddress);
            if (!alreadyseenresults.Contains(scanresult))
            {   DebugWriter.LogMissionDebug("New Mission Result Found," + scanresult.ToString("X"));
                alreadyseenresults.Add(scanresult);
                if (MissionData.Missions.TryGetValue(scanresult, out var mission))
                {   DebugWriter.LogMissionDebug("Mission Value Matched");
                    await AddMission(mission.Name, mission.ApId, currentmissionresultsaddress, client);
                }
            }
        }
    }
}
