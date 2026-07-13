using Archipelago.Core.Util;
using THAWAPClient.Helpers;
using Serilog;
using Archipelago.Core;

namespace THAWAPClient.Models
{
    public static class TrickCashing

    {
        public static int SavedScanValue { get; set; } = 0;

        private static CancellationTokenSource? _cts4tc;

        public static void StartTrickCashLoop()
        {
            if (_cts4tc != null)
                return; // already running

            _cts4tc = new CancellationTokenSource();
            _ = StartTrickCashLoopAsync(_cts4tc.Token);
        }

        public static void StopTrickCashLoop()
        {
            _cts4tc?.Cancel();
            _cts4tc = null;
        }

        public static async Task StartTrickCashLoopAsync(CancellationToken token)
        {
            try
            {
                while (!token.IsCancellationRequested)
                {
                    await EvaluateCashAsync();

                    await Task.Delay(TimeSpan.FromSeconds(0.15), token);
                }
            }
            catch (TaskCanceledException)
            {
                // Expected when stopping — no action needed
            }
        }

        public static async Task EvaluateCashAsync()
        {   if (!LevelID.IsInGame())
                {return;}
            int scorescanresult = Memory.ReadInt(Addresses.TopLeftScore);
            if (scorescanresult != SavedScanValue && scorescanresult > 4000)
            {
                SavedScanValue = scorescanresult;
                int cashtogive = scorescanresult/4000;
                CashHelper.AddCash(cashtogive);
            };
        }
    }
}