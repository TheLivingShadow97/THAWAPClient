using Archipelago.Core.Util;
using Archipelago.Core.Models;


namespace THAWAPClient.Models
{
    public class GapLocationReading
    {
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
            "FireEscape Level 3",
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
        
    }
}