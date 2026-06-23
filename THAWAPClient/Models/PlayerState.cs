using Archipelago.Core.Util;
using THAWAPClient.Helpers;
using Serilog;
using Archipelago.Core;

namespace THAWAPClient.Models
{
    public static class PlayerState
    {
        //public static int HeldCash { get; set; }

        //stats
        public static float StatAir { get; set => field = Math.Min(value, 10); } = 0;
        public static float StatRun { get; set => field = Math.Min(value, 10); } = 0;
        public static float StatOllie { get; set => field = Math.Min(value, 10); } = 0;
        public static float StatSpeed { get; set => field = Math.Min(value, 10); } = 0;
        public static float StatSpin { get; set => field = Math.Min(value, 10); } = 0;
        public static float StatFlip { get; set => field = Math.Min(value, 10); } = 0;
        public static float StatSwitch { get; set => field = Math.Min(value, 10); } = 0;
        public static float StatRail { get; set => field = Math.Min(value, 10); } = 0;
        public static float StatLip { get; set => field = Math.Min(value, 10); } = 0;
        public static float StatManual { get; set => field = Math.Min(value, 10); } = 0;

        //skater abilities
        public static int HasManual { get; set; } = 0;
        public static int HasRevert { get; set; } = 0;
        public static int HasSpineTransfer { get; set; } = 0;
        public static int HasWallRide { get; set; } = 0;
        public static int HasStickerSlap { get; set; } = 0;
        public static int HasFlatland { get; set; } = 0;
        public static int HasNatasSpin { get; set; } = 0;
        public static int HasBoneless { get; set; } = 0;
        public static int HasSpecial { get; set; } = 0;
        public static int HasFocus { get; set; } = 0;
        public static int HasFlips { get; set; } = 0;
        public static int HasStall { get; set; } = 0;
        public static int HasSkitch { get; set; } = 0;
        public static int HasCaveman { get; set; } = 0;
        public static int HasWallRun { get; set; } = 0;
        public static int HasShimmy { get; set; } = 0;
        public static int HasWallFlip { get; set; } = 0;
        public static int HasBertSlide { get; set; } = 0;
        public static int HasTuck { get; set; } = 0;
        public static int HasBonedOllie { get; set; } = 0;

        private static CancellationTokenSource? _cts;

        public static void StartFixLoop()
        {
            if (_cts != null)
                return; // already running

            _cts = new CancellationTokenSource();
            _ = StartFixLoopAsync(_cts.Token);
        }

        public static void StopFixLoop()
        {
            _cts?.Cancel();
            _cts = null;
        }

        public static async Task StartFixLoopAsync(CancellationToken token)
        {
            try
            {
                while (!token.IsCancellationRequested)
                {
                    await FixAllStatsAsync();
                    await FixAllAbilitiesAsync();

                    await Task.Delay(TimeSpan.FromSeconds(1), token);
                }
            }
            catch (TaskCanceledException)
            {
                // Expected when stopping — no action needed
            }
        }

        public static async Task FixAllStatsAsync()
        {   if (!LevelID.IsInGame())
                {return;}
            await Task.WhenAll(
                FixAirStatAsync(),
                FixRunStatAsync(),
                FixOllieStatAsync(),
                FixSpeedStatAsync(),
                FixSpinStatAsync(),
                FixFlipStatAsync(),
                FixSwitchStatAsync(),
                FixRailStatAsync(),
                FixLipStatAsync(),
                FixManualStatAsync()
            );
        }

        public static async Task FixAllAbilitiesAsync()
        {   if (!LevelID.IsInGame())
                {return;}
            await Task.WhenAll(
                FixManualAbilityAsync(),
                FixRevertAbilityAsync(),
                FixSpineTransferAbilityAsync(),
                FixWallRideAbilityAsync(),
                FixStickerSlapAbilityAsync(),
                FixFlatlandAbilityAsync(),
                FixNatasSpinAbilityAsync(),
                FixBonelessAbilityAsync(),
                FixSpecialAbilityAsync(),
                FixFocusAbilityAsync(),
                FixFlipsAbilityAsync(),
                FixStallAbilityAsync(),
                FixSkitchAbilityAsync(),
                FixCavemanAbilityAsync(),
                FixWallRunAbilityAsync(),
                FixShimmyAbilityAsync(),
                FixWallFlipAbilityAsync(),
                FixBertSlideAbilityAsync(),
                FixTuckAbilityAsync(),
                FixBonedOllieAbilityAsync()
            );
        }

        private static async Task FixStatAsync(ulong address, float desiredValue)
        {
            await Task.Run(() =>
            {
                float current = Memory.ReadFloat(address);
                if (current != desiredValue)
                {
                    Memory.Write(address, desiredValue);
                }
            });
        }

        private static async Task FixAbilityAsync(ulong address, int desiredValue)
        {
            await Task.Run(() =>
            {
                int current = Memory.ReadInt(address);
                if (current != desiredValue)
                {
                    Memory.Write(address, desiredValue);
                }
            });
        }

        public static Task FixAirStatAsync() => FixStatAsync(Addresses.AirStat, StatAir);
        public static Task FixRunStatAsync() => FixStatAsync(Addresses.RunStat, StatRun);
        public static Task FixOllieStatAsync() => FixStatAsync(Addresses.OllieStat, StatOllie);
        public static Task FixSpeedStatAsync() => FixStatAsync(Addresses.SpeedStat, StatSpeed);
        public static Task FixSpinStatAsync() => FixStatAsync(Addresses.SpinStat, StatSpin);
        public static Task FixFlipStatAsync() => FixStatAsync(Addresses.FlipStat, StatFlip);
        public static Task FixSwitchStatAsync() => FixStatAsync(Addresses.SwitchStat, StatSwitch);
        public static Task FixRailStatAsync() => FixStatAsync(Addresses.RailStat, StatRail);
        public static Task FixLipStatAsync() => FixStatAsync(Addresses.LipStat, StatLip);
        public static Task FixManualStatAsync() => FixStatAsync(Addresses.ManualStat, StatManual);
        
        public static Task FixManualAbilityAsync() => FixAbilityAsync(Addresses.AbilityManual, HasManual);
        public static Task FixRevertAbilityAsync() => FixAbilityAsync(Addresses.AbilityRevert, HasRevert);
        public static Task FixSpineTransferAbilityAsync() => FixAbilityAsync(Addresses.AbilitySpineTransfer, HasSpineTransfer);
        public static Task FixWallRideAbilityAsync() => FixAbilityAsync(Addresses.AbilityWallRide, HasWallRide);
        public static Task FixStickerSlapAbilityAsync() => FixAbilityAsync(Addresses.AbilityStickerSlap, HasStickerSlap);
        public static Task FixFlatlandAbilityAsync() => FixAbilityAsync(Addresses.AbilityFlatland, HasFlatland);
        public static Task FixNatasSpinAbilityAsync() => FixAbilityAsync(Addresses.AbilityNatasSpin, HasNatasSpin);
        public static Task FixBonelessAbilityAsync() => FixAbilityAsync(Addresses.AbilityBoneless, HasBoneless);
        public static Task FixSpecialAbilityAsync() => FixAbilityAsync(Addresses.AbilitySpecial, HasSpecial);
        public static Task FixFocusAbilityAsync() => FixAbilityAsync(Addresses.AbilityFocus, HasFocus);
        public static Task FixFlipsAbilityAsync() => FixAbilityAsync(Addresses.AbilityFlips, HasFlips);
        public static Task FixStallAbilityAsync() => FixAbilityAsync(Addresses.AbilityStall, HasStall);
        public static Task FixSkitchAbilityAsync() => FixAbilityAsync(Addresses.AbilitySkitch, HasSkitch);
        public static Task FixCavemanAbilityAsync() => FixAbilityAsync(Addresses.AbilityCaveman, HasCaveman);
        public static Task FixWallRunAbilityAsync() => FixAbilityAsync(Addresses.AbilityWallRun, HasWallRun);
        public static Task FixShimmyAbilityAsync() => FixAbilityAsync(Addresses.AbilityShimmy, HasShimmy);
        public static Task FixWallFlipAbilityAsync() => FixAbilityAsync(Addresses.AbilityWallFlip, HasWallFlip);
        public static Task FixBertSlideAbilityAsync() => FixAbilityAsync(Addresses.AbilityBertSlide, HasBertSlide);
        public static Task FixTuckAbilityAsync() => FixAbilityAsync(Addresses.AbilityTuck, HasTuck);
        public static Task FixBonedOllieAbilityAsync() => FixAbilityAsync(Addresses.AbilityBonedOllie, HasBonedOllie);

        public static void PrintCurrentStats()
            {
                Log.Logger.Warning("--- Current Archipelago Stats --- ");
                Log.Logger.Warning("Air Stat = " + StatAir.ToString());
                Log.Logger.Warning("Run Stat = " + StatRun.ToString());
                Log.Logger.Warning("Ollie Stat = " + StatOllie.ToString());
                Log.Logger.Warning("Speed Stat = " + StatSpeed.ToString());
                Log.Logger.Warning("Spin Stat = " + StatSpin.ToString());
                Log.Logger.Warning("Flip Stat = " + StatFlip.ToString());
                Log.Logger.Warning("Switch Stat = " + StatSwitch.ToString());
                Log.Logger.Warning("Rail Stat = " + StatRail.ToString());
                Log.Logger.Warning("Lip Stat = " + StatLip.ToString());
                Log.Logger.Warning("Manual Stat = " + StatManual.ToString());
                Log.Logger.Warning("--- End of THAWAP stats. ---");
            }

        public static void UpdateArchiStats(ArchipelagoClient Client)
        {   StatAir = FindCount.ArchiCountingFloat(Client, "Progressive Air Stat");
            StatRun = FindCount.ArchiCountingFloat(Client, "Progressive Run Stat");
            StatOllie = FindCount.ArchiCountingFloat(Client, "Progressive Ollie Stat");
            StatSpeed = FindCount.ArchiCountingFloat(Client, "Progressive Speed Stat");
            StatSpin = FindCount.ArchiCountingFloat(Client, "Progressive Spin Stat");
            StatFlip = FindCount.ArchiCountingFloat(Client, "Progressive Flip Stat");
            StatSwitch = FindCount.ArchiCountingFloat(Client, "Progressive Switch Stat");
            StatRail = FindCount.ArchiCountingFloat(Client, "Progressive Rail Stat");
            StatLip = FindCount.ArchiCountingFloat(Client, "Progressive Lip Stat");
            StatManual = FindCount.ArchiCountingFloat(Client, "Progressive Manual Stat");
        }

        public static void UpdateArchiAbilities(ArchipelagoClient Client)
        {   HasManual = FindCount.ArchiCountingInt(Client, "Skate Ability: Manual");
            HasRevert = FindCount.ArchiCountingInt(Client, "Skate Ability: Revert");
            HasSpineTransfer = FindCount.ArchiCountingInt(Client, "Skate Ability: Spine Transfer/Acid Drop/Bank Drop");
            HasWallRide = FindCount.ArchiCountingInt(Client, "Skate Ability: Wall Ride");
            HasStickerSlap = FindCount.ArchiCountingInt(Client, "Skate Ability: Sticker Slap/Wall Plant/Vert Wall Plant");
            HasFlatland = FindCount.ArchiCountingInt(Client, "Skate Ability: Flatland");
            HasNatasSpin = FindCount.ArchiCountingInt(Client, "Skate Ability: Natas Spin");
            HasBoneless = FindCount.ArchiCountingInt(Client, "Skate Ability: Boneless");
            HasSpecial = FindCount.ArchiCountingInt(Client, "Skate Ability: Special");
            HasFocus = FindCount.ArchiCountingInt(Client, "Skate Ability: Focus");
            HasFlips = FindCount.ArchiCountingInt(Client, "Skate Ability: Flips/Rolls");
            HasStall = FindCount.ArchiCountingInt(Client, "Skate Ability: Stall");
            HasSkitch = FindCount.ArchiCountingInt(Client, "Skate Ability: Skitch");
            HasCaveman = FindCount.ArchiCountingInt(Client, "Skate Ability: Caveman");
            HasWallRun = FindCount.ArchiCountingInt(Client, "Skate Ability: Wall Run");
            HasShimmy = FindCount.ArchiCountingInt(Client, "Skate Ability: Shimmy");
            HasWallFlip = FindCount.ArchiCountingInt(Client, "Skate Ability: Wall Flip");
            HasBertSlide = FindCount.ArchiCountingInt(Client, "Skate Ability: Bert Slide");
            HasTuck = FindCount.ArchiCountingInt(Client, "Skate Ability: Back Tuck/Front Tuck");
            HasBonedOllie = FindCount.ArchiCountingInt(Client, "Skate Ability: Boned Ollie");
        }
        public static void UpdateSkater(ArchipelagoClient Client)
        {
            UpdateArchiStats(Client);
            UpdateArchiAbilities(Client);
        }
    }
}