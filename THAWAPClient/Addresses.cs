using THAWAPClient.Helpers;

namespace THAWAPClient
{
    public static class Addresses
    {

        public static ulong PCSX2Offset;

        public static ulong ShortOffset = 0x02;
        public static ulong IntOffset = 0x04;

        //Player Status Stuff
        public static ulong CurrentCash = 0xd24294;
        public static ulong CurrentLevel = 0xd240b0;
        public static ulong InGameTime = 0xd55644;

        //Gap Start Addresses
        public static ulong HWGapStart = 0xd240b8;

        //Shop Address Pointer
        public static ulong ShopStartAddress = 0x5aba4c; 

        //Bus Unlocks
        public static ulong BeverlyHills = 0x9ae398;
        public static ulong SkateRanch = 0x9ae718;
        public static ulong Downtown = 0x9ae498;
        public static ulong VansPark = 0x9aeb18;
        public static ulong SantaMonica = 0x9ae598;
        public static ulong OilRig = 0x9ae618;
        public static ulong EastLA = 0x9ae518;
        public static ulong Casino = 0x9ae698;

        // Stats (Floats)
        public static ulong AirStat = 0x715e04;
        public static ulong RunStat = 0x715e08;
        public static ulong OllieStat = 0x715e0c;
        public static ulong SpeedStat = 0x715e10;
        public static ulong SpinStat = 0x715e14;
        public static ulong FlipStat = 0x715e18;
        public static ulong SwitchStat = 0x715e1c;
        public static ulong RailStat = 0x715e20;
        public static ulong LipStat = 0x715e24;
        public static ulong ManualStat = 0x715e28;

        //Skating Abilities
        public static ulong AbilityManual = 0x71e4f0;
        public static ulong AbilityRevert = 0x71e4f8;
        public static ulong AbilitySpineTransfer = 0x71e500;
        public static ulong AbilityWallRide = 0x71e510;
        public static ulong AbilityStickerSlap = 0x71e518;
        public static ulong AbilityFlatland = 0x71e548;
        public static ulong AbilityNatasSpin = 0x71e550;
        public static ulong AbilityBoneless = 0x71e560;
        public static ulong AbilitySpecial = 0x71e588;
        public static ulong AbilityFocus = 0x71e590;
        public static ulong AbilityFlips = 0x71e598;
        public static ulong AbilityStall = 0x71e5a0;
        public static ulong AbilitySkitch = 0x71e5a8;
        public static ulong AbilityCaveman = 0x71e5b0;
        public static ulong AbilityWallRun = 0x71e5b8;
        public static ulong AbilityShimmy = 0x71e5c0;
        public static ulong AbilityWallFlip = 0x71e5c8;
        public static ulong AbilityBertSlide = 0x71e5e8;
        public static ulong AbilityTuck = 0x71e5f0;
        public static ulong AbilityBonedOllie = 0x71e5f8;

    }
}