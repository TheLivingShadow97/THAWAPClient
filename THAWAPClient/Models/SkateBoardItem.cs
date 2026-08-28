using Archipelago.Core.Util;
using THAWAPClient.Helpers;
using Serilog;
using Archipelago.Core;

namespace THAWAPClient.Models
{
    public static class SkateboardItem
    {
        public static volatile bool HasSkateboard = false;

        private static CancellationTokenSource? _cts4hs;

        public static void StartSkateboardLoop()
        {
            if (_cts4hs != null)
                return; // already running

            _cts4hs = new CancellationTokenSource();
            _ = StartSkateboardLoopAsync(_cts4hs.Token);
        }

        public static void StopSkateboardLoop()
        {
            _cts4hs?.Cancel();
            _cts4hs = null;
        }

        public static async Task StartSkateboardLoopAsync(CancellationToken token)
        {
            try
            {
                while (!token.IsCancellationRequested)
                {
                    await EvaluateSkateboardAsync();

                    await Task.Delay(TimeSpan.FromMilliseconds(350), token);
                }
            }
            catch (TaskCanceledException)
            {
                // Expected when stopping — no action needed
            }
        }

        public static async Task EvaluateSkateboardAsync()
        {   if (!LevelID.IsInGame())
                {return;}
            if (HasSkateboard == true)
                {
                    ulong HasBoardAddress = Memory.ReadUInt(Addresses.SkaterPointer) +0x88;
                    if (Memory.ReadBit(HasBoardAddress,0)!= true)
                        {Memory.WriteBit(HasBoardAddress, 0, true);}
                    if (Memory.ReadByte(Addresses.SkateboardDisabled)!= 0)
                        {Memory.Write(Addresses.SkateboardDisabled, 0);}
                    StopSkateboardLoop();
                    return;}
            
            else if (HasSkateboard == false)
                {
                    ulong HasBoardAddress = Memory.ReadUInt(Addresses.SkaterPointer) +0x88;
                    if (Memory.ReadBit(HasBoardAddress,0)!= false)
                        {Memory.WriteBit(HasBoardAddress, 0, false);}
                    if (Memory.ReadByte(Addresses.SkateboardDisabled) != 1)
                        {Memory.Write(Addresses.SkateboardDisabled, 1);}
                    return;}
            
        }

        public static void UpdateSkateboard(ArchipelagoClient Client)
        {
            int BoardCount = FindCount.ArchiCountingInt(Client, "Skateboard Unlock");
            if (BoardCount >= 1)
                {HasSkateboard=true;
                Log.Logger.Warning("Your skateboard is currently unlocked");}
            else
                {HasSkateboard=false;
                Log.Logger.Warning("Your skateboard is not currently unlocked");}
        }
            
    
    }
}