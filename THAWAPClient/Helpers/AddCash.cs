using Archipelago.Core.Util;

namespace THAWAPClient.Helpers
{
    public class CashHelper
    {
        public static bool IsCashEditing { get; set; } = false;
        public static int AllowedMaxCash { get; set; } = 20000000;

        public static void AddCash(int addamount)
        {   IsCashEditing = true;
            int finalcash = Memory.ReadInt(Addresses.CurrentCash)+addamount;
            if (finalcash > AllowedMaxCash)
            {
                finalcash = AllowedMaxCash;
            }
            Memory.Write(Addresses.CurrentCash, finalcash);
            IsCashEditing = false;
        }
    }
}