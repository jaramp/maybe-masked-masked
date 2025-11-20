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
        Plugin.Logger.LogInfo($"LethalConfig detected — integrating {PluginInfo.PLUGIN_GUID} config.");
        RegisterAll();

        // LethalConfigManager.AddConfigItem(new GenericButtonConfigItem("Help", "Name", "Description", "Button", () =>
        // {
        //     Plugin.Logger.LogDebug($"== MaskChance: {Plugin.ModConfig.MaskChance.Value} ==");
        // }));
    }

    private static void RegisterAll()
    {
        // # General
        RegisterSlider(Plugin.ModConfig.MaskChance.Entry);
    }

    private static void RegisterInput(ConfigEntry<int> entry)
    {
        var opts = new IntInputFieldOptions { RequiresRestart = false };
        LethalConfigManager.AddConfigItem(new IntInputFieldConfigItem(entry, opts));
    }

    private static void RegisterSlider(ConfigEntry<int> entry)
    {
        var opts = new IntSliderOptions { RequiresRestart = false };
        LethalConfigManager.AddConfigItem(new IntSliderConfigItem(entry, opts));
    }

    private static void RegisterSlider(ConfigEntry<float> entry)
    {
        var opts = new FloatSliderOptions { RequiresRestart = false };
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
