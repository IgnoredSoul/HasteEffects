# Haste Effects v1.0.0 (Public release)
Simple mod for [Haste](https://landfall.se/hastebrokenworlds) developed by [Landfall](https://landfall.se/) using [MelonLoader](https://melonwiki.xyz/).<>
Selecting random stats and modifing them to create unique challenges or easy as fuck levels.

All min / max values can be modified in `/UserData/StatsRandomizer.cfg` !

# Notes
I think there is some conflics with the game and how I manage the multipliers, like items changing the multipliers when in a run instead of overall.
This is why I don't chance them back to their defualt values 'cause the games does it already. So maybe I need to take in the items values and combine them with the changed multiplier values.
</br>
I also need to find a better way to manage effects when loading into new levels, 'cause right now endless is not supported. Also doesn't check if there's a scene where you're running but is not in the fucking if block.
</br>
Just found out Haste uses the steam's workshop... I fucking hate myself.
</br>
Update.... [this exist](https://steamcommunity.com/sharedfiles/filedetails/?id=3460597298)
</br>
Update update... [they have their own modding framework](https://github.com/ItzRock/IamTheCaptainNow/blob/main/Plugin.cs#L11)
</br>
There's literally a file called "Landfall.modding" in it's managed folder... ;-;
</br>
Welp, time to remake this mod using their fucking modding tool... God i fucking hate it here
