using UnityEngine.SceneManagement;

namespace StatsRandomizer;

// This entire class can be better but this is what my sleep deprived brain could manage...
// Still this took me like an hour ;-;
internal class Manager
{
    internal static PlayerStat GetStat(Stat stat)
    {
        switch (stat)
        {
            case Stat.Gravity:
                return Player.localPlayer.stats.gravity;

            case Stat.RunSpeed:
                return Player.localPlayer.stats.runSpeed;

            case Stat.AirSpeed:
                return Player.localPlayer.stats.airSpeed;

            case Stat.TurnSpeed:
                return Player.localPlayer.stats.turnSpeed;

            case Stat.Drag:
                return Player.localPlayer.stats.drag;

            case Stat.MaxEnergy:
                return Player.localPlayer.stats.maxEnergy;

            case Stat.PickupRange:
                return Player.localPlayer.stats.sparkPickupRange;

            case Stat.Boost:
                return Player.localPlayer.stats.boost;

            case Stat.FastFall:
                return Player.localPlayer.stats.fastFallSpeed;

            default:
                MelonLoader.MelonLogger.Error($"That doesn't exist... {stat}");
                return null;
        }
    }

    internal static void RandomizeStat(Stat stat)
    {
        switch (stat)
        {
            case Stat.Gravity:
                Player.localPlayer.stats.gravity.multiplier = NumberUtils.Next(Config.Gravity_Min, Config.Gravity_Max);
                break;

            case Stat.RunSpeed:
                Player.localPlayer.stats.runSpeed.multiplier = NumberUtils.Next(Config.RunSpeed_Min, Config.RunSpeed_Max);
                break;

            case Stat.AirSpeed:
                Player.localPlayer.stats.airSpeed.multiplier = NumberUtils.Next(Config.AirSpeed_Min, Config.AirSpeed_Max);
                break;

            case Stat.TurnSpeed:
                Player.localPlayer.stats.turnSpeed.multiplier = NumberUtils.Next(Config.TurnSpeed_Min, Config.TurnSpeed_Max);
                break;

            case Stat.Drag:
                Player.localPlayer.stats.drag.multiplier = NumberUtils.Next(Config.Drag_Min, Config.Drag_Max);
                break;

            case Stat.MaxEnergy:
                Player.localPlayer.stats.maxEnergy.multiplier = NumberUtils.Next(Config.MaxEnergy_Min, Config.MaxEnergy_Max);
                break;

            case Stat.PickupRange:
                Player.localPlayer.stats.sparkPickupRange.multiplier = NumberUtils.Next(Config.PickupRange_Min, Config.PickupRange_Max);
                break;

            case Stat.Boost:
                Player.localPlayer.stats.boost.multiplier = NumberUtils.Next(Config.Boost_Min, Config.Boost_Max);
                break;

            case Stat.FastFall:
                Player.localPlayer.stats.fastFallSpeed.multiplier = NumberUtils.Next(Config.FastFall_Min, Config.FastFall_Max);
                break;

            default:
                MelonLoader.MelonLogger.Error($"Erm? {stat} does not exist?");
                return;
        }
        //MelonLogger.Msg($"{stat}'s mult is now: {multi}x");
    }

    /// <summary>
    /// Ruturns true if the player is in a defined scene.
    /// </summary>
    // This needs to be expanded at some point.
    internal static bool IsRun
    {
        get
        {
            Scene curScn = SceneManager.GetActiveScene();
            if (curScn.name.ToLower().Contains("challenge") ||
                (curScn.buildIndex == 7 || curScn.buildIndex == 13))
                return true;
            return false;
        }
    }
}

// Kind of want to addd colors depending if the given effect is worth, like pickup range would be a blue color
// and like high gravity would be a red color? I dont know 'cause high gravity sounds bad but paired with increased run speed is goated.
internal class UIStats
{
    public UIStats(string Text, int shift)
    {
        InfoObject = UnityEngine.GameObject.Instantiate(UnityEngine.GameObject.Find("GAME/UI_Gameplay/Speed/"), UnityEngine.GameObject.Find("GAME/UI_Gameplay/").transform);
        UnityEngine.Object.Destroy(InfoObject.GetComponent<UI_Speed>());
        InfoObject.transform.position = new(InfoObject.transform.position.x, InfoObject.transform.position.y - (50 * shift), InfoObject.transform.position.z);
        InfoObject.GetComponentInChildren<UnityEngine.UI.Image>().color = new UnityEngine.Color(0.067f, 0.878f, 0.537f);

        InfoText = InfoObject.GetComponent<TMPro.TextMeshProUGUI>();
        InfoText.text = Text;
        InfoText.richText = true;
        InfoText.enableAutoSizing = true;
        InfoText.fontSizeMax = 20;
        InfoText.color = new UnityEngine.Color(0, 1, 0.58f);
    }

    public void Destroy() => UnityEngine.GameObject.Destroy(InfoObject);

    public UnityEngine.GameObject InfoObject;
    public TMPro.TextMeshProUGUI InfoText;
}

public enum Stat
{
    RunSpeed,       // FUCKING ZOOM unless it's a negative then... alt f4 try again lmao
    Gravity,        // Garbage unless run speed is also very high, otherwise, good luck
    AirSpeed,       // Zoom in the air or literally slow's ur ass down if not on the ground
    TurnSpeed,      // Basically just dodge shit quicker
    Drag,           // The fucking worst if set high
    MaxEnergy,      // Not 100x usefull but can save your ass
    PickupRange,    // Goated
    Boost,          // Think this only applied to your power
    FastFall        // Goated sometimes
}

/* 
    Can add these if I want more effects;

    public PlayerStat maxHealth;                    // Could be silly and wacky
    public PlayerStat runSpeed;                     // Added
    public PlayerStat airSpeed;                     // Added
    public PlayerStat turnSpeed;                    // Added
    public PlayerStat drag;                         // Added
    public PlayerStat gravity;                      // Added
    public PlayerStat fastFallSpeed;                // Added
    public PlayerStat fastFallLerp;                 // Not 100% sure what this does
    public PlayerStat lives;                        // Honeslty I wouldn't do this unless it's like "harcore" and have only one life throughout the entire run
    public PlayerStat dashes;                       // I have no idea what this is
    public PlayerStat boost;                        // Added
    public PlayerStat luck;                         // Kind of useless I think?
    public PlayerStat startWithEnergyPercentage;    // Also kind of useless? Think this only used for setting the energy percentage after a previous level
    public PlayerStat maxEnergy;                    // Added
    public PlayerStat itemPriceMultiplier;          // Useless since modified stats do not cross between levels
    public PlayerStat itemRarity;                   // ^
    public PlayerStat sparkMultiplier;              // Could be interesting but if it's like 0.8x then that could fuck with people.
    public PlayerStat startingResource;             // Idk what this does
    public PlayerStat energyGain;                   // Would fuck over some people or just outright be over powered depending on what ability
    public PlayerStat damageMultiplier;             // Could be interesting, if it's like 0.25x you could literally just crash into everything. Think this applies to the blue shit
    public PlayerStat sparkPickupRange;             // Added
    public PlayerStat extraLevelSparks;             // Don't think this will work since it gets applied after world gen
    public PlayerStat extraLevelDifficulty;         // Also don't think this works. ^
*/ 