using MelonLoader;

namespace StatsRandomizer
{
    internal class Config
    {
        private MelonPreferences_Category Category;

        internal static float Gravity_Min;
        internal static float Gravity_Max;

        internal static float RunSpeed_Min;
        internal static float RunSpeed_Max;

        internal static float TurnSpeed_Min;
        internal static float TurnSpeed_Max;

        internal static float AirSpeed_Min;
        internal static float AirSpeed_Max;

        internal static float Drag_Min;
        internal static float Drag_Max;

        internal static float Luck_Min;
        internal static float Luck_Max;

        internal static float MaxEnergy_Min;
        internal static float MaxEnergy_Max;

        internal static float PickupRange_Min;
        internal static float PickupRange_Max;

        internal static float Boost_Min;
        internal static float Boost_Max;

        internal static float FastFall_Min;
        internal static float FastFall_Max;

        public Config()
        {
            Category = MelonPreferences.CreateCategory("Randomization");
            Category.SetFilePath("UserData/StatsRandomizer.cfg");

            Gravity_Min = Category.CreateEntry("Gravity Min", 0.25f).Value;
            Gravity_Max = Category.CreateEntry("Gravity Max", 4.0f).Value;

            RunSpeed_Min = Category.CreateEntry("RunSpeed Min", 0.8f).Value;
            RunSpeed_Max = Category.CreateEntry("RunSpeed Max", 2.2f).Value;

            TurnSpeed_Min = Category.CreateEntry("TurnSpeed Min", 0.5f).Value;
            TurnSpeed_Max = Category.CreateEntry("TurnSpeed Max", 3.0f).Value;

            MaxEnergy_Min = Category.CreateEntry("MaxEnergy Min", 1f).Value;
            MaxEnergy_Max = Category.CreateEntry("MaxEnergy Max", 5f).Value;

            AirSpeed_Min = Category.CreateEntry("AirSpeed Min", 0.75f).Value;
            AirSpeed_Max = Category.CreateEntry("AirSpeed Max", 3.25f).Value;

            Drag_Min = Category.CreateEntry("Drag Min", 0.85f).Value;
            Drag_Max = Category.CreateEntry("Drag Max", 2.35f).Value;

            PickupRange_Min = Category.CreateEntry("PickupRange Min", 0.85f).Value;
            PickupRange_Max = Category.CreateEntry("PickupRange Max", 3.0f).Value;

            Boost_Min = Category.CreateEntry("Boost Min", 0.75f).Value;
            Boost_Max = Category.CreateEntry("Boost Max", 2.0f).Value;

            FastFall_Min = Category.CreateEntry("FastFall Min", 0.25f).Value;
            FastFall_Max = Category.CreateEntry("FastFall Max", 5.0f).Value;

            Category.SaveToFile();
        }
    }
}