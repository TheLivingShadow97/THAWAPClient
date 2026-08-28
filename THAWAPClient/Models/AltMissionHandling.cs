using Archipelago.Core.Util;
using THAWAPClient.Helpers;
using Archipelago.Core.Models;
using Archipelago.Core;
using Serilog;
using System;
using System.Threading;
using Avalonia.Input;
using System.Net.Sockets;

namespace THAWAPClient.Models
{
    public class MissionReader
    {
        public required ArchipelagoClient _Client;
        public bool _shouldstop = false;
        public static HashSet<uint> alreadyseenaltresults = new HashSet<uint>();

        public void RequestStop()
        {
            _shouldstop = true;
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
        public void MissionScan()
        {
            while (!_shouldstop)
            { 
                uint baseaddress = GetCurrentBaseMissionAddress();
                uint currentmissionaddress = GetCurrentMissionAddress(baseaddress);
                uint scanresult = Memory.ReadUInt(currentmissionaddress);
                if (!alreadyseenaltresults.Contains(scanresult))
                {   DebugWriter.LogMissionDebug("New Mission Result Found," + scanresult.ToString("X"));
                    alreadyseenaltresults.Add(scanresult);
                    MissionChecker checkerObject = new MissionChecker
                    {
                        scanresult = scanresult,
                        baseaddress = baseaddress,
                        Client = _Client
                    };
                    Thread checkerThread = new Thread(checkerObject.EvaluateMission);

                    // Start the worker thread.
                    checkerThread.Start();
                    DebugWriter.LogMissionDebug("Starting Mission Matcher Thread");
                }
                Thread.Sleep(500);
            }
        }
    }

    public class MissionChecker
    {
        public uint scanresult;
        public uint baseaddress;
        public required ArchipelagoClient Client;
        public void EvaluateMission()
        {
            if (MissionData.Missions.TryGetValue(scanresult, out var mission))
                {   //debug Log.Logger.Warning("Mission Value Matched");
                    ulong currentmissionresultsaddress = baseaddress + 0x6c;
                    //Log.Logger.Information("current mission address is " + currentmissionresultsaddress.ToString("X"));

                    Location newlocation = new Location()
                    {
                        Id = mission.ApId,
                        Name = mission.Name,
                        Address = currentmissionresultsaddress,
                        AddressBit = 2,
                        CheckType = LocationCheckType.Bit,
                        Category = "Missions"
                    };

                    App.Client.LocationManager.AddLocation(newlocation);
                    //debug Log.Logger.Warning("New Mission Location Added, " + Name);
                    DebugWriter.LogMissionDebug("New Mission Location Added, " + mission.Name.ToString());
                }
            
        }
    }
}
