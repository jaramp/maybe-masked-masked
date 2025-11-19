using System.Reflection;
using BepInEx.Configuration;
using HarmonyLib;

namespace MaybeMaskedMasked.Integrations;

[HarmonyPatch(typeof(Mirage.Domain.Config.LocalConfig))]
public static class MirageLocalConfigPatch
{
    [HarmonyPostfix]
    [HarmonyPatch(MethodType.Constructor, [typeof(ConfigFile), typeof(ConfigFile), typeof(ConfigFile)])]
    public static void PostCtor(Mirage.Domain.Config.LocalConfig __instance)
    {
        try
        {
            Plugin.Logger.LogDebug("Forcing Mirage 'Enable mask texture' to true and detaching SettingChanged (LocalConfig ctor).");
            var maskEntry = __instance.EnableMaskTexture;
            maskEntry.ConfigFile.SaveOnConfigSet = false;
            maskEntry.Value = true;
            var evtField = typeof(ConfigEntryBase).GetField("SettingChanged", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
            evtField?.SetValue(maskEntry, null);
            maskEntry.ConfigFile.SaveOnConfigSet = true;
            Plugin.Logger.LogDebug("Successfully suppressed Mirage config setting.");
        }
        catch
        {
            Plugin.Logger.LogError($"Failed to patch Mirage. Please ensure 'Enable mask texture' is turned ON in Mirage settings in order for this mod to work.");
        }
    }
}