using Archipelago.Core.Util;
using THAWAPClient.Helpers;
using Serilog;
using Archipelago.Core;

namespace THAWAPClient.Models
{
    public static class ShopKeys
    {
        public static int LastDetectedLevel = 0; 
        public static bool HasBeverlyHillsShopKey = false;
        public static bool HasDowntownShopKey = false;

        private static CancellationTokenSource? _cts4sk;

        public static void StartShopKeyLoop()
        {
            if (_cts4sk != null)
                return; // already running

            _cts4sk = new CancellationTokenSource();
            _ = StartShopKeyLoopAsync(_cts4sk.Token);
        }

        public static void StopShopkeyLoop()
        {
            _cts4sk?.Cancel();
            _cts4sk = null;
        }

        public static async Task StartShopKeyLoopAsync(CancellationToken token)
        {
            try
            {
                while (!token.IsCancellationRequested)
                {
                    await EvaluateShopKeyAsync();

                    await Task.Delay(TimeSpan.FromSeconds(1), token);
                }
            }
            catch (TaskCanceledException)
            {
                // Expected when stopping — no action needed
            }
        }

        public static async Task EvaluateShopKeyAsync()
        {   if (!LevelID.IsInGame())
                {return;}
            int CurrentLevel = LevelID.GetCurrentLevel();
            if (CurrentLevel != LastDetectedLevel)
            {
                LastDetectedLevel = CurrentLevel;
                if (CurrentLevel == (int)LevelID.CurrentLevel.Hollywood)
                {UnlockShops();}
                if ((CurrentLevel == (int)LevelID.CurrentLevel.BeverlyHills) && (HasBeverlyHillsShopKey==true))
                {}
                if ((CurrentLevel == (int)LevelID.CurrentLevel.BeverlyHills) && (HasBeverlyHillsShopKey==false))
                {}
            }
            
        }

        public static void UnlockShops()
        {}    
        public static void LockShops()
        {}
    }
}