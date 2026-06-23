using Archipelago.Core.Util;


namespace THAWAPClient.Helpers
{
    public class CashHelper
    {
        public static void AddCash(int addamount)
        {
            int finalcash = Memory.ReadInt(Addresses.CurrentCash)+addamount;
            Memory.Write(Addresses.CurrentCash, finalcash);
        }
    }
}