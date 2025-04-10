using Landfall.Modding;

namespace HasteEffects;

[LandfallPlugin]
public class Main
{
    private static HarmonyLib.Harmony harmony;
    public static string GUID = "com.github.ignoredsoul";
    public static string NAME = "HastyEffects";
    public static Values Values { get; set; }
    static Main()
    {
        harmony = new(GUID);
        harmony.PatchAll(typeof(Patching));
    }

    private static List<UIStats> stats = new List<UIStats>();

    // I was thinking about making weights to each effect so runspeed has more of a change to be selected than like airspeed.
    // But I want it to be random, fully random at the same time. So it's just a 25% chance to not be selected.
    // Which I don't think it's actually 25% to NOT be selected, 'cause there's times where I've only gotten one effect.
    // idk I was sleep deprived making this shit
    internal static void Randomize()
    {
        /// Old code that I was gonna do, basically just chooses a random enum from the list and that's the stat it changes
        /// but opped out 'cause changing multiple effects sounds more fun
        //Stat randomStat = Enum.GetValues(typeof(Stat))
        //.Cast<Stat>()
        //.OrderBy(x => NumberUtils.random.Next())
        //.FirstOrDefault();
        //Manager.RandomizeStat(randomStat, NumberUtils.Next(1.0f, 5.0f));

        List<Stat> availableStats = Enum.GetValues(typeof(Stat)).Cast<Stat>().ToList();
        List<Stat> selectedStats = new();

        // We always want to include a stat change, what's the point if the mod if it's just going to do nothing
        Stat gstat = availableStats[NumberUtils.random.Next(0, availableStats.Count)];
        availableStats.Remove(gstat);
        selectedStats.Add(gstat);

        for (int i = 0; (i < 5 && availableStats.Any()); i++)
        {
            Stat stat = availableStats[NumberUtils.random.Next(0, availableStats.Count)];
            availableStats.Remove(stat);
            if (NumberUtils.NextD() > (i * 0.25)) selectedStats.Add(stat);
        }

        stats.ForEach(stat => stat.Destroy());
        stats.Clear();
        selectedStats.ForEach((stat) =>
        {
            Manager.RandomizeStat(stat);
            UIStats newStat = new(
                stat.ToString() + " / " + Manager.GetStat(stat).multiplier.ToString("0.0") + "x",
                (stats.Count + 1)
            );
            stats.Add(newStat);
        });
        //MelonLogger.Warning($"Randomized {selectedStats.Count} stats");
    }
}

[HarmonyLib.HarmonyPatch]
public class Patching
{
    // This get's called even if the player starts the level for a first time.
    // Allows us to reroll effects after a player loses a life in a run.

    // LOADING INTO THE FUCKING BOSS FIGHT DOES NOT CALL THIS!?!? WHAT THE FUCK?
    // So I should just make it roll when the player spawns?
    // But the random stats is too quick for the actual stats to load / take place.
    // WHAT THE FUCK
    [HarmonyLib.HarmonyPatch(typeof(PlayerCharacter), "RestartPlayer_Launch", new System.Type[] { typeof(UnityEngine.Transform), typeof(float) })]
    [HarmonyLib.HarmonyPostfix]
    private static void OnRestartPlayer(UnityEngine.Transform spawnPoint, float minVel = 0f) { if (Manager.IsRun) Main.Randomize(); }

    [HarmonyLib.HarmonyPatch(typeof(HasteSettingsHandler), "RegisterPage")]
    [HarmonyLib.HarmonyPrefix]
    static void RegisterPagePrefix(HasteSettingsHandler __instance) => Main.Values = new Values();

    [HarmonyLib.HarmonyPatch(typeof(Zorro.Localization.LocalizeUIText), "OnStringChanged")]
    [HarmonyLib.HarmonyPostfix]
    static void OnStringChangedPostfix(Zorro.Localization.LocalizeUIText __instance)
    {
        if (__instance.String == null) return;
        if (__instance.String.TableReference.TableCollectionName != Main.GUID) return;
        __instance.Text.text = __instance.String.TableEntryReference.Key;
    }
}