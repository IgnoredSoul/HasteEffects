using System.Linq;
using MelonLoader;
using System.Collections.Generic;

// Think I'll call it "HasteEffects" instead of fucking StAtS rAnDoMiZeR :3c
namespace StatsRandomizer;
public class Main : MelonMod
{
    public override void OnInitializeMelon() => new Config();

    public override void OnSceneWasLoaded(int buildIndex, string sceneName)
    {
        // Log scene name & index
        //MelonLogger.Warning($"Loading {sceneName} ({buildIndex})");

        // Check if the scene is of running or a challange, can add more or just flat out do it in every scene.
        if (sceneName.ToLower().Contains("challenge") || (buildIndex == 7 || buildIndex == 13)) { Randomize(); }
    }

    private List<UIStats> stats = new List<UIStats>();

    // I was thinking about making weights to each effect so runspeed has more of a change to be selected than like airspeed.
    // But I want it to be random, fully random at the same time. So it's just a 25% chance to not be selected.
    // Which I don't think it's actually 25% to NOT be selected, 'cause there's times where I've only gotten once effect.
    // idk I was sleep deprived making this shit
    private void Randomize()
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
            Stat stat = availableStats[NumberUtils.random.Next(availableStats.Count)];
            availableStats.Remove(stat);
            if (NumberUtils.random.NextDouble() > (i * 0.25)) selectedStats.Add(stat);
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