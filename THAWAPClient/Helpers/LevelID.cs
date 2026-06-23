using Archipelago.Core.Util;

namespace THAWAPClient.Helpers
{
    public class LevelID
    {
        public static bool IsInGame()
        {
            if (GetCurrentLevel() != 0)
                return true;
            return false;
        }
        public static uint GetCurrentLevel()
        {
            uint tempcurrentlevel = Memory.ReadUInt(Addresses.CurrentLevel);
            return tempcurrentlevel;
        }

        public enum CurrentLevel
        {
            MainMenu = 0,
            Hollywood = 1,
            BeverlyHills = 2,
            Downtown = 3,
            EastLA = 4,
            SantaMonica = 5,
            OilRig = 6,
            Casino = 7,
            SkateRanch = 8,
            Atlanta = 9,
            Chicago = 0xa,
            Minneapolis = 0xb,
            Kyoto = 0xc,
            TheMall = 0xd,
            Marseilles = 0xe,
            SantaCruz = 0xf,
            VansPark = 0x10,
            TheRuins = 0x11,
            StoryModeIntro = 0x17,
        }
    }

}

