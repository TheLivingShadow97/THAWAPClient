using Archipelago.Core.Util;
using Archipelago.Core.Models;


namespace THAWAPClient.Models
{
    public class ShopLocationReading
    {
        public static List<ILocation> GetHollywoodShopLocations()
        {
            var locations = new List<ILocation>();
            int shirtindex = 0;
            int pantsindex = 0;
            int decksindex = 0;
            int griptapeindex = 0;
            int elbowpadsindex = 0;
            int kneepadsindex = 0;
            int shoesindex = 0;
            int socksindex = 0;
            int hairindex = 0;

            foreach (var shirt in hwShirts)
            {
                string itemName = shirt.Key;
                uint offset = shirt.Value;
                shirtindex++;
                int baseId = 10310000;

                locations.Add(new Location
                {
                    Id = baseId + shirtindex,
                    Name = itemName,
                    Address = GetShopAddress(offset),
                    CheckType = LocationCheckType.UInt,
                    CheckValue = "1",
                    CompareType = LocationCheckCompareType.Match,
                    Category = "Hollywood Shop Items"
                });
            }

            foreach (var pants in hwPants)
            {
                string itemName = pants.Key;
                uint offset = pants.Value;
                pantsindex++;
                int baseId = 10320000;

                locations.Add(new Location
                {
                    Id = baseId + pantsindex,
                    Name = itemName,
                    Address = GetShopAddress(offset),
                    CheckType = LocationCheckType.UInt,
                    CheckValue = "1",
                    CompareType = LocationCheckCompareType.Match,
                    Category = "Hollywood Shop Items"
                });
            }

            foreach (var deck in hwDecks)
            {
                string itemName = deck.Key;
                uint offset = deck.Value;
                decksindex++;
                int baseId = 10330000;

                locations.Add(new Location
                {
                    Id = baseId + decksindex,
                    Name = itemName,
                    Address = GetShopAddress(offset),
                    CheckType = LocationCheckType.UInt,
                    CheckValue = "1",
                    CompareType = LocationCheckCompareType.Match,
                    Category = "Hollywood Shop Items"
                });
            }

            foreach (var griptape in hwGripTapes)
            {
                string itemName = griptape.Key;
                uint offset = griptape.Value;
                griptapeindex++;
                int baseId = 10340000;

                locations.Add(new Location
                {
                    Id = baseId + griptapeindex,
                    Name = itemName,
                    Address = GetShopAddress(offset),
                    CheckType = LocationCheckType.UInt,
                    CheckValue = "1",
                    CompareType = LocationCheckCompareType.Match,
                    Category = "Hollywood Shop Items"
                });
            }

            foreach (var elbowpads in hwElbowPads)
            {
                string itemName = elbowpads.Key;
                uint offset = elbowpads.Value;
                elbowpadsindex++;
                int baseId = 10350000;

                locations.Add(new Location
                {
                    Id = baseId + elbowpadsindex,
                    Name = itemName,
                    Address = GetShopAddress(offset),
                    CheckType = LocationCheckType.UInt,
                    CheckValue = "1",
                    CompareType = LocationCheckCompareType.Match,
                    Category = "Hollywood Shop Items"
                });
            }

            foreach (var kneepads in hwKneePads)
            {
                string itemName = kneepads.Key;
                uint offset = kneepads.Value;
                kneepadsindex++;
                int baseId = 10360000;

                locations.Add(new Location
                {
                    Id = baseId + kneepadsindex,
                    Name = itemName,
                    Address = GetShopAddress(offset),
                    CheckType = LocationCheckType.UInt,
                    CheckValue = "1",
                    CompareType = LocationCheckCompareType.Match,
                    Category = "Hollywood Shop Items"
                });
            }

            foreach (var shoes in hwShoes)
            {
                string itemName = shoes.Key;
                uint offset = shoes.Value;
                shoesindex++;
                int baseId = 10370000;

                locations.Add(new Location
                {
                    Id = baseId + shoesindex,
                    Name = itemName,
                    Address = GetShopAddress(offset),
                    CheckType = LocationCheckType.UInt,
                    CheckValue = "1",
                    CompareType = LocationCheckCompareType.Match,
                    Category = "Hollywood Shop Items"
                });
            }

            foreach (var socks in hwSocks)
            {
                string itemName = socks.Key;
                uint offset = socks.Value;
                socksindex++;
                int baseId = 10380000;

                locations.Add(new Location
                {
                    Id = baseId + socksindex,
                    Name = itemName,
                    Address = GetShopAddress(offset),
                    CheckType = LocationCheckType.UInt,
                    CheckValue = "1",
                    CompareType = LocationCheckCompareType.Match,
                    Category = "Hollywood Shop Items"
                });
            }

            foreach (var hair in hwHair)
            {
                string itemName = hair.Key;
                uint offset = hair.Value;
                hairindex++;
                int baseId = 10390000;

                locations.Add(new Location
                {
                    Id = baseId + hairindex,
                    Name = itemName,
                    Address = GetShopAddress(offset),
                    CheckType = LocationCheckType.UInt,
                    CheckValue = "1",
                    CompareType = LocationCheckCompareType.Match,
                    Category = "Hollywood Shop Items"
                });
            }

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
            { "HW Deck: RDS1", 0x578},
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
            { "HW Shoes: Emerica ReynoldS3", 0x6b8},
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
    }
}