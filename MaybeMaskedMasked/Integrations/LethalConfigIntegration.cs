using System.Runtime.CompilerServices;
using BepInEx.Configuration;
using LethalConfig;
using LethalConfig.ConfigItems;
using LethalConfig.ConfigItems.Options;

namespace MaybeMaskedMasked.Integrations;

internal static class LethalConfigIntegration
{
    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    public static void Initialize()
    {
        Plugin.Logger.LogInfo("LethalConfig detected — integrating MaybeMaskedMasked config.");
        RegisterAll();
    }

    private static void RegisterAll()
    {
        // # General
        RegisterInput(Plugin.ModConfig.MaskChance.Entry);
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
}
