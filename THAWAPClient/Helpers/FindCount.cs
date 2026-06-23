using Archipelago.Core;

namespace THAWAPClient.Helpers
{
    public class FindCount
    {
        public static float ArchiCountingFloat(ArchipelagoClient Client, string APname)
        {
            float tempcount = 0;
            var allItems = Client.CurrentSession.Items.AllItemsReceived;
            for (int i = 0; i < allItems.Count; i++)
            {
                if (allItems[i].ItemName.Contains(APname))
                    tempcount++;
            }
            return tempcount;
        }

        public static int ArchiCountingInt(ArchipelagoClient Client, string APname)
        {
            int tempcount = 0;
            var allItems = Client.CurrentSession.Items.AllItemsReceived;
            for (int i = 0; i < allItems.Count; i++)
            {
                if (allItems[i].ItemName.Contains(APname))
                    tempcount++;
            }
            return tempcount;
        }
    }
}