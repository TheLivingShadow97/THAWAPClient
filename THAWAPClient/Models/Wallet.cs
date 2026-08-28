using Archipelago.Core;
using Archipelago.Core.Util;
using THAWAPClient.Helpers;
using Serilog;
using Silk.NET.Core;

namespace THAWAPClient.Models
{
    public class WalletManaging
    {
        public static int WalletCount { get; set; } = 0;

        public static void UpdateWallets(ArchipelagoClient Client)
        {
            WalletCount = FindCount.ArchiCountingInt(Client, "Progressive Wallet");
            switch (WalletCount)
            {case 0:
                {CashHelper.AllowedMaxCash = 5;}
                break;
            case 1:
                {CashHelper.AllowedMaxCash = 10;}
                break;
            case 2:
                {CashHelper.AllowedMaxCash = 20;}
                break;
            case 3:
                {CashHelper.AllowedMaxCash = 30;}
                break;
            case 4:
                {CashHelper.AllowedMaxCash = 50;}
                break;
            case 5:
                {CashHelper.AllowedMaxCash = 100;}
                break;
            case 6:
                {CashHelper.AllowedMaxCash = 250;}
                break;
            case 7:
                {CashHelper.AllowedMaxCash = 600;}
                break;
            case 8:
                {CashHelper.AllowedMaxCash = 1250;}
                break;
            case 9:
                {CashHelper.AllowedMaxCash = 2500;}
                break;
            case 10:
                {CashHelper.AllowedMaxCash = 5000;}
                break;
            case 11:
                {CashHelper.AllowedMaxCash = 10000;}
                break;
            case 12:
                {CashHelper.AllowedMaxCash = 20000;}
                break;
            case 13:
                {CashHelper.AllowedMaxCash = 40000;}
                break;
            case 14:
                {CashHelper.AllowedMaxCash = 80000;}
                break;
            case 15:
                {CashHelper.AllowedMaxCash = 200000;}
                break;
            }
            Log.Logger.Warning("You can now carry $"+CashHelper.AllowedMaxCash.ToString());
        }

        private static CancellationTokenSource? _cts4w;

        public static void StartWalletLoop()
        {
            if (_cts4w != null)
                return; // already running

            _cts4w = new CancellationTokenSource();
            _ = StartWalletLoopAsync(_cts4w.Token);
        }

        public static void StopWalletLoop()
        {
            _cts4w?.Cancel();
            _cts4w = null;
        }

        public static async Task StartWalletLoopAsync(CancellationToken token)
        {
            try
            {
                while (!token.IsCancellationRequested)
                {
                    await EvaluateMaxCashAsync();

                    await Task.Delay(TimeSpan.FromSeconds(1), token);
                }
            }
            catch (TaskCanceledException)
            {
                // Expected when stopping — no action needed
            }
        }

        public static async Task EvaluateMaxCashAsync()
        {   if (!LevelID.IsInGame())
                {return;}
            if (CashHelper.IsCashEditing == true)
                {return;}
            int currentcash = Memory.ReadInt(Addresses.CurrentCash);
            if (currentcash > CashHelper.AllowedMaxCash)
            {
                Memory.Write(Addresses.CurrentCash, CashHelper.AllowedMaxCash);
            }
        }
            
    }
}