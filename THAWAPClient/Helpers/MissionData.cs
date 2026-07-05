namespace THAWAPClient.Helpers
{
    public class MissionData
    {
        public record MissionInfo(string Name, int ApId);
        public static readonly Dictionary<uint, MissionInfo> Missions = new()
        {
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
            // Beverly Hills Missions
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
        };
    }
}
