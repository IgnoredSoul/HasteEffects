using HarmonyLib;
using Zorro.Settings;
using Unity.Mathematics;
using UnityEngine.Localization;

// Modified from https://github.com/NaokoAF/HastyControls, just making it fit my style better ig
namespace HasteEffects;
public class HastySetting
{
    public string ModName { get; private set; }
    public string ModDesc { get; private set; }

    static AccessTools.FieldRef<HasteSettingsHandler, List<Setting>> settingsRef = AccessTools.FieldRefAccess<HasteSettingsHandler, List<Setting>>("settings");
    static AccessTools.FieldRef<HasteSettingsHandler, ISettingsSaveLoad> settingsSaveLoadRef = AccessTools.FieldRefAccess<HasteSettingsHandler, ISettingsSaveLoad>("_settingsSaveLoad");

    public HastySetting(string modName, string modGUID)
    {
        ModName = modName;
        SettingsUIPage.LocalizedTitles.Add(ModName, new(Main.GUID, ModName));
    }
    public void Add<T>(T setting) where T : Setting
    {
        var handler = GameHandler.Instance.SettingsHandler;
        settingsRef(handler).Add(setting);
        setting.Load(settingsSaveLoadRef(handler));
        setting.ApplyValue();
    }

    internal LocalizedString CreateDisplayName(string name, string description) => new(Main.GUID, $"{name}\n<size=60%><alpha=#50>{description}");
}

public class HastyFloat : FloatSetting, IExposedSetting
{
    public event Action<float>? Applied;

    private float defaultValue;
    private float2 minMax;
    private HastySetting config;
    private LocalizedString displayName;

    public HastyFloat(HastySetting cfg, string name, string description, float min, float max, float defaultValue)
    {
        config = cfg;
        this.defaultValue = defaultValue;
        minMax = new float2(min, max);
        displayName = cfg.CreateDisplayName(name, description);
        cfg.Add(this);
    }

    public string GetCategory() => config.ModName;
    public LocalizedString GetDisplayName() => displayName;
    public override float GetDefaultValue() => defaultValue;
    public override float2 GetMinMaxValue() => minMax;
    public override void ApplyValue() => Applied?.Invoke(Value);
}

public class HastyBool : BoolSetting, IExposedSetting, IEnumSetting
{
    public event Action<bool>? Applied;

    private bool defaultValue;
    private List<string> choices;
    private HastySetting config;
    private LocalizedString displayName;

    // Constructor follows the new format: config, name, description, defaultValue, offChoice, onChoice
    public HastyBool(HastySetting cfg, string name, string description, bool defaultValue, string offChoice = "Off", string onChoice = "On")
    {
        config = cfg; // Automatically set category from config
        this.defaultValue = defaultValue;
        displayName = cfg.CreateDisplayName(name, description);
        choices = new List<string> { offChoice, onChoice };

        // Automatically add this setting to the HastySettings configuration
        cfg.Add(this);
    }

    public string GetCategory() => config.ModName;
    public LocalizedString GetDisplayName() => displayName;
    public override bool GetDefaultValue() => defaultValue;
    public override LocalizedString OffString => null!;
    public override LocalizedString OnString => null!;

    List<string> IEnumSetting.GetUnlocalizedChoices() => choices;
    public override void ApplyValue() => Applied?.Invoke(Value);
}
