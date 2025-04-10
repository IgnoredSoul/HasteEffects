using System;
using System.Collections.Generic;
using System.Linq;
using MelonLoader;

// Think I'll call it "HasteEffects" instead of fucking StAtS rAnDoMiZeR :3c

[assembly: MelonInfo(typeof(StatsRandomizer.Main), "HasteEffects", "1.0.0", "IGNOREDSOUL")]

namespace StatsRandomizer
{
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

        private void Randomize()
        {
            //Stat randomStat = Enum.GetValues(typeof(Stat))
            //.Cast<Stat>()
            //.OrderBy(x => NumberUtils.random.Next())
            //.FirstOrDefault();

            //Manager.RandomizeStat(randomStat, NumberUtils.Next(1.0f, 5.0f));

            List<Stat> availableStats = Enum.GetValues(typeof(Stat)).Cast<Stat>().ToList();
            List<Stat> selectedStats = new();

            for (int i = 0; i < 5 && availableStats.Any(); i++)
            {
                Stat stat = availableStats[NumberUtils.random.Next(availableStats.Count)];
                availableStats.Remove(stat);
                if (NumberUtils.random.NextDouble() > (i * 0.25)) selectedStats.Add(stat);
            }

            stats.ForEach(stat => UnityEngine.Object.Destroy(stat.InfoObject));
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
}