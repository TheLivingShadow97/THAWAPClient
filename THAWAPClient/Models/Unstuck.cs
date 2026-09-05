using Archipelago.Core;
using Archipelago.Core.Util;
using THAWAPClient.Helpers;

namespace THAWAPClient.Models
{
    public class Unstuck
    {
        public static void TeleportMe()
        {
            int tempcurrentlevel = Memory.ReadInt(Addresses.CurrentLevel);
            if (tempcurrentlevel == (int)LevelID.CurrentLevel.Hollywood)
            {
                Memory.Write(Addresses.Xcoord, -2200f);
                Memory.Write(Addresses.Ycoord, 100f);
                Memory.Write(Addresses.Zcoord, -2200f);
            }
            else if (tempcurrentlevel == (int)LevelID.CurrentLevel.BeverlyHills)
            {
                Memory.Write(Addresses.Xcoord, -13000);
                Memory.Write(Addresses.Ycoord, -125);
                Memory.Write(Addresses.Zcoord, 7200);
            }
            else if (tempcurrentlevel == (int)LevelID.CurrentLevel.SkateRanch)
            {
                Memory.Write(Addresses.Xcoord, -25000f);
                Memory.Write(Addresses.Ycoord, 600f);
                Memory.Write(Addresses.Zcoord, 9300f);
            }
            else if (tempcurrentlevel == (int)LevelID.CurrentLevel.Downtown)
            {
                Memory.Write(Addresses.Xcoord, 10000f);
                Memory.Write(Addresses.Ycoord, -50f);
                Memory.Write(Addresses.Zcoord, 6200f);
            }
            else if (tempcurrentlevel == (int)LevelID.CurrentLevel.VansPark)
            {
                Memory.Write(Addresses.Xcoord, -7600f);
                Memory.Write(Addresses.Ycoord, 100f);
                Memory.Write(Addresses.Zcoord, 1700f);
            }
            else
            {}
        }
        
    }
}