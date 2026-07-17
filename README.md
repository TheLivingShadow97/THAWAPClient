# THAWAPClient
Archipelago Client for Tony Hawk's American Wasteland (USA) (Collector's Edition)

## **This implementation is still a work in progress. Expect bugs and issues.**

#### Table of Contents
[Required Software](#Required-Software)  
[Setup](#Setup)   
[Roadmap](#Roadmap)  
[Special Thanks](#Special-Thanks)  
[Changelog](#Changelog)  

# Required Software
* A current stable version of PCSX2. It may not work with very old versions. (I tested and built the program using 2.6.2, for example)
* A rom for Tony Hawk's American Wasteland (USA) (Collector's Edition). **IT MUST BE THIS SPECIFIC VERSION.**
* A current APworld (and optionally a Yaml file to skip a step) which you can find [here](https://github.com/TheLivingShadow97/THAWAP/releases).
* An up-to-date version of the [THAWAPClient, which you can download here](https://github.com/TheLivingShadow97/THAWAPClient/releases).
* [Installed Archipelago Launcher, found here.](https://github.com/ArchipelagoMW/Archipelago/releases/latest)

* Optional, but recommended: [Universal Tracker](https://github.com/FarisTheAncient/Archipelago/releases?q=Tracker), to see what checks you can still send, or what you may need to progress.

# Setup
1. Download the latest APWorld (`thaw.apworld`) and THAWAP Desktop Client zip (`THAWAPClient.zip`) from the pages linked above.
2. Install the Archipelago Launcher, if you haven't already.
3. Double click on the APWorld to install it to your Archipelago installation.
4. Extract the THAWAP Desktop Client zip to a folder of your choosing.

### Creating your Options File (yaml)
1. Install the latest Archipelago Launcher version from the page linked above.
2. Run the Archipelago Launcher and open "Options Creator" (Archipelago v0.6.5+ only). In Options Creator:
    * Select "Tony Hawk's American Wasteland", type in a player name for your slot, and edit the options to your liking.
    * Export to your Archipelago "Players/" folder (not the Templates subfolder!). This will put a yaml with your player name in that folder.

### Generating a World
1. Install the latest Archipelago Launcher version from the page linked above.
2. Place all the .yaml files you wish to generate with in the Players/ folder of your Archipelago Launcher installation.
3. Press the Generate button on the launcher to generate a seed/multiworld.
4. When this has completed, you will have a zip file in your Archipelago/Output folder.
5. Host the output zip by uploading it to Archipelago.gg

### Running and Connecting the Game

To connect Tony Hawk's American Wasteland (USA) (Collector's Edition) to Archipelago:

1. Start PCSX2 as well as your ROM and make sure you have no save files for THAW. You need a fresh save to keep from mistakenly sending checks from earlier progress. 

2. Start your new story save file, pick your default skater, and **wait up until you exit the bus and you're finally in-game** ready to change your hair, then pause the game.

3. Connect the Client:
   Run `THAWAPClient.exe` that you extracted earlier from the THAWAP Desktop Client zip.  
   
   3a. Click the top-left three-horizontal-line ("hamburger menu") icon, and fill in your connection details: host, slot, and password (if required).

   3b. With THAW running **and you in-game, turn the overlay toggle to off** (it will crash otherwise) and press the "Connect" button in the THAWAP client. You should see the client log start to populate, ~~and if you have the overlay option on, messages appear over your game window.~~  
      It will most likely immedately send a few locations out upon connecting, since it thinks you already "bought" whatever items your chosen default skater is wearing.

4. Start playing as normal. You must keep the THAWAP client running while you play the game for items and location to be sent and received correctly. Please Enjoy! :)

# Roadmap
* Adding of each region and associated gaps and missions, with a few separate goals like "Win the Skate Competion" and "Get to the Skate Ranch" to choose from
* Fix the overlay
* Bus Access, BW Tattoo, and BMX Tri-key Items
* Traps
* Skate Tricks Items(ex. The 900)
* Deathlink(?)
* ~~BMX Missions~~ Not unless you really want them lol. 

# Special Thanks
Special thanks to: 
* Timenoe from RetroAchievements, for having already done much of the necessary ramdigging, which I could read on the RA code notes page for the game.
* ArsonAssassin, for their awesome coding library which does most of the work, and the example of their code using it.
* Kass(andra), for their help in DMs and the example of their code using the library.
* Uroogla, for the example of their code using the library, and good advice on where to start learning.

# Changelog
## Version 0.0.1
* Initial release containing only early Hollywood checks and a "Smash the T-rex" goal. 
* Overlay does not work at present, but the rest of the app seems to work fine.
* Locations include all buyables, gaps, tagging side missions and main missions until you smash the t-rex before entering Beverly Hills.
* Items include all 10 progressive stats from 1-10, all unlockable skating abilities like manual and revert for example, and cash.
