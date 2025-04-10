using System.Linq;
using MelonLoader;
using System.Collections.Generic;

// Think I'll call it "HasteEffects" instead of fucking StAtS rAnDoMiZeR :3c
namespace StatsRandomizer;

public class Main : MelonMod
{
    public HarmonyLib.Harmony harmony;
    public override void OnInitializeMelon()
    {
        new Config();

        harmony = new HarmonyLib.Harmony("com.github.ignoredsoul");
        harmony.PatchAll(typeof(Patching));
    }

    private static List<UIStats> stats = new List<UIStats>();

    // I was thinking about making weights to each effect so runspeed has more of a change to be selected than like airspeed.
    // But I want it to be random, fully random at the same time. So it's just a 25% chance to not be selected.
    // Which I don't think it's actually 25% to NOT be selected, 'cause there's times where I've only gotten once effect.
    // idk I was sleep deprived making this shit
    internal static void Randomize()
    {
        /// Old code that I was gonna do, basically just chooses a random enum from the list and that's the stat it changes-
        /// but opped out 'cause changing multiple effects is better
        //Stat randomStat = Enum.GetValues(typeof(Stat))
        //.Cast<Stat>()
        //.OrderBy(x => NumberUtils.random.Next())
        //.FirstOrDefault();
        //Manager.RandomizeStat(randomStat, NumberUtils.Next(1.0f, 5.0f));

        List<Stat> availableStats = System.Enum.GetValues(typeof(Stat)).Cast<Stat>().ToList();
        List<Stat> selectedStats = new();

        for (int i = 0; i < 5 && availableStats.Any(); i++)
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
    [HarmonyLib.HarmonyPatch(typeof(PlayerCharacter), "RestartPlayer_Launch", new System.Type[] { typeof(UnityEngine.Transform), typeof(float) })]
    [HarmonyLib.HarmonyPostfix]
    private static void OnRestartPlayer(UnityEngine.Transform spawnPoint, float minVel = 0f) { if (Manager.IsRun) Main.Randomize(); }
}