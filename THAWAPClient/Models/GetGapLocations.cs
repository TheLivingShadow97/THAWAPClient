using Archipelago.Core.Util;
using Archipelago.Core.Models;
using THAWAPClient.Helpers;
using Archipelago.Core;
using Serilog;


namespace THAWAPClient.Models
{
    public class GapLocationReading
    {   
        public static bool HollywoodNeeded = false;
        public static bool BeverlyHillsNeeded = false;
        public static bool SkateRanchNeeded = false;
        public static bool DowntownNeeded = false;

        private static CancellationTokenSource? _cts4gr;

        public static void StartGapLocationInitializationLoop(ArchipelagoClient Client, TonyHawkOptions Options)
        {
            if (_cts4gr != null)
                return; // already running

            _cts4gr = new CancellationTokenSource();
            HollywoodNeeded = true;
            if (Options.ChosenGoal >= (int)TonyHawkOptions.EndGoal.option_get_to_the_skate_ranch)
                {BeverlyHillsNeeded = true;};
            if (Options.ChosenGoal >= (int)TonyHawkOptions.EndGoal.option_win_the_skate_competition)
                {DowntownNeeded = true;
                SkateRanchNeeded = true;};
            _ = StartGapLocationInitializationLoopAsync(_cts4gr.Token, Client);
        }

        public static void StopGapLocationInitializationLoop()
        {
            _cts4gr?.Cancel();
            _cts4gr = null;
        }

        public static async Task StartGapLocationInitializationLoopAsync(CancellationToken token, ArchipelagoClient Client)
        {
            try
            {
                while (!token.IsCancellationRequested)
                {
                    await EvaluateInitializationAsync(Client);

                    await Task.Delay(TimeSpan.FromSeconds(2), token);
                }
            }
            catch (TaskCanceledException)
            {
                // Expected when stopping — no action needed
            }
        }

        public static async Task EvaluateInitializationAsync(ArchipelagoClient Client)
        {   if (!LevelID.IsInGame())
                {return;}
            int currentlevel = LevelID.GetCurrentLevel();
            if (currentlevel == (int)LevelID.CurrentLevel.Hollywood && HollywoodNeeded == true)
            {
                HollywoodNeeded = false;
                await Task.Delay(TimeSpan.FromSeconds(8));
                var HollywoodGaps = GetHollywoodGapData();
                foreach (var loc in HollywoodGaps)
                    {
                        Client.LocationManager.AddLocation(loc);
                    }
                Log.Logger.Information("Hollywood Gap Locations Added");
                DebugWriter.LogLocationDebug("Hollywood Gap Locations Added");
            }
            if (currentlevel == (int)LevelID.CurrentLevel.BeverlyHills && BeverlyHillsNeeded == true)
            {
                BeverlyHillsNeeded = false;
                await Task.Delay(TimeSpan.FromSeconds(8));
                var BeverlyHillsGaps = GetBeverlyHillsGapData();
                foreach (var loc in BeverlyHillsGaps)
                    {
                        Client.LocationManager.AddLocation(loc);
                    }
                Log.Logger.Information("Beverly Hills Gap Locations Added");
                DebugWriter.LogLocationDebug("Beverly Hills Gap Locations Added");
            }
            if (currentlevel == (int)LevelID.CurrentLevel.SkateRanch && SkateRanchNeeded == true)
            {
                SkateRanchNeeded = false;
                await Task.Delay(TimeSpan.FromSeconds(8));
                var SkateRanchGaps = GetSkateRanchGapData();
                foreach (var loc in SkateRanchGaps)
                    {
                        Client.LocationManager.AddLocation(loc);
                    }
                Log.Logger.Information("Skate Ranch Gap Locations Added");
                DebugWriter.LogLocationDebug("Skate Ranch Gap Locations Added");
            }
            if (currentlevel == (int)LevelID.CurrentLevel.Downtown && DowntownNeeded == true)
            {
                DowntownNeeded = false;
                await Task.Delay(TimeSpan.FromSeconds(8));
                var DowntownGaps = GetDowntownGapData();
                foreach (var loc in DowntownGaps)
                    {
                        Client.LocationManager.AddLocation(loc);
                    }
                Log.Logger.Information("Downtown Gap Locations Added");
                DebugWriter.LogLocationDebug("Downtown Gap Locations Added");
            }
            if ((HollywoodNeeded == false) && (BeverlyHillsNeeded == false) && (SkateRanchNeeded == false) && (DowntownNeeded == false))
                {StopGapLocationInitializationLoop();}
        }

        public static List<ILocation> GetDowntownGapData()
        {
            var locations = new List<ILocation>();

            int baseId = 40200000;

            for (int i = 0; i < DowntownGapNames.Count; i++)
            {
                int index = i + 1;

                locations.Add(new Location
                {
                    Id = baseId + index,
                    Name = $"DT Gap: {DowntownGapNames[i]}",
                    Address = GetGapAddress(Addresses.DTGapStart, index),
                    CheckType = LocationCheckType.UInt,
                    CheckValue = "0",
                    CompareType = LocationCheckCompareType.GreaterThan,
                    Category = "Downtown Gaps"
                });
            }

            return locations;
        }

        public static List<ILocation> GetSkateRanchGapData()
        {
            var locations = new List<ILocation>();

            int baseId = 30200000;

            for (int i = 0; i < SkateRanchGapNames.Count; i++)
            {
                int index = i + 1;

                locations.Add(new Location
                {
                    Id = baseId + index,
                    Name = $"SR Gap: {SkateRanchGapNames[i]}",
                    Address = GetGapAddress(Addresses.SRGapStart, index),
                    CheckType = LocationCheckType.UInt,
                    CheckValue = "0",
                    CompareType = LocationCheckCompareType.GreaterThan,
                    Category = "Skate Ranch Gaps"
                });
            }

            return locations;
        }

        public static List<ILocation> GetBeverlyHillsGapData()
        {
            var locations = new List<ILocation>();

            int baseId = 20200000;

            for (int i = 0; i < BeverlyHillsGapNames.Count; i++)
            {
                int index = i + 1;

                locations.Add(new Location
                {
                    Id = baseId + index,
                    Name = $"BH Gap: {BeverlyHillsGapNames[i]}",
                    Address = GetGapAddress(Addresses.BHGapStart, index),
                    CheckType = LocationCheckType.UInt,
                    CheckValue = "0",
                    CompareType = LocationCheckCompareType.GreaterThan,
                    Category = "Beverly Hills Gaps"
                });
            }

            return locations;
        }

        public static List<ILocation> GetHollywoodGapData()
        {
            var locations = new List<ILocation>();

            int baseId = 10200000;

            for (int i = 0; i < HollywoodGapNames.Count; i++)
            {
                int index = i + 1;

                locations.Add(new Location
                {
                    Id = baseId + index,
                    Name = $"HW Gap: {HollywoodGapNames[i]}",
                    Address = GetGapAddress(Addresses.HWGapStart, index),
                    CheckType = LocationCheckType.UInt,
                    CheckValue = "0",
                    CompareType = LocationCheckCompareType.GreaterThan,
                    Category = "Hollywood Gaps"
                });
            }

            return locations;
        }

        public static ulong GetGapAddress(ulong gapstartaddress, int gapindex)
        {
            ulong addy = Memory.ReadUInt(gapstartaddress) + 0x10;

            for (int i = 1; i < gapindex; i++)
            {
                addy = Memory.ReadUInt(addy) + 0xC;
            }

            return Convert.ToUInt64(Memory.ReadUInt(addy) + 0x44);
        }

        private static readonly List<string> HollywoodGapNames =
        [
            "Bench2Bench",
            "FireEscape Level1",
            "Hollywood High Steps",
            "Planter2Planter",
            "Rail2Bleacher",
            "Rail2Rail",
            "Bleacher Hop",
            "Car Hop",
            "Chinese Transfer",
            "El Teniente Spine",
            "FireEscape Level2",
            "Hollywood Sign Blast",
            "Pin Plant",
            "Planter Pop",
            "Romperwood Spine",
            "Romperwood Transfer",
            "Schools Out",
            "Tony to Tony",
            "Velvet Rope",
            "Voodoo Spine",
            "Half Moon Grind",
            "Straight Outta Bronson",
            "Yellow",
            "FireEscape Level3",
            "Record Deal",
            "Spinner",
            "Dump Up",
            "El Teniente Drop",
            "Romper Rail",
            "Bronson Backlog",
            "Trapdoor",
            "FireEscape Level4",
            "Goat Whackin'",
            "Manual the Stars",
            "Over vine",
            "FireEscape Level5",
            "Hollywood High Line"
        ];

        private static readonly List<string> BeverlyHillsGapNames =
        [
            "Rail hop",
            "Ledge 2 Rail",
            "Rail 2 Ledge",
            "Car hop",
            "Ted's Lip",
            "Useless crap",
            "Between the trees",
            "Going the distance",
            "Rail 2 QP",
            "Ramp 2 wire",
            "Short turd drop",
            "Upper ledge",
            "Across the street",
            "Alley transfer",
            "Andys happy place",
            "City hall transfer",
            "Easy gap",
            "Got Gas?",
            "Modern art?",
            "Roof 2 roof",
            "No Tea baggin",
            "Out by 7!!",
            "Ledge 2 awning",
            "Long turd drop",
            "Qp 2 wire hop",
            "Frontside entrance transfer",
            "Got wings?",
            "Nice manual",
            "Ralphs transfer",
            "Gas 2 rail",
            "Sweet stairset",
            "Get your pizza",
            "Wall 2 wire",
            "Learn to swim"
        ];

        private static readonly List<string> SkateRanchGapNames =
        [
            "Car hop",
            "Car Parts Hop",
            "Casino Elevator",
            "Back of Couch",
            "Control Box Spinner",
            "Downtown Street",
            "Bail Bonds Wallride",
            "Courthouse Big Spine",
            "Strange Decoration Boost Air",
            "Bail Bonds Limousine",
            "Record Co. Top Jump",
            "Ventura Fwy Drop",
            "Spotlight Air",
            "Bag Shop Arch Manual",
            "Green Dome Air",
            "Skatepark Office Kicker",
            "Museum Gates Raildrop",
            "El Teniente Grind",
            "Fame Grind Gap",
            "Fire Escape Grind",
            "City Hall Star Jump",
            "Dinosaur Head Gap",
            "Pyramid Natas",
            "Destroyed Hotel Jump",
            "Chinaman Tower",
            "777 Gold Ring",
            "Escalators Pop",
            "FLOOR PLATE GAP",
            "Mexico Bell",
            "Movie Ropes Hip Transfer",
            "Oil Tanks Pipe",
            "Underground Tunnel Pipe",
            "Oil Ring Vator",
            "Pier Scope Jump",
            "Oil Rig Chunk Air",
            "Kicker to Pier Sign",
            "Ferris A-Frame Drop",
            "Shark Head Tele",
            "WASTELAND Grind",
            "Roulet Hop 2 Natas",
            "Doggtown Stairset",
            "Spike Pit Statue Natas",
            "Green Pipe Point Manual"
        ];

        private static readonly List<string> DowntownGapNames =
        [
            "Hop On!",
            "Car hop",
            "China Awning",
            "Chinatown Sign",
            "Electric Wire!",
            "Pillar",
            "Dumpster 2 Loading!",
            "Loading 2 Dumpster!",
            "Dumpster 2 Fence!",
            "Fence 2 Dumpster!",
            "Moca 2 Pool",
            "Angel Goin Down!",
            "Fence 2 Fence!",
            "Loading 2 Fence!",
            "Bowl 2 Edge",
            "Fence 2 Loading!",
            "Loading Edge!",
            "Over the Fountain!",
            "QP 2 Edge",
            "Pipe a Chuy",
            "Pool 2 Moca",
            "Angel Goin Up!",
            "Over The Hut",
            "Chinese QP Transfer",
            "Fountain Manual",
            "Freeway Flyer",
            "Low 2 Medium",
            "Medium 2 High",
            "Around The Metro",
            "Big Blow Transfer",
            "Chinese Air Transfer!",
            "La Sala Air Transfer!",
            "Low 2 High",
            "Manual The Dumpster!",
            "Awning 2 Wire!",
            "Freeway Bank!",
            "Underground Bank Transfer",
            "Wire 2 Awning!",
            "Tunnel Transfer",
            "Pyramid Drop!",
            "Big Lip!",
            "Overpass Air!"
        ];
        
    }
}