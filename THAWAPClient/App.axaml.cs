using Archipelago.Core;
using Archipelago.Core.AvaloniaGUI.Models;
using Archipelago.Core.AvaloniaGUI.ViewModels;
using Archipelago.Core.AvaloniaGUI.Views;
using Archipelago.Core.Helpers;
using Archipelago.Core.Models;
using Archipelago.Core.Util;
using Archipelago.Core.Util.Overlay;
using Archipelago.MultiClient.Net.MessageLog.Messages;
using Archipelago.MultiClient.Net.Helpers;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Color = Avalonia.Media.Color;
using THAWAPClient.Models;
using THAWAPClient.ViewModels;
using Newtonsoft.Json;
using ReactiveUI;
using Serilog;
using System.Text.RegularExpressions;
using System.Reactive.Concurrency;

using THAWAPClient.Helpers;
using Silk.NET.Core;
using System.Collections;
using System.Net.NetworkInformation;

namespace THAWAPClient;

public partial class App : Application
{
    static MainWindowViewModel Context;
    public static THAWControlsWindowModel ControlsContext;
    private THAWControlsWindow thawControlsWindow;
    static DateTime lastItemReceived = DateTime.MinValue;
    static uint batchItemsReceived = 0;
    public static ArchipelagoClient Client { get; set; }
    private bool overlayInitialized = false;
    private static readonly object _lockObject = new object();
    public static int Goal { get; set; }
    public static TonyHawkOptions THAWOptions;
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
        Context = new MainWindowViewModel() { ConnectButtonEnabled = true };
        Context.ConnectClicked += Context_ConnectClicked;
        Context.CommandReceived += (_, a) => Client?.SendMessage(a.Command);
        Context.CommandReceived += Context_CommandReceived;
        Context.OverlayEnabled = true;
        Context.AutoscrollEnabled = true;

        ControlsContext = new THAWControlsWindowModel();
        thawControlsWindow = new THAWControlsWindow()
        {
            DataContext = ControlsContext
        };
        //dsrControlsWindow.InitializeComponent();
        Context.CustomControlsWindow = thawControlsWindow;
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = Context
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainWindow
            {
                DataContext = Context
            };
        }
        base.OnFrameworkInitializationCompleted();
    }

    private void UnsubscribeClientEvents()
    {
        if (Client == null) return;

        Client.Connected -= OnConnectedAsync;
        Client.Disconnected -= OnDisconnected;
        Client.ItemManager.ItemReceived -= Client_ItemReceived;
        Client.MessageReceived -= Client_MessageReceived;
        Client.LocationManager.LocationCompleted -= Client_LocationCompleted;
        Client.LocationManager.CancelMonitors();
    }
    private bool TryConnectToGame()
    {
        var client = new GameClient("pcsx2-qt");
        if (!client.Connect())
        {
            Log.Logger.Error("PCSX2 not running, open PCSX2 and load your game before connecting!");
            return false;
        }

        Client = new ArchipelagoClient(client);
        Memory.GlobalOffset = Memory.GetPCSX2Offset();
        return true;
    }
    private async Task ConnectToArchipelago(ConnectClickedEventArgs e)
    {
        if (Client == null) return;

        Client.Connected += OnConnectedAsync;
        Client.Disconnected += OnDisconnected;
        Client.MessageReceived += Client_MessageReceived;

        await Client.Connect(e.Host, "Tony Hawk's American Wasteland");

        await Client.Login(e.Slot, string.IsNullOrWhiteSpace(e.Password) ? null : e.Password);
        Client.ItemManager!.Initialize();
        Client.ItemManager.ItemReceived += Client_ItemReceived;
        await Client.ItemManager.ReceiveReady(Client.CurrentSession);
        Client.LocationManager.LocationCompleted += Client_LocationCompleted;

        UpdatePlayer(Client);
        PlayerState.StartFixLoop();
        if (THAWOptions.TrickCashing == true)
            {TrickCashing.StartTrickCashLoop();}
        // if (THAWOptions.DeathlinkSettings>=(uint)TonyHawkOptions.DeathlinkChoice.option_1_bail)
        //     {Deathlinking.StartDeathLinkLoop(Client, THAWOptions);}
        if (THAWOptions.ProgressiveWallet==true)
            {WalletManaging.StartWalletLoop();}
        if (THAWOptions.SkateboardIncludedInItemPool==true)
            {SkateboardItem.StartSkateboardLoop();}
        await SetupLocationMonitoring(Client);
    }
    private async void Context_ConnectClicked(object? sender, ConnectClickedEventArgs e)
    {
        Context.ConnectButtonEnabled = false;
        Log.Logger.Information("Connecting...");
        UnsubscribeClientEvents();

        if (!TryConnectToGame())
        {
            Context.ConnectButtonEnabled = true;
            return;
        }

        //if (!ValidateGameVersion())
        //    return;

        await ConnectToArchipelago(e);

        if (!overlayInitialized) // only init overlay if it hasn't already been initialized (initing it twice causes a crash)
            {
                overlayInitialized = true;
                if (Context.OverlayEnabled)
                {
                    Client.IntializeOverlayService(new WindowsOverlayService(new OverlayOptions()
                    {
                        YOffset = 250 // later, set this dynamically based on "UI scale" option
                    }));
                }
                else // otherwise set a task to poll it until it's enabled.
                {
                    Task.Run(async () =>
                    {
                        while (true)
                        {
                            await Task.Delay(2000);
                            if (Context.OverlayEnabled)
                            {
                                Client.IntializeOverlayService(new WindowsOverlayService(new OverlayOptions()
                                {
                                    YOffset = 250 // later, set this dynamically based on "UI scale" DSR option
                                }));
                                Log.Logger.Information("Overlay Enabled.");
                                Client.AddOverlayMessage("Overlay Enabled.");
                                break;
                            }
                        }
                    });
                }
            }

        Context.ConnectButtonEnabled = true;
    }

    private async Task SetupLocationMonitoring(ArchipelagoClient Client)
    {
        if (Client == null) return;
 
        var locations = GetLocations();
        Client.LocationManager.MonitorLocationsAsync(Client.CurrentSession, locations);

        // MissionHandling.StartMissionFinderLoop(Client);
        StartAltMissionTracker();
        GapLocationReading.StartGapLocationInitializationLoop(Client, THAWOptions);
    }

    private static List<ILocation> GetLocations()
    {  List<ILocation> listofalllocations = new List<ILocation>();
       listofalllocations.AddRange(ShopLocationReading.GetAllShopLocations(THAWOptions));
       listofalllocations.AddRange(MiscLocationReading.GetMiscLocations(THAWOptions));
       return listofalllocations;
    }
    
    private static void Client_ItemReceived(object? sender, ItemReceivedEventArgs e)
    {
        LogItem(e.Item);
        bool success = false;
        DateTime dtnow = DateTime.UtcNow;
        if (Helpers.LevelID.IsInGame())
        {
            bool will_popup = WillPopupReceive(e.Player, e.Item);
            // For items that will give popups, limit how often they send
            if (will_popup)
            {
                if (lastItemReceived > dtnow.AddMilliseconds(-250))
                {
                    batchItemsReceived++;
                    if (batchItemsReceived > 3)
                    {
                        Task.Delay((lastItemReceived.AddMilliseconds(250) - dtnow).Milliseconds).Wait();
                        batchItemsReceived = 0;
                        lastItemReceived = dtnow;
                    }
                }
                else
                {
                    batchItemsReceived = 0;
                    lastItemReceived = dtnow;
                }
            }
            switch (e.Item.Name)
            {
                case "5 Bucks":
                    CashHelper.AddCash(5);
                    success = true;
                    break;
                case "10 Bucks":
                    CashHelper.AddCash(10);
                    success = true;
                    break;
                case "40 Bucks":
                    CashHelper.AddCash(40);
                    success = true;
                    break;
                case "100 Bucks":
                    CashHelper.AddCash(100);
                    success = true;
                    break;
                case "200 Bucks":
                    CashHelper.AddCash(200);
                    success = true;
                    break;
                case "500 Bucks":
                    CashHelper.AddCash(500);
                    success = true;
                    break;
                case "Progressive Air Stat":
                    PlayerState.StatAir = FindCount.ArchiCountingFloat(Client, "Progressive Air Stat");
                    success = true;
                    break;
                case "Progressive Run Stat":
                    PlayerState.StatRun = FindCount.ArchiCountingFloat(Client, "Progressive Run Stat");
                    success = true;
                    break;
                case "Progressive Ollie Stat":
                    PlayerState.StatOllie = FindCount.ArchiCountingFloat(Client, "Progressive Ollie Stat");
                    success = true;
                    break;
                case "Progressive Speed Stat":
                    PlayerState.StatSpeed = FindCount.ArchiCountingFloat(Client, "Progressive Speed Stat");
                    success = true;
                    break;
                case "Progressive Flip Stat":
                    PlayerState.StatFlip = FindCount.ArchiCountingFloat(Client, "Progressive Flip Stat");
                    success = true;
                    break;
                 case "Progressive Spin Stat":
                    PlayerState.StatSpin = FindCount.ArchiCountingFloat(Client, "Progressive Spin Stat");
                    success = true;
                    break;
                case "Progressive Switch Stat":
                    PlayerState.StatSwitch = FindCount.ArchiCountingFloat(Client, "Progressive Switch Stat");
                    success = true;
                    break;
                case "Progressive Rail Stat":
                    PlayerState.StatRail = FindCount.ArchiCountingFloat(Client, "Progressive Rail Stat");
                    success = true;
                    break;
                case "Progressive Lip Stat":
                    PlayerState.StatLip = FindCount.ArchiCountingFloat(Client, "Progressive Lip Stat");
                    success = true;
                    break;
                case "Progressive Manual Stat":
                    PlayerState.StatManual = FindCount.ArchiCountingFloat(Client, "Progressive Manual Stat");
                    success = true;
                    break;
                case "Skate Ability: Manual":
                    PlayerState.HasManual = 1;
                    success = true;
                    break;
                case "Skate Ability: Revert":
                    PlayerState.HasRevert = 1;
                    success = true;
                    break;
                case "Skate Ability: Spine Transfer/Acid Drop/Bank Drop":
                    PlayerState.HasSpineTransfer = 1;
                    success = true;
                    break;
                case "Skate Ability: Wall Ride":
                    PlayerState.HasWallRide = 1;
                    success = true;
                    break;
                case "Skate Ability: Sticker Slap/Wall Plant/Vert Wall Plant":
                    PlayerState.HasStickerSlap = 1;
                    success = true;
                    break;
                case "Skate Ability: Flatland":
                    PlayerState.HasFlatland = 1;
                    success = true;
                    break;
                case "Skate Ability: Natas Spin":
                    PlayerState.HasNatasSpin = 1;
                    success = true;
                    break;
                case "Skate Ability: Boneless":
                    PlayerState.HasBoneless = 1;
                    success = true;
                    break;
                case "Skate Ability: Special":
                    PlayerState.HasSpecial = 1;
                    success = true;
                    break;
                case "Skate Ability: Focus":
                    PlayerState.HasFocus = 1;
                    success = true;
                    break;
                case "Skate Ability: Flips/Rolls":
                    PlayerState.HasFlips = 1;
                    success = true;
                    break;
                case "Skate Ability: Stall":
                    PlayerState.HasStall = 1;
                    success = true;
                    break;
                case "Skate Ability: Skitch":
                    PlayerState.HasSkitch = 1;
                    success = true;
                    break;
                case "Skate Ability: Caveman":
                    PlayerState.HasCaveman = 1;
                    success = true;
                    break;
                case "Skate Ability: Wall Run":
                    PlayerState.HasWallRun = 1;
                    success = true;
                    break;
                case "Skate Ability: Shimmy":
                    PlayerState.HasShimmy = 1;
                    success = true;
                    break;
                case "Skate Ability: Wall Flip":
                    PlayerState.HasWallFlip = 1;
                    success = true;
                    break;
                case "Skate Ability: Bert Slide":
                    PlayerState.HasBertSlide = 1;
                    success = true;
                    break;
                case "Skate Ability: Back Tuck/Front Tuck":
                    PlayerState.HasTuck = 1;
                    success = true;
                    break;
                case "Skate Ability: Boned Ollie":
                    PlayerState.HasBonedOllie = 1;
                    success = true;
                    break;
                case "Bus Access: Beverly Hills":
                    Memory.WriteBit(Addresses.BeverlyHills, 6, true);
                    success = true;
                    break;
                case "Bus Access: Downtown":
                    Memory.WriteBit(Addresses.Downtown, 6, true);
                    success = true;
                    break;
                case "Progressive Wallet":
                    WalletManaging.UpdateWallets(Client);
                    success = true;
                    break;
                case "Skateboard Unlock":
                    SkateboardItem.HasSkateboard=true;
                    success = true;
                    break;
            }

            Log.Logger.Information($"Received {e.Item.Name} ({e.Item.Id})");
            Client.AddOverlayMessage($"Received {e.Item.Name} ({e.Item.Id})");
            e.Success = success;

            /* If receive didn't work, schedule re-receive for when we are back in game */
            if (!success)
            {
                Log.Logger.Warning($"Failed to receive item - Player not loaded into game. Will retry when player is once again in game.");
                Client.AddOverlayMessage($"Failed to receive item - Player not loaded into game. Will retry when player is once again in game.");
                
                Task.Run(async () =>
                {
                    /* Check every second if player is in game again yet */
                    while(!LevelID.IsInGame())
                    {
                        await Task.Delay(1000);
                    }

                    Log.Logger.Warning($"Player once again detected as in game. Re-trying item receive.");
                    Client.AddOverlayMessage($"Player once again detected as in game. Re-trying item receive.");
                    /* Once finally in game, re-enable receives. */
                    Client.ReceiveReady();
                });
            }
        }
    }

    private static bool WillPopupReceive(PlayerInfo player, Item item)
    {
        bool willsend = false;
        if (player.Slot == Client.CurrentSession.ConnectionInfo.Slot) // from and to ourself
        {
            if (item.IsProgression && ControlsContext.FoundItemProgressive)
                willsend = true;
            else if (item.IsUseful && ControlsContext.FoundItemUseful)
                willsend = true;
            else if (item.IsTrap && ControlsContext.FoundItemTrap)
                willsend = true;
            else if (ControlsContext.FoundItemFiller && item.flags == 0)
                willsend = true;
        }
        else // from someone else
        {
            if (item.IsProgression && ControlsContext.ReceivedItemProgressive)
                willsend = true;
            else if (item.IsUseful && ControlsContext.ReceivedItemUseful)
                willsend = true;
            else if (item.IsTrap && ControlsContext.ReceivedItemTrap)
                willsend = true;
            else if (ControlsContext.ReceivedItemFiller && item.flags == 0)
                willsend = true;
        }
        return willsend;
    }

    private static void Client_LocationCompleted(object? sender, Archipelago.Core.Models.LocationCompletedEventArgs e)
    {   
        DebugWriter.LogLocationDebug("Completed Location, " + e.CompletedLocation.Name);
        if (THAWOptions.ChosenGoal == (uint)TonyHawkOptions.EndGoal.option_smash_the_t_rex)
        {
                var locid = e.CompletedLocation.Id;
            if (e.CompletedLocation.Name.Contains("HW Mission: Get Into Beverly Hills"))
            {
                Log.Logger.Information($"Sending Goal for location: Smash the T-Rex");
                GoalTracking.SendGoal(Client);
            }
            else if (locid == 10100008) // get into BH location
            {
            Log.Logger.Information($"Sending Goal for location: Smash the T-Rex");
                GoalTracking.SendGoal(Client);
            }
            else
            {}
        }
        else if (THAWOptions.ChosenGoal == (uint)TonyHawkOptions.EndGoal.option_get_to_the_skate_ranch)
        {
                var locid = e.CompletedLocation.Id;
            if (e.CompletedLocation.Name.Contains("Visit the Skate Ranch"))
            {
                Log.Logger.Information($"Sending Goal for location: Get to the Skate Ranch");
                GoalTracking.SendGoal(Client);
            }
            else if (locid == 20000002) // get into skate ranch location
            {
                Log.Logger.Information($"Sending Goal for location: Get to the Skate Ranch");
                GoalTracking.SendGoal(Client);
            }
        }
        else if (THAWOptions.ChosenGoal == (uint)TonyHawkOptions.EndGoal.option_win_the_skate_competition)
        {
                var locid = e.CompletedLocation.Id;
            if (e.CompletedLocation.Name.Contains("VP Mission: Win the Tony Hawk AMJAM"))
            {
                Log.Logger.Information($"Sending Goal for location: Win the Skate Competition");
                GoalTracking.SendGoal(Client);
            }
            else if (locid == 50100003) // win amjam location
            {
                Log.Logger.Information($"Sending Goal for location: Win the Skate Competition");
                GoalTracking.SendGoal(Client);
            }
        }

    }

    public async void Context_CommandReceived(object? sender, ArchipelagoCommandEventArgs a)
    {
        if (string.IsNullOrWhiteSpace(a.Command)) return;

        string command = a.Command.Trim().ToLower();
        Log.Logger.Debug($"command received: {command}");
        if (command.StartsWith("/help"))
        {
            Log.Logger.Warning("--- THAWAP commands: --- ");
            Log.Logger.Warning(" /help - Display this menu.");
            Log.Logger.Warning(" /currentstats - Prints out your current stats given by archipelago.");
            Log.Logger.Warning(" /unstuck - Teleports you to a certain location in the current level to unstick you.");
            Log.Logger.Warning(" /emergencygoalsend - Completes your slot and sends your goal for emergency purposes.");
            Log.Logger.Warning(" /goal - Says what your goal is.");
            Log.Logger.Warning(" /checkgoal - Checks the goal completion and sends it if it finds its complete.");
            Log.Logger.Warning(" /deathlink - Toggles deathlink.");
            Log.Logger.Warning(" /deathlinksettingX - Sets the number in place of X as the number of bails needed to trigger sending a deathlink.");
            Log.Logger.Warning("--- End of THAWAP commands. ---");
            Client?.SendMessage(a.Command); /* send original command through client for the rest of /help - maybe player will have something if they are an admin. */
        }
        else if (command.StartsWith("/currentstats"))
        {
            PlayerState.PrintCurrentStats(Client);
        }
        else if (command.StartsWith("/unstuck"))
        {
            Unstuck.TeleportMe();
        }
        else if (command.StartsWith("/emergencygoalsend"))
        {
            GoalTracking.SendGoal(Client);
        }
        else if (command.StartsWith("/goal"))
        {
            switch (THAWOptions.ChosenGoal)
            {
            case (uint)TonyHawkOptions.EndGoal.option_smash_the_t_rex:
            {Log.Logger.Warning("Your current goal is Smash the T-rex");}
            break;
            case (uint)TonyHawkOptions.EndGoal.option_get_to_the_skate_ranch:
            {Log.Logger.Warning("Your current goal is Get to the Skate Ranch");}
            break;
            case (uint)TonyHawkOptions.EndGoal.option_win_the_skate_competition:
            {Log.Logger.Warning("Your current goal is Win the Skate Competition.");}
            break;
            }
        }
        else if (command.StartsWith("/checkgoal"))
        {   switch (THAWOptions.ChosenGoal)
                {case (uint)TonyHawkOptions.EndGoal.option_smash_the_t_rex:   
                    {
                        if (Memory.ReadBit(Addresses.BeverlyHills,5))
                        {GoalTracking.SendGoal(Client);}
                    }
                    break;
                case (uint)TonyHawkOptions.EndGoal.option_get_to_the_skate_ranch:
                    {
                        if (Memory.ReadBit(Addresses.SkateRanch,5))
                        {GoalTracking.SendGoal(Client);}
                    }
                    break;
                case (uint)TonyHawkOptions.EndGoal.option_win_the_skate_competition:
                    {
                        if (Memory.ReadBit(Addresses.VansPark,5))
                        {GoalTracking.SendGoal(Client);}
                    }
                    break;
                }
        }
        else if (command == "/deathlink")
        {
            await Deathlinking.ToggleDeathlink(Client, THAWOptions);
        }
        else if (command.StartsWith("/deathlinksetting"))
        {   string prefix = "/deathlinksetting";
            string numberText = command[prefix.Length..];

            if (int.TryParse(numberText, out int number))
            {
                if (number >= 1)
                {Deathlinking.NumberofBailsNeeded = number;}
            }
        } 
    }

    private void Client_MessageReceived(object? sender, MessageReceivedEventArgs e)
    {
        if (e.Message.Parts.Any(x => x.Text == "[Hint]: "))
        {
            LogHint(e.Message);
        }
        Log.Logger.Information(JsonConvert.SerializeObject(e.Message));
    }

    private static void LogItem(Item item)
    {
        lock (_lockObject)
        {
            RxApp.MainThreadScheduler.Schedule(() =>
            {
                var messageToLog = new LogListItem(new List<TextSpan>()
                {
                    new TextSpan(){Text = $"[{item.Id.ToString()}] -", TextColor = new SolidColorBrush(Color.FromRgb(255, 255, 255))},
                    new TextSpan(){Text = $"{item.Name}", TextColor = new SolidColorBrush(Color.FromRgb(200, 255, 200))}
                });
                Context.ItemList.Add(messageToLog);
            });
        }
    }

    private static void LogHint(LogMessage message)
    {
        var newMessage = message.Parts.Select(x => x.Text);
        lock (_lockObject)
        {
            RxApp.MainThreadScheduler.Schedule(() =>
            {
                if (Context.HintList.Any(x => x.TextSpans.Select(y => y.Text) == newMessage))
                {
                    return; //Hint already in list
                }
                List<TextSpan> spans = new List<TextSpan>();
                foreach (var part in message.Parts)
                {
                    spans.Add(new TextSpan() { Text = part.Text, TextColor = new SolidColorBrush(Color.FromRgb(part.Color.R, part.Color.G, part.Color.B)) });
                }

                Context.HintList.Add(new LogListItem(spans));
            });
        }
    }

    private static void OnConnectedAsync(object sender, EventArgs args)
    {
        Log.Logger.Information("Connected to Archipelago");
        Log.Logger.Information($"Playing {Client.CurrentSession.ConnectionInfo.Game} as {Client.CurrentSession.Players.GetPlayerName(Client.CurrentSession.ConnectionInfo.Slot)}");
        int currentSlot = App.Client.CurrentSession.ConnectionInfo.Slot;
        var slotDataTask = App.Client.CurrentSession.DataStorage.GetSlotDataAsync(currentSlot);
        slotDataTask.Wait();
        Dictionary<string, object> slotData = slotDataTask.Result;

        THAWOptions = new TonyHawkOptions(App.Client.Options, slotData);
        uint currentgoal = THAWOptions.ChosenGoal;
        switch (currentgoal)
        {
        case (uint)TonyHawkOptions.EndGoal.option_smash_the_t_rex:
           {Log.Logger.Warning("Your current goal is Smash the T-rex");}
           break;
        case (uint)TonyHawkOptions.EndGoal.option_get_to_the_skate_ranch:
           {Log.Logger.Warning("Your current goal is Get to the Skate Ranch");}
           break;
        case (uint)TonyHawkOptions.EndGoal.option_win_the_skate_competition:
           {Log.Logger.Warning("Your current goal is Win the Skate Competition.");}
           break;
        }
        if (THAWOptions.TrickCashing == true)
            {{Log.Logger.Warning("Tricks 4 Cash is turned on.");}}
        else if (THAWOptions.TrickCashing == false)
            {{Log.Logger.Warning("Tricks 4 Cash is turned off.");}}
    }

    private static void OnDisconnected(object sender, EventArgs args)
    {
        Log.Logger.Information("Disconnected from Archipelago");
    }

    public static void UpdatePlayer(ArchipelagoClient Client)
    {
        PlayerState.UpdateSkater(Client);
        BusAccess.UpdateBusAccess(Client);
        if (THAWOptions.ProgressiveWallet==true)
            {WalletManaging.UpdateWallets(Client);}
        if (THAWOptions.SkateboardIncludedInItemPool==true)
            {SkateboardItem.UpdateSkateboard(Client);}
    }

    //private static bool ValidateGameVersion()
    //{
       //var palAddress = Memory.ReadInt(0x003694D0);
        //var usAddress = Memory.ReadInt(0x00364BD0);
        //var gameVersion = palAddress == 1701667175 ? "PAL" : usAddress == 1701667175 ? "US" : "";
        //if (string.IsNullOrWhiteSpace(gameVersion))
        //{
            //Log.Logger.Information("Dark cloud 2 is not loaded, please load the game and try again.");
            //Context.ConnectButtonEnabled = true;
            //return false;
        //}
        //else if (gameVersion == "PAL")
        //{
            //Log.Logger.Information("You have loaded Dark Chronicle (PAL). Only Dark Cloud 2 (US) is supported");
            //Context.ConnectButtonEnabled = true;
            //return false;
        //}

        //return true;
    //}

    public void StartAltMissionTracker()
    {
        MissionReader MissionReaderObject = new MissionReader
            {
                _Client = Client
            };
            Thread MissionReaderThread = new Thread(MissionReaderObject.MissionScan);

            // Start the worker thread.
            MissionReaderThread.Start();
            DebugWriter.LogMissionDebug("Starting Alt Mission Tracker");
    }
}