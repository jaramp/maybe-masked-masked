using BepInEx.Configuration;
using HarmonyLib;

namespace MaybeMaskedMasked.Integrations;

[HarmonyPatch]
public class MirageCompatibility
{
    [HarmonyPatch(typeof(ConfigEntry<bool>), "get_Value")]
    [HarmonyPrefix]
    public static bool ForceMaskTextureValue(ConfigEntry<bool> __instance, ref bool __result)
    {
        var setting = __instance?.Definition;
        if (setting == null) return true;

        if (setting.Section == "Masked enemy" && setting.Key == "Enable mask texture") return !(__result = true);
        return true;
    }
}