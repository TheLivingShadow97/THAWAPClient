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
            { 0x3D68B260, new MissionInfo("HW Mission: Fifth Tagging Mission", 10150005) }
        };
    }
}
