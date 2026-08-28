using Archipelago.Core.Util;
using Archipelago.Core.Models;
using Archipelago.Core;


namespace THAWAPClient.Models
{
    public class MiscLocationReading
    {
        public static List<ILocation> GetMiscLocations(TonyHawkOptions Options)
        {
            var locations = new List<ILocation>();
            if (Options.ChosenGoal >= (int)TonyHawkOptions.EndGoal.option_get_to_the_skate_ranch)
            {
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
            }
            if (Options.ChosenGoal >= (int)TonyHawkOptions.EndGoal.option_win_the_skate_competition)
            {
                locations.Add(new Location
                    {
                        Id = 40000001,
                        Name = "Visit Downtown",
                        Address = Addresses.Downtown,
                        CheckType = LocationCheckType.Bit,
                        AddressBit = 5,
                        Category = "Visiting Locations"
                    });

                locations.Add(new Location
                    {
                        Id = 50000001,
                        Name = "Visit Vans Park",
                        Address = Addresses.VansPark,
                        CheckType = LocationCheckType.Bit,
                        AddressBit = 5,
                        Category = "Visiting Locations"
                    });
            }
            return locations;
        }
    }
}