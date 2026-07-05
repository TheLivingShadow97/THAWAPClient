using Archipelago.Core.Util;
using Archipelago.Core.Models;


namespace THAWAPClient.Models
{
    public class MiscLocationReading
    {
        public static List<ILocation> AddMiscBHLocations()
        {
            var locations = new List<ILocation>();

            locations.Add(new Location
                {
                    Id = 20000001,
                    Name = "Visit Beverly Hills",
                    Address = Addresses.BeverlyHills,
                    CheckType = LocationCheckType.Bit,
                    AddressBit = 5,
                    Category = "Visiting Locations"
                });

            locations.Add(new Location
                {
                    Id = 20000002,
                    Name = "Visit the Skate Ranch",
                    Address = Addresses.SkateRanch,
                    CheckType = LocationCheckType.Bit,
                    AddressBit = 5,
                    Category = "Visiting Locations"
                });

            return locations;
        }
    }
}