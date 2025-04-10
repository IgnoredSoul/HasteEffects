namespace HasteEffects;

internal class Manager
{
    // These two need to be fucking re-done.
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
                //MelonLoader.MelonLogger.Error($"That doesn't exist... {stat}");
                return null;
        }
    }

    internal static void RandomizeStat(Stat stat)
    {
        switch (stat)
        {
            case Stat.Gravity:
                Player.localPlayer.stats.gravity.multiplier = Main.Values.Gravity;
                break;

            case Stat.RunSpeed:
                Player.localPlayer.stats.runSpeed.multiplier = Main.Values.RunSpeed;
                break;

            case Stat.AirSpeed:
                Player.localPlayer.stats.airSpeed.multiplier = Main.Values.AirSpeed;
                break;

            case Stat.TurnSpeed:
                Player.localPlayer.stats.turnSpeed.multiplier = Main.Values.TurnSpeed;
                break;

            case Stat.Drag:
                Player.localPlayer.stats.drag.multiplier = Main.Values.Drag;
                break;

            case Stat.MaxEnergy:
                Player.localPlayer.stats.maxEnergy.multiplier = Main.Values.MaxEnergy;
                break;

            case Stat.PickupRange:
                Player.localPlayer.stats.sparkPickupRange.multiplier = Main.Values.PickupRange;
                break;

            case Stat.Boost:
                Player.localPlayer.stats.boost.multiplier = Main.Values.Boost;
                break;

            case Stat.FastFall:
                Player.localPlayer.stats.fastFallSpeed.multiplier = Main.Values.FastFall;
                break;

            default:
                //MelonLoader.MelonLogger.Error($"Erm? {stat} does not exist?");
                return;
        }
        //MelonLogger.Msg($"{stat}'s mult is now: {multi}x");
    }

    // This needs to be expanded at some point.
    internal static bool IsRun
    {
        get
        {
            UnityEngine.SceneManagement.Scene curScn = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
            UnityEngine.Debug.LogError($"Loaded level: {curScn.name} ({curScn.buildIndex})");
            if ((curScn.name.ToLower().Contains("challenge") && Main.Values.ChallengeLevel_Apply.Value)
                || (curScn.buildIndex == 27 && Main.Values.BossLevel_Apply.Value)
                || curScn.buildIndex == 7)
                return true;
            return false;
        }
    }


    /*
     * RunScene (7)
     * Challenge_AgencyLasers (12)
     * Challenge_AgencyCaptainChase (13)
     * Challenge_AgencyCoinVault (14)
     * Challenge_AgencyTrackingTriangulationSnake (15)
     * Challenge_EyeOfSauron (16)
     * Challenge_FallingIsland (17)
     * Challenge_HeirPointsWayOutOfLavaWorld (18)
     * Challenge_ForestBoss (19)
     * Challenge_WraithRestoreTheWorld (20)
     * Challenge_BigRing_Petter (20)
     * Challenge_ConfusingWraithLookForCluesChallenge (22)
     * Challenge_Ghost_Chase (23)
     * Challenge_SageLearnToFlyChallenge (24)
     * Challenge_DesertBoss (25)
     * Challenge_SnakeBoss (26)
     * EndBoss (27)
    */
}

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

public class Values
{
    public Values()
    {
        HastySetting cfg = new($"<size=80%>{Main.NAME}", Main.GUID);

        ChallengeLevel_Apply = new HastyBool(cfg, "Challenges", "Apply to challenege levels", false);
        BossLevel_Apply = new HastyBool(cfg, "Boss", "Apply to the boss level", false);

        Gravity_Min = new HastyFloat(cfg, "Gravity", "min", 0f, 10f, 0.8f);
        Gravity_Max = new HastyFloat(cfg, "Gravity", "max", 0f, 10f, 2f);

        RunSpeed_Min = new HastyFloat(cfg, "Run Speed", "min", 0f, 10f, 0.4f);
        RunSpeed_Max = new HastyFloat(cfg, "Run Speed", "max", 0f, 10f, 3f);

        TurnSpeed_Min = new HastyFloat(cfg, "Turn Speed", "min", 0f, 10f, 0.8f);
        TurnSpeed_Max = new HastyFloat(cfg, "Turn Speed", "max", 0f, 10f, 3f);

        AirSpeed_Min = new HastyFloat(cfg, "Air Speed", "min", 0f, 10f, 0.4f);
        AirSpeed_Max = new HastyFloat(cfg, "Air Speed", "max", 0f, 10f, 2f);

        Drag_Min = new HastyFloat(cfg, "Drag", "min", 0f, 10f, 0.75f);
        Drag_Max = new HastyFloat(cfg, "Drag", "max", 0f, 10f, 1.25f); // Honestly, fuck drag so much.

        MaxEnergy_Min = new HastyFloat(cfg, "Max Energy", "min", 0f, 10f, 0.8f);
        MaxEnergy_Max = new HastyFloat(cfg, "Max Energy", "max", 0f, 10f, 3f);

        PickupRange_Min = new HastyFloat(cfg, "Pickup Range", "min", 0f, 10f, 0.5f);
        PickupRange_Max = new HastyFloat(cfg, "Pickup Range", "max", 0f, 10f, 2.5f);

        Boost_Min = new HastyFloat(cfg, "Boost", "min", 0f, 10f, 0.75f);
        Boost_Max = new HastyFloat(cfg, "Boost", "max", 0f, 10f, 2.25f);

        FastFall_Min = new HastyFloat(cfg, "Fast Fall", "min", 0f, 10f, 0.5f);
        FastFall_Max = new HastyFloat(cfg, "Fast Fall", "max", 0f, 10f, 3f);

        SparkMulti_Min = new HastyFloat(cfg, "Spark Multiplier", "min", 0f, 10f, 0.95f);
        SparkMulti_Max = new HastyFloat(cfg, "Spark Multiplier", "max", 0f, 10f, 5f);
    }

    internal HastyBool ChallengeLevel_Apply;
    internal HastyBool BossLevel_Apply;

    internal HastyFloat Gravity_Min;
    internal HastyFloat Gravity_Max;
    internal float Gravity => NumberUtils.Next(Gravity_Min.Value, Gravity_Max.Value);

    internal HastyFloat RunSpeed_Min;
    internal HastyFloat RunSpeed_Max;
    internal float RunSpeed=> NumberUtils.Next(RunSpeed_Min.Value, RunSpeed_Max.Value);

    internal HastyFloat TurnSpeed_Min;
    internal HastyFloat TurnSpeed_Max;
    internal float TurnSpeed => NumberUtils.Next(TurnSpeed_Min.Value, TurnSpeed_Max.Value);

    internal HastyFloat AirSpeed_Min;
    internal HastyFloat AirSpeed_Max;
    internal float AirSpeed => NumberUtils.Next(AirSpeed_Min.Value, AirSpeed_Max.Value);

    internal HastyFloat Drag_Min;
    internal HastyFloat Drag_Max;
    internal float Drag => NumberUtils.Next(Drag_Min.Value, Drag_Max.Value);

    internal HastyFloat MaxEnergy_Min;
    internal HastyFloat MaxEnergy_Max;
    internal float MaxEnergy => NumberUtils.Next(MaxEnergy_Min.Value, MaxEnergy_Max.Value);

    internal HastyFloat PickupRange_Min;
    internal HastyFloat PickupRange_Max;
    internal float PickupRange => NumberUtils.Next(PickupRange_Min.Value, PickupRange_Max.Value);

    internal HastyFloat Boost_Min;
    internal HastyFloat Boost_Max;
    internal float Boost => NumberUtils.Next(Boost_Min.Value, Boost_Max.Value);

    internal HastyFloat FastFall_Min;
    internal HastyFloat FastFall_Max;
    internal float FastFall => NumberUtils.Next(FastFall_Min.Value, FastFall_Max.Value);

    internal HastyFloat SparkMulti_Min;
    internal HastyFloat SparkMulti_Max;
    internal float SparkMulti => NumberUtils.Next(SparkMulti_Min.Value, SparkMulti_Max.Value);
}

internal static class NumberUtils
{
    /// <summary>
    /// Represents a shared instance of a random number generator.
    /// </summary>
    internal static readonly System.Random random = new(GenerateTrulyRandomNumber());

    /// <summary>
    /// Generates a truly random number using cryptographic random number generation.
    /// </summary>
    /// <returns>A truly random number within a specified range.</returns>
    internal static int GenerateTrulyRandomNumber()
    {
        using System.Security.Cryptography.RNGCryptoServiceProvider rng = new();
        byte[] bytes = new byte[4]; // 32 bities :3c
        rng.GetBytes(bytes);

        // Convert the random bytes to an integer and ensure it falls within the specified range
        int randomInt = System.BitConverter.ToInt32(bytes, 0);
        return System.Math.Abs(randomInt % (50 - 10)) + 10;
    }

    /// <summary>
    /// Returns a random float number within the specified range.
    /// </summary>
    /// <param name="min">The inclusive lower bound of the random float number to be generated.</param>
    /// <param name="max">The exclusive upper bound of the random float number to be generated.</param>
    /// <returns>A random float number within the specified range.</returns>
    internal static float Next(float min, float max) => (float)((NextD() * (max - min)) + min);

    /// <summary>
    /// Returns a random double number between 0.0 and 1.0.
    /// </summary>
    /// <returns>A random double number between 0.0 and 1.0... Duh</returns>
    internal static double NextD() => random.NextDouble();
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
    FastFall,       // Goated sometimes
    SparkMulti      // Free moneyyyy
}

/* 
    Can add these if I want more effects;

    public PlayerStat maxHealth;                    // Could be silly and wacky
    public PlayerStat runSpeed;                     // ZOOMIES ON THE GROUND
    public PlayerStat airSpeed;                     // ZOOMIES IN THE AIR
    public PlayerStat turnSpeed;                    // Just makes it easier to avoid shit
    public PlayerStat drag;                         // Fuck drag, if it's more than 1.5x then you're fucked, good luck.
    public PlayerStat gravity;                      // Can be good but really GG go next
    public PlayerStat fastFallSpeed;                // Pretty poggers ngl
    public PlayerStat fastFallLerp;                 // Not 100% sure what this does
    public PlayerStat lives;                        // Honeslty I wouldn't do this unless it's like "harcore" and have only one life throughout the entire run
    public PlayerStat dashes;                       // I have no idea what this is
    public PlayerStat boost;                        // Can be cool
    public PlayerStat luck;                         // Kind of useless I think?
    public PlayerStat startWithEnergyPercentage;    // Also kind of useless? Think this only used for setting the energy percentage after a previous level
    public PlayerStat maxEnergy;                    // Kinda cool
    public PlayerStat itemPriceMultiplier;          // Useless since modified stats do not cross between levels
    public PlayerStat itemRarity;                   // ^
    public PlayerStat sparkMultiplier;              // Could be interesting but if it's like 0.8x then that could fuck with people.
    public PlayerStat startingResource;             // Idk what this does
    public PlayerStat energyGain;                   // Would fuck over some people or just outright be over powered depending on what ability
    public PlayerStat damageMultiplier;             // Could be interesting, if it's like 0.25x you could literally just crash into everything. Think this applies to the blue shit
    public PlayerStat sparkPickupRange;             // Yeah, cool ig
    public PlayerStat extraLevelSparks;             // Don't think this will work since it gets applied after world gen
    public PlayerStat extraLevelDifficulty;         // Also don't think this works. ^
*/ 