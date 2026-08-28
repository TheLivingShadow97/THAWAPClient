using System;
using Archipelago.Core;
using Archipelago.Core.Models;
using Archipelago.Core.Util;
using THAWAPClient.Helpers;
using Archipelago.MultiClient.Net.BounceFeatures.DeathLink;
using Serilog;
using Avalonia.Media.Imaging;
using Silk.NET.Core;

namespace THAWAPClient.Models
{
    public class Deathlinking
    {
        private static DeathLinkService _deathLinkService { get; set; }
        public static bool deathlink_enabled { get; set; } = false;
        public static bool DeathLinkWasReceived = false;
        public static int NumberofBails = 0;
        public static int NumberofBailsNeeded = 1;
        public static async Task BailPlayer(DeathLink deathLink, ArchipelagoClient Client)
        {
            if (deathlink_enabled==false)
            {return;}
            DeathLinkWasReceived=true;
            await StartBailLoop(deathLink, Client);
            await Task.Delay(TimeSpan.FromSeconds(6));
            StopBailLoop();
            DeathLinkWasReceived=false;
        }
        public static async Task ToggleDeathlink(ArchipelagoClient Client, TonyHawkOptions Options)
        {
            if (deathlink_enabled==false)
            {   Log.Logger.Information("Starting Deathlink Service.");
                StartDeathLinkLoop(Client, Options);}
            if (deathlink_enabled==true)
            {   Log.Logger.Information("Stopping Deathlink Service.");
                StopDeathLinkLoop();}
        }

        private static CancellationTokenSource? _cts4bp;

        public static async Task StartBailLoop(DeathLink deathLink, ArchipelagoClient Client)
        {
            if (deathLink.Source == Client.CurrentSession.Players.ActivePlayer.Name)
        {
            return;
        }
            if (_cts4bp != null)
                return; // already running
            
            _cts4bp = new CancellationTokenSource();
            
            _ = StartBailLoopAsync(_cts4bp.Token);
        }

        public static void StopBailLoop()
        {
            _cts4bp?.Cancel();
            _cts4bp = null;
        }

        public static async Task StartBailLoopAsync(CancellationToken token)
        {
            try
            {
                while (!token.IsCancellationRequested)
                {
                    await EvaluateBailAsync();

                    await Task.Delay(TimeSpan.FromSeconds(0.2), token);
                }
            }
            catch (TaskCanceledException)
            {
                // Expected when stopping — no action needed
            }
        }

        public static async Task EvaluateBailAsync()
        {
            if (Memory.ReadBit(Addresses.IgnoreInput,0) == true)
            {   StopBailLoop();
                DebugWriter.LogDeathlinkDebug("Player bailed successfully on deathlink receive.");
                return;}
            await Task.WhenAll(
                MakeBailable(Addresses.ComboGrindSeconds),
                MakeBailable(Addresses.ComboManualSeconds),
                MakeBailable(Addresses.ComboLipSeconds)
            );
        }

        public static async Task MakeBailable(ulong Address)
        {if (Memory.ReadFloat(Address)!=200)
            {
                Memory.Write(Address, 200);
            }
        }

        private static CancellationTokenSource? _cts4dl;

        public static void StartDeathLinkLoop(ArchipelagoClient Client, TonyHawkOptions Options)
        {
            if (_cts4dl != null)
                return; // already running

            _cts4dl = new CancellationTokenSource();
            deathlink_enabled = true;
            if (_deathLinkService == null)
            {
            DebugWriter.LogDeathlinkDebug("Starting Deathlink Loop");
            _deathLinkService = Client.EnableDeathLink();
            _deathLinkService.OnDeathLinkReceived += (args) => BailPlayer(args,Client);}
            switch (Options.DeathlinkSettings)
            {
            case (uint)TonyHawkOptions.DeathlinkChoice.option_1_bail:
                {NumberofBailsNeeded=1;
                Log.Logger.Warning("Deathlink is on and requires 1 bail to send.");}
                break;
            case (uint)TonyHawkOptions.DeathlinkChoice.option_5_bails:
                {NumberofBailsNeeded=5;
                Log.Logger.Warning("Deathlink is on and requires 5 bails to send.");}
                break;
            case (uint)TonyHawkOptions.DeathlinkChoice.option_10_bails:
                {NumberofBailsNeeded=10;
                Log.Logger.Warning("Deathlink is on and requires 10 bails to send.");}
                break;
            case (uint)TonyHawkOptions.DeathlinkChoice.option_15_bails:
                {NumberofBailsNeeded=15;
                Log.Logger.Warning("Deathlink is on and requires 15 bails to send.");}
                break;
            case (uint)TonyHawkOptions.DeathlinkChoice.option_20_bails:
                {NumberofBailsNeeded=20;
                Log.Logger.Warning("Deathlink is on and requires 20 bails to send.");}
                break;
                }
            _ = StartDeathLinkLoopAsync(_cts4dl.Token, Client);
        }

        public static void StopDeathLinkLoop()
        {
            _cts4dl?.Cancel();
            _cts4dl = null;
            deathlink_enabled = false;
        }

        public static async Task StartDeathLinkLoopAsync(CancellationToken token, ArchipelagoClient Client)
        {
            try
            {
                while (!token.IsCancellationRequested)
                {
                    await EvaluateDeathLinkAsync(Client);

                    await Task.Delay(TimeSpan.FromSeconds(1), token);
                }
            }
            catch (TaskCanceledException)
            {
                // Expected when stopping — no action needed
            }
        }

        public static async Task EvaluateDeathLinkAsync(ArchipelagoClient Client)
        {
            if (DeathLinkWasReceived == true)
            {   return;}
            if (Memory.ReadBit(Addresses.IgnoreInput, 0)==true)
            {
                NumberofBails++;
                DebugWriter.LogDeathlinkDebug("Bail counted. You are at "+ NumberofBails.ToString() + " out of " + NumberofBailsNeeded.ToString() + " bails needed to send a deathlink");
                if (NumberofBails >= NumberofBailsNeeded)
                {
                    DebugWriter.LogDeathlinkDebug("Deathlink sending.");
                    NumberofBails = 0;
                    await SendTHAWDeathLink(Client);
                }
                Log.Logger.Warning("You are at "+ NumberofBails.ToString() + " out of " + NumberofBailsNeeded.ToString() + " bails needed to send a deathlink");
                await Task.Delay(TimeSpan.FromSeconds(5));
            }

        }
        public static async Task SendTHAWDeathLink(ArchipelagoClient Client)
        {
            Log.Logger.Warning("Sending Deathlink");
            Random random = new Random();

            int randomNumber = Random.Shared.Next(1, 9);
            switch (randomNumber)
            {
                case 1:
                    _deathLinkService.SendDeathLink(new DeathLink(Client.CurrentSession.Players.ActivePlayer.Name, Client.CurrentSession.Players.ActivePlayer.Name.ToString() + " broke his leg in 12 places, including California."));
                    break;
                case 2:
                    _deathLinkService.SendDeathLink(new DeathLink(Client.CurrentSession.Players.ActivePlayer.Name, Client.CurrentSession.Players.ActivePlayer.Name.ToString() + " got a free curbside nosejob."));
                    break;
                case 3:
                    _deathLinkService.SendDeathLink(new DeathLink(Client.CurrentSession.Players.ActivePlayer.Name, Client.CurrentSession.Players.ActivePlayer.Name.ToString() + " got 12 stitches with some free medical debt."));
                    break;
                case 4:
                    _deathLinkService.SendDeathLink(new DeathLink(Client.CurrentSession.Players.ActivePlayer.Name, Client.CurrentSession.Players.ActivePlayer.Name.ToString() + " did the 870 and broke their collarbone."));
                    break;
                case 5:
                    _deathLinkService.SendDeathLink(new DeathLink(Client.CurrentSession.Players.ActivePlayer.Name, Client.CurrentSession.Players.ActivePlayer.Name.ToString() + " landed on their head."));
                    break;
                case 6:
                    _deathLinkService.SendDeathLink(new DeathLink(Client.CurrentSession.Players.ActivePlayer.Name, Client.CurrentSession.Players.ActivePlayer.Name.ToString() + " inspected the pavement a little too closely."));
                    break;
                case 7:
                    _deathLinkService.SendDeathLink(new DeathLink(Client.CurrentSession.Players.ActivePlayer.Name, Client.CurrentSession.Players.ActivePlayer.Name.ToString() + " tested gravity. Gravity won."));
                    break;
                case 8:
                    _deathLinkService.SendDeathLink(new DeathLink(Client.CurrentSession.Players.ActivePlayer.Name, Client.CurrentSession.Players.ActivePlayer.Name.ToString() + " is hoping Tony Hawk will sign their cast."));
                    break;
            }

        }
    }
}