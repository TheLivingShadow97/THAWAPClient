using Archipelago.Core;
using System.Text.Json;


namespace THAWAPClient.Helpers
{
    public class THAWAPSaveData
    {
        public string RoomSeed { get; set; } = "0";
        public int APGivenCash {get; set;} = 0;
        
    }
    public class SavingAndLoading
    {
        public static string GetRoomSeed(ArchipelagoClient Client)
        {   string roomseed = Client.CurrentSession.RoomState.Seed;
            return roomseed;
        }
        public static void UpdateSaveData(string roomseed , int apgivengold)
        {
            var newsavedata = new THAWAPSaveData
            {
                RoomSeed = roomseed,
                APGivenCash = apgivengold
            };
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(newsavedata, options);
            File.WriteAllText("THAWAPSaveData.json", jsonString);
        }
        public static THAWAPSaveData LoadSaveData()
        {
            if (File.Exists("THAWAPSaveData.json")) {
                string jsonString = File.ReadAllText("THAWAPSaveData.json");
                THAWAPSaveData loadedsavedata = JsonSerializer.Deserialize<THAWAPSaveData>(jsonString)!;
                return loadedsavedata;
            }
            else
            {
                UpdateSaveData("0",0);
                string jsonString = File.ReadAllText("THAWAPSaveData.json");
                THAWAPSaveData loadedsavedata = JsonSerializer.Deserialize<THAWAPSaveData>(jsonString)!;
                return loadedsavedata;
            }
        }
    }
}