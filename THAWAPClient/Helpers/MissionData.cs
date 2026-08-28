namespace THAWAPClient.Helpers
{
    public class MissionData
    {
        public record MissionInfo(string Name, int ApId);
        public static readonly Dictionary<uint, MissionInfo> Missions = new()
        {
            // HW Stage 1 Missions
            { 0x666F7029, new MissionInfo("HW Mission: Change Your Look", 10100001) },
            { 0x2433E624, new MissionInfo("HW Mission: Change Your Clothes", 10100002) },
            { 0x94E3B022, new MissionInfo("HW Mission: Learn to Caveman", 10100003) },
            { 0x5334D6B2, new MissionInfo("HW Mission: Do a Sponsor Challenge", 10100004) },
            { 0xF042B255, new MissionInfo("HW Mission: Kickflip Whofleck", 10100005) },
            { 0xF44056E1, new MissionInfo("HW Mission: Learn to Revert", 10100006) },
            { 0x6E2627F6, new MissionInfo("HW Mission: Get Your Stuff Back", 10100007) },
            { 0x2E30A2B8, new MissionInfo("HW Mission: Get Into Beverly Hills", 10100008) },
            { 0x3A057679, new MissionInfo("HW Mission: First Tagging Mission", 10150001) },
            { 0x4D0246EF, new MissionInfo("HW Mission: Second Tagging Mission", 10150002) },
            { 0xD366D34C, new MissionInfo("HW Mission: Third Tagging Mission", 10150003) },
            { 0xA461E3DA, new MissionInfo("HW Mission: Fourth Tagging Mission", 10150004) },
            { 0x3D68B260, new MissionInfo("HW Mission: Fifth Tagging Mission", 10150005) },
            // Beverly Hills Stage 1 Missions
            { 0x8E2DEC40, new MissionInfo("BH Mission: Learn the Natas Spin", 20100001) },
            { 0xECF5D341, new MissionInfo("BH Mission: Learn Parkour tricks", 20100002) },
            { 0x05967674, new MissionInfo("BH Mission: Learn Spines, Flips, Rolls, Acid Drops, Banks", 20100003) },
            { 0xDA8B20C7, new MissionInfo("BH Mission: Learn the Boneless and Boned Ollie", 20100004) },
            { 0x962167F4, new MissionInfo("BH Mission: Learn some wall tricks", 20100005) },
            { 0x0B655A1C, new MissionInfo("BH Mission: Impress Murphy", 20100006) },
            { 0x7C626A8A, new MissionInfo("BH Mission: Impress Boone", 20100007) },
            { 0x9501CFBF, new MissionInfo("BH Mission: Impress Dave", 20100008) },
            { 0x38C7A1AF, new MissionInfo("BH Mission: First Tagging Mission", 20150009) },
            { 0x4FC09139, new MissionInfo("BH Mission: Second Tagging Mission", 20150010) },
            { 0xD1A4049A, new MissionInfo("BH Mission: Third Tagging Mission", 20150011) },
            { 0xA6A3340C, new MissionInfo("BH Mission: Fourth Tagging Mission", 20150012) },
            { 0x3FAA65B6, new MissionInfo("BH Mission: Fifth Tagging Mission", 20150013) },
            // Skate Ranch First Missions
            { 0x55C9D19C, new MissionInfo("SR Mission: Skitch Sanchez", 30100001) },
            { 0x313B473E, new MissionInfo("SR Mission: Learn the Bert Slide from Iggy", 30100002) },
            // Downtown Stage 1 Missions
            { 0x567AD81C, new MissionInfo("DT Mission: First Tagging Mission", 40100001) },
            { 0x217DE88A, new MissionInfo("DT Mission: Second Tagging Mission", 40100002) },
            { 0xBF197D29, new MissionInfo("DT Mission: Third Tagging Mission", 40100003) },
            { 0xC81E4DBF, new MissionInfo("DT Mission: Fourth Tagging Mission", 40100004) },
            { 0x51171C05, new MissionInfo("DT Mission: Fifth Tagging Mission", 40100005) },
            // Hollywood Stage 2 Missions
            { 0xA42186DF, new MissionInfo("HW Mission: Buy the Dino Head", 11100001) },
            { 0x8F0CD51C, new MissionInfo("HW Mission: Help Mr. D get into Downtown", 11100002) },
            { 0x6FD93C02, new MissionInfo("HW Mission: Get the red velvet ropes", 11100003) },
            { 0x802846DA, new MissionInfo("HW Mission: Get the Radio Tower", 11100004) },
            { 0x3D28D765, new MissionInfo("HW Mission: Get some stars", 11100005) },
            { 0x88611105, new MissionInfo("HW Mission: Get the giant record needle", 11100006) },
            // Beverly Hills Stage 2 Missions
            { 0x20E1A85F, new MissionInfo("BH Mission: Get the 69 gas station sign", 21100001) },
            { 0xDA94B486, new MissionInfo("BH Mission: Get the metal awning", 21100002) },
            { 0xC9820D6A, new MissionInfo("BH Mission: Get the naked lady statue", 21100003) },
            { 0xBE2C9C7C, new MissionInfo("BH Mission: Get the Green Top", 21100004) },
            // Downtown Stage 2 Missions
            { 0x6F3E0CF2, new MissionInfo("DT Mission: Knock the fire escape down", 41100001) },
            { 0x5790293B, new MissionInfo("DT Mission: Kick off the bell", 41100002) },
            { 0xF6375D48, new MissionInfo("DT Mission: Help Joey B. get to the manhole", 41100003) },
            { 0xBEF38C0E, new MissionInfo("DT Mission: Loosen the Pyramid!", 41100004) },
            { 0xFE11E769, new MissionInfo("DT Mission: Smash the Chinatown tower!", 41100005) },
            { 0xF89C1E47, new MissionInfo("DT Mission: Learn the Board Stall", 41100006) },
            { 0xC5FC1FAC, new MissionInfo("DT Mission: Learn Special and Focus", 41100007) },
            // Skate Ranch Piece Missions
            { 0x0EF99F38, new MissionInfo("SR Mission: Land a Combo on the T-rex", 31100001) },
            { 0x79FEAFAE, new MissionInfo("SR Mission: Combo the Pipes and Radio Tower", 31100002) },
            { 0xE0F7FE14, new MissionInfo("SR Mission: Combo the Velvet Ropes", 31100003) },
            { 0x97F0CE82, new MissionInfo("SR Mission: Combo the Walk of Fame", 31100004) },
            { 0x17E2AE79, new MissionInfo("SR Mission: Combo the Broken Floor from Beverly Hills", 31100005) },
            { 0x60E59EEF, new MissionInfo("SR Mission: Combo the Bag Shop Arch", 31100006) },
            { 0xF9ECCF55, new MissionInfo("SR Mission: Combo the Gate Roll-in", 31100007) },
            { 0x108F6A60, new MissionInfo("SR Mission: Combo the Green Dome", 31100008) },
            { 0xFE810B4C, new MissionInfo("SR Mission: Combo the Freeway Sign", 31100009) },
            { 0x89863BDA, new MissionInfo("SR Mission: Combo the Bell", 31100010) },
            { 0xAC70E02B, new MissionInfo("SR Mission: Combo the Pyramid", 31100011) },
            { 0x3CCFFDBA, new MissionInfo("SR Mission: Combo the Piece of Road from Downtown", 31100012) },
            { 0x4BC8CD2C, new MissionInfo("SR Mission: Combo the Fire Escape", 31100013) },
            { 0xD2C19C96, new MissionInfo("SR Mission: Combo the Chinese Tower", 31100014) },
            { 0xB56BD16A, new MissionInfo("SR Mission: Combo the Naked Lady Statue", 31100015) },
            { 0xCBDAADD7, new MissionInfo("SR Mission: Combo the Quarterpipe", 31100016) },
            // Vans Park Missions
            { 0xB7B71F9F, new MissionInfo("VP Mission: Get into the Tony Hawk AMJAM", 50100001) },
            { 0x4D8731E0, new MissionInfo("VP Mission: Pay the fee and get sponsored", 50100002) },
            { 0x3A800176, new MissionInfo("VP Mission: Win the Tony Hawk AMJAM", 50100003) },
        };
    }
}
