using Archipelago.Core.Util;
using Archipelago.Core.Models;


namespace THAWAPClient.Models
{
    public class ShopLocationReading
    {   
        public static void AddShopLocations(List<ILocation> locations, Dictionary<string, uint> items, int baseId, string _Category)
        {
            int index = 0;

            foreach (var (itemName, offset) in items)
            {
                locations.Add(new Location
                {
                    Id = baseId + ++index,
                    Name = itemName,
                    Address = GetShopAddress(offset),
                    CheckType = LocationCheckType.UInt,
                    CheckValue = "1",
                    CompareType = LocationCheckCompareType.Match,
                    Category = _Category
                });
            }
        }

        public static List<ILocation> GetHollywoodShopLocations()
        {
            var locations = new List<ILocation>();

            AddShopLocations(locations, hwShirts,     10310000, "Hollywood Shop Locations");
            AddShopLocations(locations, hwPants,      10320000, "Hollywood Shop Locations");
            AddShopLocations(locations, hwDecks,      10330000, "Hollywood Shop Locations");
            AddShopLocations(locations, hwGripTapes,  10340000, "Hollywood Shop Locations");
            AddShopLocations(locations, hwElbowPads,  10350000, "Hollywood Shop Locations");
            AddShopLocations(locations, hwKneePads,   10360000, "Hollywood Shop Locations");
            AddShopLocations(locations, hwShoes,      10370000, "Hollywood Shop Locations");
            AddShopLocations(locations, hwSocks,      10380000, "Hollywood Shop Locations");
            AddShopLocations(locations, hwHair,       10390000, "Hollywood Shop Locations");
            
            return locations;
        }

        public static List<ILocation> GetBeverlyHillsShopLocations()
        {
            var locations = new List<ILocation>();

            AddShopLocations(locations, bhShirts,     20310000, "Beverly Hills Shop Locations");
            AddShopLocations(locations, bhPants,      20320000, "Beverly Hills Shop Locations");
            AddShopLocations(locations, bhDecks,      20330000, "Beverly Hills Shop Locations");
            AddShopLocations(locations, bhGripTapes,  20340000, "Beverly Hills Shop Locations");
            AddShopLocations(locations, bhShoes,      20350000, "Beverly Hills Shop Locations");
            AddShopLocations(locations, bhLeftAccessories,       20360000, "Beverly Hills Shop Locations");
            AddShopLocations(locations, bhRightAccessories,      20370000, "Beverly Hills Shop Locations");
            AddShopLocations(locations, bhGloves,      20380000, "Beverly Hills Shop Locations");
            AddShopLocations(locations, bhBackpacks,      20390000, "Beverly Hills Shop Locations");
            AddShopLocations(locations, bhHats,      20391000, "Beverly Hills Shop Locations");
            AddShopLocations(locations, bhGlasses,      20392000, "Beverly Hills Shop Locations");

            locations.Add(new Location
                {
                    Id = 20310030,
                    Name = "BH Shirt: Panda Suit",
                    Address = 0x9AF753,
                    CheckType = LocationCheckType.Bit,
                    AddressBit = 3,
                    Category = "Beverly Hills Shop Locations"
                });


            return locations;
        }

        public static ulong GetShopAddress(uint offset)
        {
            ulong addy = Memory.ReadUInt(Addresses.ShopStartAddress) + offset;
            addy = Memory.ReadUInt(addy) + 0x4;
            addy = Memory.ReadUInt(addy) + 0xc;

            return Convert.ToUInt64(Memory.ReadUInt(addy) + 0x8);
        }

        public static Dictionary<string, uint> hwShirts = new()
        {
            { "HW Shirt: Tanktop", 0xd0},
            { "HW Shirt: Sleeveless", 0xd4},
            { "HW Shirt: T-shirt", 0xe0},
            { "HW Shirt: Long Sleeve T", 0xec},
            { "HW Shirt: Layered T", 0xfc},
            { "HW Shirt: Military Coat", 0x100},
            { "HW Shirt: Sweater", 0x110},
            { "HW Shirt: Hoody", 0x114},
            { "HW Shirt: Zip Up Hoody", 0x118},
            { "HW Shirt: Leather Vest", 0x124},
            { "HW Shirt: Leather Jacket 1", 0x128},
            { "HW Shirt: Leather Jacket 2", 0x12c},
            { "HW Shirt: Sleeveless Denim", 0x134},
            { "HW Shirt: Vans Jersey", 0x14c},
            { "HW Shirt: Hawk Hoody", 0x160},
            { "HW Shirt: Hawk Thunder", 0x168},
            { "HW Shirt: Quiksilver Button-up", 0x174},
            { "HW Shirt: Quiksilver Hoody 1", 0x17c},
            { "HW Shirt: Electric Corpo", 0x1ac},
            { "HW Shirt: Electric Electroseal", 0x1b0},
            { "HW Shirt: Electric Plasma", 0x1b4},
            { "HW Shirt: Blind Bat Reaper Hoody", 0x1cc},
            { "HW Shirt: Nixon Shirt 2", 0x1e4},
            { "HW Shirt: Independent Stage 9 T", 0x200},
            { "HW Shirt: Von Zipper Hoody", 0x214},
            { "HW Shirt: Sessions Standard Issue Hoody", 0x224},
            { "HW Shirt: Volcom Hoody", 0x238},
            { "HW Shirt: Famous Stars and Straps Shirt 2", 0x240},
        };

        public static Dictionary<string, uint> hwPants = new()
        {
            { "HW Pants: Jeans", 0x264},
            { "HW Pants: Tight Jeans", 0x268},
            { "HW Pants: Jeans Ripped", 0x26c},
            { "HW Pants: Plaid Pants", 0x278},
            { "HW Pants: Cargo Pants", 0x27c},
            { "HW Pants: Leather Pants", 0x280},
            { "HW Pants: Plaid Shorts", 0x284},
            { "HW Pants: Cargo Shorts", 0x290},
            { "HW Pants: Bunch Shorts", 0x294},
            { "HW Pants: Camo Shorts", 0x298},
            { "HW Pants: Quiksilver Jeans", 0x29c},
            { "HW Pants: Baker Jeans", 0x2a0},
            { "HW Pants: Hurley Icon Jeans", 0x2a4},
            { "HW Pants: DVS Jeans", 0x2a8},
            { "HW Pants: DC Aidan DX Jeans", 0x2b0},
        };
        public static Dictionary<string, uint> hwDecks = new()
        {
            { "HW Deck: Almost Mullen 10 Stair", 0x458},
            { "HW Deck: Almost Daewon Peace", 0x46c},
            { "HW Deck: Alva Blue Tile", 0x470},
            { "HW Deck: Alva N", 0x484},
            { "HW Deck: Antihero Green", 0x498},
            { "HW Deck: Baker Brand Logo", 0x4a0},
            { "HW Deck: Birdhouse Compass", 0x4ac},
            { "HW Deck: Birdhouse Falcon Yellow", 0x4c0},
            { "HW Deck: Birdhouse Icon", 0x4d4},
            { "HW Deck: Blind Original", 0x4d8},
            { "HW Deck: 5Boro-Cinco Barrios", 0x4e4},
            { "HW Deck: DGK Ultra", 0x4ec},
            { "HW Deck: Element Mike v Big Series", 0x500},
            { "HW Deck: Element Section", 0x514},
            { "HW Deck: Hook Ups", 0x51c},
            { "HW Deck: World Ind Vallely Animal Man", 0x534},
            { "HW Deck: World Ind Vallely Snake", 0x548},
            { "HW Deck: Plan B Team 3-D", 0x54c},
            { "HW Deck: Powell Peralta Cab", 0x560},
            { "HW Deck: Powell Peralta Mullen", 0x574},
            { "HW Deck: RDS 1", 0x578},
            { "HW Deck: The Firm Bobs Stencil", 0x5a8},
            { "HW Deck: World Industries 1", 0x588},
        };

        public static Dictionary<string, uint> hwGripTapes = new()
        {
            { "HW Grip Tape: Generic Cut", 0x5bc},
            { "HW Grip Tape: Solid", 0x5c0},
            { "HW Grip Tape: Razor's Edge", 0x5c4},
            { "HW Grip Tape: Equal", 0x5c8},
            { "HW Grip Tape: Slasher", 0x5cc},
            { "HW Grip Tape: Ye Old School", 0x5d0},
            { "HW Grip Tape: Hawk", 0x600},
        };

        public static Dictionary<string, uint> hwElbowPads = new()
        {
            { "HW Elbow Pads: Elbow Pads", 0x618},
            { "HW Elbow Pads: Left Elbow Pad", 0x61c},
            { "HW Elbow Pads: Right Elbow Pad", 0x620},
        };

        public static Dictionary<string, uint> hwKneePads = new()
        {
            { "HW Knee Pads", 0x630},
        };

        public static Dictionary<string, uint> hwShoes = new()
        {
            { "HW Shoes: Skate Shoe 1", 0x648},
            { "HW Shoes: Combat Boots", 0x64c},
            { "HW Shoes: Hi Tops", 0x650},
            { "HW Shoes: Hurley the Crown", 0x654},
            { "HW Shoes: Hurley Amp", 0x658},
            { "HW Shoes: Hurley Burnquist", 0x65c},
            { "HW Shoes: DGK Workout Low - White on White", 0x660},
            { "HW Shoes: DGK Workout Low - Navy", 0x664},
            { "HW Shoes: DGK Williams", 0x668},
            { "HW Shoes: Globe Icon", 0x66c},
            { "HW Shoes: Globe Finale", 0x670},
            { "HW Shoes: Globe Mullen Tensor", 0x674},
            { "HW Shoes: DVS Milan", 0x678},
            { "HW Shoes: DVS Kenyan", 0x67c},
            { "HW Shoes: DVS Revival Splat", 0x680},
            { "HW Shoes: DVS Daewon", 0x684},
            { "HW Shoes: Vans Tony III TT/Fog", 0x688},
            { "HW Shoes: Vans Tony III Blk/Wht", 0x68c},
            { "HW Shoes: Vans Tony III Grn/Blk", 0x690},
            { "HW Shoes: Vans Tony III Garg/Nvy", 0x694},
            { "HW Shoes: Vans Tony III Gry/Wht", 0x698},
            { "HW Shoes: Checkered", 0x69c},
            { "HW Shoes: ES Anti-Social", 0x6a4},
            { "HW Shoes: ES K7", 0x6a8},
            { "HW Shoes: ES Accelerate", 0x6ac},
            { "HW Shoes: Emerica Crass", 0x6b0},
            { "HW Shoes: Emerica Felt", 0x6b4},
            { "HW Shoes: Emerica Reynolds 3", 0x6b8},
            { "HW Shoes: Etnies Bastien", 0x6c0},
            { "HW Shoes: Etnies Arto", 0x6c4},
            { "HW Shoes: Etnies Lo-cal", 0x6c8},
            { "HW Shoes: Quiksilver Hawk 1", 0x6d0},
            { "HW Shoes: Quiksilver Hawk 2", 0x6d4},
            { "HW Shoes: Adio Selego", 0x6d8},
            { "HW Shoes: Adio Jeremy Wray", 0x6dc},
            { "HW Shoes: Adio Brian Sumner", 0x6e0},
            { "HW Shoes: Adio Tony Hawk", 0x6e4},
            { "HW Shoes: Adio Viva Bam", 0x6e8},
            { "HW Shoes: Nike Airzoom 1", 0x6ec},
            { "HW Shoes: Nike Airzoom 2", 0x6f0},
            { "HW Shoes: DC Manteca", 0x6f8},
            { "HW Shoes: Element Fuji", 0x700},
        };

        public static Dictionary<string, uint> hwSocks = new()
        {
            { "HW Socks: Knee High", 0x638},
            { "HW Socks: Medium", 0x63c},
            { "HW Socks: Low", 0x640},
        };

        public static Dictionary<string, uint> hwHair = new()
        {
            { "HW Hair: Bald", 0x64},
            { "HW Hair: Buzzed", 0x68},
            { "HW Hair: Devil Lock", 0x6c},
            { "HW Hair: Dead Guy Doo", 0x70},
            { "HW Hair: Mullet", 0x74},
            { "HW Hair: McSqueeb R", 0x78},
            { "HW Hair: McSqueeb L", 0x7c},
            { "HW Hair: Mohawk A", 0x80},
            { "HW Hair: Mohawk B", 0x84},
            { "HW Hair: Liberty Spikes 1", 0x88},
            { "HW Hair: Liberty Spikes 2", 0x8c},
            { "HW Hair: Spiked 1", 0x90},
            { "HW Hair: Spiked 2", 0x94},
            { "HW Hair: Fauxhawk", 0x98},
            { "HW Hair: Long", 0x9c},
            { "HW Hair: Ponytail", 0xa0},
            { "HW Hair: Afro", 0xa4},
            { "HW Hair: Dreadlocks", 0xa8},
            { "HW Hair: Pompadour", 0xac},
            { "HW Hair: Flat Top", 0xb0},
            { "HW Hair: Pigtails", 0xb4},
            { "HW Hair: Caesar", 0xb8},
            { "HW Hair: Cornrows", 0xbc},
            { "HW Hair: Medium", 0xc0},
            { "HW Hair: Short", 0xc4}
        };
    
        public static Dictionary<string, uint> bhShirts = new()
        {
            { "BH Shirt: Ringer T", 0xdc},
            { "BH Shirt: Polo Shirt", 0xe8},
            { "BH Shirt: Baseball 1", 0xf4},
            { "BH Shirt: Sports Coat", 0x104},
            { "BH Shirt: Dress Beater", 0x108},
            { "BH Shirt: Dress Shirt T", 0x10c},
            { "BH Shirt: Denim Jacket", 0x130},
            { "BH Shirt: Track Jacket", 0x138},
            { "BH Shirt: Basketball Jersey", 0x250},
            { "BH Shirt: Hawaiian", 0x140},
            { "BH Shirt: Vans Birdseye", 0x150},
            { "BH Shirt: Hawk Brigade", 0x164},
            { "BH Shirt: Hawk Rocker", 0x16c},
            { "BH Shirt: Quiksilver Striped", 0x170},
            { "BH Shirt: Quiksilver Hoody 2", 0x180},
            { "BH Shirt: Hurley Biker Jacket", 0x188},
            { "BH Shirt: Hurley Polo", 0x18c},
            { "BH Shirt: DC Stence Tank", 0x254},
            { "BH Shirt: Element Baroque", 0x19c},
            { "BH Shirt: Element Addict", 0x1a4},
            { "BH Shirt: Element Ardent", 0x1a8},
            { "BH Shirt: Electric Hoody", 0x1b8},
            { "BH Shirt: Nixon Shirt 1", 0x1e0},
            { "BH Shirt: Birdhouse Shirt 1", 0x1e8},
            { "BH Shirt: Alva OG Shirt", 0x1f8},
            { "BH Shirt: Von Zipper Shirt 1", 0x208},
            { "BH Shirt: Volcom Houston", 0x230},
            { "BH Shirt: Free Agent Shirt 1", 0x248},
            { "BH Shirt: Globe Quaranteen T", 0x260},
        };

        public static Dictionary<string, uint> bhPants = new()
        {
            { "BH Pants: Khaki Pants", 0x274 },
            { "BH Pants: Board Shorts", 0x288 },
            { "BH Pants: Short Shorts", 0x28c },
        };

        public static Dictionary<string, uint> bhDecks = new()
        {
            { "BH Deck: Almost Sheckler Hate", 0x45c },
            { "BH Deck: Alva Cross", 0x474 },
            { "BH Deck: Alva Ocean Size", 0x488 },
            { "BH Deck: Birdhouse Dragon Skull", 0x4b0 },
            { "BH Deck: Birdhouse Giant Black", 0x4c4 },
            { "BH Deck: Blind Reaper", 0x4dc },
            { "BH Deck: 5Boro Script Logo", 0x4e8 },
            { "BH Deck: DGK Logo", 0x4f0 },
            { "BH Deck: Element Bam Margera Big Series", 0x504 },
            { "BH Deck: Element Elemental Seal", 0x518 },
            { "BH Deck: Blind Chainsaw Kitten", 0x538 },
            { "BH Deck: Plan B Team Bow Wow", 0x550 },
            { "BH Deck: Powell Peralta Hawk", 0x564 },
            { "BH Deck: RDS 2", 0x57c },
            { "BH Deck: World Industries 2", 0x58c },
        };

        public static Dictionary<string, uint> bhGripTapes = new()
        {
            { "BH Grip Tape: Striper", 0x5d4 },
            { "BH Grip Tape: Thrashed", 0x5d8 },
            { "BH Grip Tape: Colored Nuts", 0x5dc },
            { "BH Grip Tape: Green Machine", 0x5e0 },
            { "BH Grip Tape: Blues", 0x5e4 },
            { "BH Grip Tape: Red Light", 0x5e8 },
            { "BH Grip Tape: Crack", 0x5ec },
            { "BH Grip Tape: Eye Don't Know", 0x5f0 },
            { "BH Grip Tape: NS Single Eye", 0x5f4 },
            { "BH Grip Tape: The Bat", 0x5f8 },
            { "BH Grip Tape: Right Thrashed", 0x5fc },
            { "BH Grip Tape: Mullen", 0x608 },
            { "BH Grip Tape: Sheckler", 0x610 },
        };

        public static Dictionary<string, uint> bhShoes = new()
        {
            { "BH Shoes: Vans Tony III", 0x6a0 },
            { "BH Shoes: Emerica Reynolds 2", 0x6bc },
        };

        public static Dictionary<string, uint> bhLeftAccessories = new()
        {
            { "BH Left Accessory: Wrist Band A", 0x3e4 },
            { "BH Left Accessory: Watch A", 0x3e8 },
            { "BH Left Accessory: Baker Die-Cut", 0x3ec },
            { "BH Left Accessory: DGK Baller Band", 0x3f0 },
            { "BH Left Accessory: Nixon Player", 0x3f4 },
            { "BH Left Accessory: Nixon Dork", 0x3f8 },
            { "BH Left Accessory: Nixon Special Ops", 0x3fc },
            { "BH Left Accessory: Nixon Rocker", 0x400 },
            { "BH Left Accessory: Nixon Band", 0x404 },
            { "BH Left Accessory: Quiksilver Watch", 0x408 },
            { "BH Left Accessory: Wristguard", 0x40c },
            { "BH Left Accessory: Spikes", 0x410 },
        };

        public static Dictionary<string, uint> bhRightAccessories = new()
        {
            { "BH Right Accessory: Wrist Band A", 0x41c },
            { "BH Right Accessory: Watch A", 0x420 },
            { "BH Right Accessory: Baker Die-Cut", 0x424 },
            { "BH Right Accessory: DGK Baller Band", 0x428 },
            { "BH Right Accessory: Nixon Player", 0x42c },
            { "BH Right Accessory: Nixon Dork", 0x430 },
            { "BH Right Accessory: Nixon Special Ops", 0x434 },
            { "BH Right Accessory: Nixon Rocker", 0x438 },
            { "BH Right Accessory: Nixon Band", 0x43c },
            { "BH Right Accessory: Quiksilver Watch", 0x440 },
            { "BH Right Accessory: Wristguard", 0x444 },
            { "BH Right Accessory: Spikes", 0x448 },
        };

        public static Dictionary<string, uint> bhGloves = new()
        {
            { "BH Gloves: Glove 1", 0x2c4 },
            { "BH Gloves: Glove 2", 0x2c8 },
        };

        public static Dictionary<string, uint> bhBackpacks = new()
        {
            { "BH Backpack: Backpack A", 0x2b4 },
            { "BH Backpack: Backpack B", 0x2b8 },
            { "BH Backpack: Messenger Bag", 0x2bc },
            { "BH Backpack: Backpack Paint", 0x2c0 },
        };

        public static Dictionary<string, uint> bhHats = new()
        {
            { "BH Hats: Flatbilled Cap", 0x2f4 },
            { "BH Hats: Baseball Cap", 0x2f8 },
            { "BH Hats: Cap Backwards", 0x2fc },
            { "BH Hats: Upturned Bill", 0x300 },
            { "BH Hats: Trucker Hat", 0x304 },
            { "BH Hats: Trucker Backwards", 0x308 },
            { "BH Hats: Brimmed Beanie", 0x310 },
            { "BH Hats: Headband", 0x318 },
            { "BH Hats: Visor", 0x328 },
            { "BH Hats: Baker Hanoi A", 0x358 },
            { "BH Hats: Baker Hanoi B", 0x35c },
            { "BH Hats: Hurley Rude Boy", 0x364 },
            { "BH Hats: DGK Headband", 0x368 },
            { "BH Hats: DGK Cap 1", 0x36c },
            { "BH Hats: DGK Cap 2", 0x370 },
            { "BH Hats: DVS Trucker", 0x374 },
            { "BH Hats: Electric Cap", 0x378 },
            { "BH Hats: Pro-Tec Hassan", 0x380 },
            { "BH Hats: Pro-Tec Lasek", 0x384 },
            { "BH Hats: Pro-Tec Caballero", 0x388 },
            { "BH Hats: Nixon Cap", 0x398 },
            { "BH Hats: Quiksilver Cap 1", 0x39c },
            { "BH Hats: Quiksilver Cap 2", 0x3a0 },
            { "BH Hats: Birdhouse Cap 1", 0x3a4 },
            { "BH Hats: Birdhouse Cap 2", 0x3a8 },
            { "BH Hats: Alva Scratch Cap", 0x3ac },
            { "BH Hats: Zumiez Cap", 0x3b0 },
            { "BH Hats: Indy Red/White Cross", 0x3b4 },
            { "BH Hats: Plan B Cap 1", 0x3b8 },
            { "BH Hats: Plan B Cap 2", 0x3bc },
            { "BH Hats: Sessions Kimballotta Cap", 0x3c0 },
            { "BH Hats: RDS Cap", 0x3c4 },
            { "BH Hats: Nike Cap", 0x3c8 },
            { "BH Hats: Volcom Premium Cheese", 0x3cc },
            { "BH Hats: Volcom Cap", 0x3d0 },
            { "BH Hats: Famous Stars and Straps Headband", 0x3d4 },
            { "BH Hats: Free Agent Beanie", 0x3d8 },
            { "BH Hats: Free Agent Trucker", 0x3dc },
            { "BH Hats: Globe Colab", 0x3e0 },
        };

        public static Dictionary<string, uint> bhGlasses = new()
        {
            { "BH Glasses: Aviator", 0x2cc },
            { "BH Glasses: Electric BSG", 0x2d0 },
            { "BH Glasses: Arnette 1", 0x2d4 },
            { "BH Glasses: Arnette 2", 0x2d8 },
            { "BH Glasses: Arnette 3", 0x2dc },
            { "BH Glasses: Arnette 4", 0x2e0 },
            { "BH Glasses: Von Zipper Papa G", 0x2e4 },
            { "BH Glasses: Von Zipper Rockford", 0x2e8 },
            { "BH Glasses: Von Zipper Brooklyn", 0x2ec },
            { "BH Glasses: Von Zipper Skitch", 0x2f0 },
        };
    }
}