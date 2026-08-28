using Archipelago.Core;
using Archipelago.Core.Util;
using THAWAPClient.Helpers;

namespace THAWAPClient.Models
{
    public class BusAccess
    {
        public static void UpdateBusAccess(ArchipelagoClient Client)
        {
            if (FindCount.ArchiCountingInt(Client, "Bus Access: Beverly Hills") > 0)
            {Memory.WriteBit(Addresses.BeverlyHills, 6, true);}
            if (FindCount.ArchiCountingInt(Client, "Bus Access: Downtown") > 0)
            {Memory.WriteBit(Addresses.Downtown, 6, true);}
        }
    }
}