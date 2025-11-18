using System.Runtime.CompilerServices;
using BepInEx.Configuration;
using LethalConfig;
using LethalConfig.ConfigItems;
using LethalConfig.ConfigItems.Options;

namespace NameOfMod.Integrations;

internal static class LethalConfigIntegration
{
    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    public static void Initialize()
    {
        Plugin.Logger.LogInfo("LethalConfig detected — integrating NameOfMod config.");
        RegisterAll();
    }

    private static void RegisterAll()
    {
        // # Truthy
        RegisterCheckbox(Plugin.ModConfig.BooleanSetting.Entry);

        // # Numeric
        RegisterInput(Plugin.ModConfig.IntegerSetting.Entry);
        RegisterSlider(Plugin.ModConfig.FloatSetting.Entry, 0, 1);

        // # Misc
        RegisterTextInput(Plugin.ModConfig.StringSetting.Entry);
        RegisterDropdown(Plugin.ModConfig.EnumSetting.Entry);

        // # Help
        RegisterYippeeButton();
    }

    private static void RegisterInput(ConfigEntry<int> entry)
    {
        var opts = new IntInputFieldOptions { RequiresRestart = false };
        LethalConfigManager.AddConfigItem(new IntInputFieldConfigItem(entry, opts));
    }

    private static void RegisterSlider(ConfigEntry<int> entry, int min, int max)
    {
        var opts = new IntSliderOptions { Min = min, Max = max, RequiresRestart = false };
        LethalConfigManager.AddConfigItem(new IntSliderConfigItem(entry, opts));
    }

    private static void RegisterSlider(ConfigEntry<float> entry, float min, float max)
    {
        var opts = new FloatSliderOptions { Min = min, Max = max, RequiresRestart = false };
        LethalConfigManager.AddConfigItem(new FloatSliderConfigItem(entry, opts));
    }

    private static void RegisterCheckbox(ConfigEntry<bool> entry)
    {
        LethalConfigManager.AddConfigItem(new BoolCheckBoxConfigItem(entry, requiresRestart: false));
    }

    private static void RegisterDropdown(ConfigEntry<ModEnum> entry)
    {
        var opts = new EnumDropDownOptions { RequiresRestart = false };
        LethalConfigManager.AddConfigItem(new EnumDropDownConfigItem<ModEnum>(entry, opts));
    }

    private static void RegisterTextInput(ConfigEntry<string> entry)
    {
        LethalConfigManager.AddConfigItem(new TextInputFieldConfigItem(entry, requiresRestart: false));
    }

    private static void RegisterYippeeButton()
    {
        LethalConfigManager.AddConfigItem(new GenericButtonConfigItem("Help", "Name", "Description", "Button", () =>
        {
            HUDManager.Instance?.DisplayTip("Yippie", "You clicked the button!");
        }));
    }
}
