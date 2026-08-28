using Archipelago.Core.Util;
using Archipelago.Core;
using Serilog;

namespace THAWAPClient.Models
{
    public class Traps
    {
        public async Task StartInvisibleTrap()
        {
            
        }
        public async Task TrapMoonGravity()
        {
            Memory.WriteBit(Addresses.CheatsActivated2, 1, true);
            Log.Logger.Information("You now have moon gravity for the next 60 seconds.");
            await Task.Delay(TimeSpan.FromSeconds(60));
            Memory.WriteBit(Addresses.CheatsActivated2, 1, false);
        }
    }
}