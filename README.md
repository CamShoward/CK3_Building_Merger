# CK3 Building Merger
## Overview
CK3 Building Merger is a Windows Forms application designed to help users merge building files from multiple Crusader Kings 3 (CK3) mods. This tool allows users to browse and select a JSON playset file, a Steam Workshop folder, and a CK3 mod folder, and then generates a consolidated holdings file for the selected mods.

## Features
Select JSON playset file.
Browse and select Steam Workshop and CK3 mod folders.
Generate consolidated holdings files for castle, city, tribal, and church holdings.
Automatically create a new folder for the merged files based on the playset name.
Error detection to check the validity of the provided file paths.
### Prerequisites
.NET Framework 4.7.2 or later
Newtonsoft.Json library for JSON handling
### Installation
Clone the repository:
sh
Copy code
git clone https://github.com/yourusername/CK3_Building_Merger.git
Open the solution file CK3_Building_Merger.sln in Visual Studio.
Restore the NuGet packages to ensure all dependencies are installed.
Build the solution.
## Usage
Select Playset File:
Click on the Browse button and select a JSON playset file.
Select Steam Workshop Folder:
Click on the Select Steam Workshop Folder button and navigate to your Steam Workshop folder.
Select CK3 Mod Folder:
Click on the Select CK3 Mod Folder button and navigate to your CK3 mod folder.
Submit:
Click on the Submit button to generate the consolidated holdings files.
Clear:
Click on the Clear button to clear all selected paths.
Error Handling
The application includes error detection to ensure the provided file paths exist. If an invalid path is detected, a message box will display the error, and the process will halt until the issue is resolved.

# Example JSON Playset
Ensure your JSON playset file matches the expected structure:

## JSON
```
 {"game":"ck3","name":"Clone of 7/10","mods":[{"displayName":"Elf Destiny: Elven Superiority 2.0","enabled":true,"position":0,"steamId":"3213123230"},{"displayName":"Spartan Flavor - Standalone","enabled":true,"position":1,"steamId":"3220530566"},{"displayName":"ARY NEW Traditions","enabled":true,"position":2,"steamId":"3241130652"},{"displayName":"2024新版可用大肖像","enabled":true,"position":3,"steamId":"3235414892"},{"displayName":"Always Move Realm Capital","enabled":false,"position":4,"steamId":"2259210465"},{"displayName":"Arcane Legacy 2","enabled":false,"position":5,"steamId":"3132210253"},{"displayName":"Battle Graphics","enabled":true,"position":6,"steamId":"3225355262"},{"displayName":"Better Battle Legitimacy 0.15","enabled":true,"position":7,"steamId":"3174538591"},{"displayName":"Better Crusader Kings - (Councillor Lifestyle)","enabled":true,"position":8,"steamId":"3179749338"},{"displayName":"BetterRenown","enabled":true,"position":9,"steamId":"2310894427"},{"displayName":"Big Battle View","enabled":true,"position":10,"steamId":"3030427202"},{"displayName":"Character beautification（人物美化）","enabled":true,"position":11,"steamId":"2222302033"},{"displayName":"Character UI Overhaul","enabled":true,"position":12,"steamId":"2519175282"},{"displayName":"Choose Legend Special Building Location","enabled":true,"position":13,"steamId":"3223291850"},{"displayName":"Commanders+","enabled":true,"position":14,"steamId":"3191893683"},{"displayName":"Designate Heir - freely","enabled":true,"position":15,"steamId":"2869221069"},{"displayName":"Dynamic Lifestyle XP Redux","enabled":true,"position":16,"steamId":"2985350156"},{"displayName":"Elf Destiny","enabled":true,"position":17,"steamId":"3114064450"},{"displayName":"Graduate Lifestyle","enabled":true,"position":18,"steamId":"2583567726"},{"displayName":"Hiraeth - Dynasty Legacies Overhaul","enabled":true,"position":19,"steamId":"2697392271"},{"displayName":"Immersive Negative Trait Removal","enabled":true,"position":20,"steamId":"3173823584"},{"displayName":"Modify Vassal Contract","enabled":true,"position":21,"steamId":"2779619112"},{"displayName":"More Buildings Reboot","enabled":true,"position":22,"steamId":"2943715129"},{"displayName":"More Interactive Vassals","enabled":true,"position":23,"steamId":"2712590542"},{"displayName":"More Lifestyles","enabled":true,"position":24,"steamId":"3013259695"},{"displayName":"More Single Combats","enabled":true,"position":25,"steamId":"2510732954"},{"displayName":"More Traditions v2","enabled":true,"position":26,"steamId":"2893793966"},{"displayName":"No Tournament Animated Screens","enabled":true,"position":27,"steamId":"3109614871"},{"displayName":"Reduced Delay Between Cultural Reform","enabled":true,"position":28,"steamId":"2784700508"},{"displayName":"No Holy Sites Needed","enabled":true,"position":29,"steamId":"2831192883"},{"displayName":"Sappho's Daughter II","enabled":false,"position":30,"steamId":"2863937956"},{"displayName":"Numenywhere","enabled":true,"position":31,"steamId":"3269656307"},{"displayName":"Dragon World","enabled":true,"position":32,"steamId":"2744166597"},{"displayName":"Medieval Matriarchs","enabled":true,"position":33,"steamId":"2312667949"},{"displayName":"Nicelander's Blood Mage","enabled":true,"position":34,"steamId":"2737082376"},{"displayName":"No More Levies - No MAA, Knights, and Accolades update","enabled":true,"position":35,"steamId":"2810220558"},{"displayName":"Visible Disfigurement - No More Masks","enabled":true,"position":36,"steamId":"3245958435"},{"displayName":"Newer Better Buyable Building Slot + Infinite Building Slot","enabled":true,"position":37,"steamId":"2891294108"}]}
```
License
This project is licensed under the MIT License. See the LICENSE file for details.
