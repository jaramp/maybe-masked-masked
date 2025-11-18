using System;
using HarmonyLib;

namespace MaybeMaskedMasked.Patches;

[HarmonyPatch]
internal class MaskedPatch
{
    private static Random random;

    [HarmonyPatch(typeof(MaskedPlayerEnemy), "Start"), HarmonyPrefix]
    public static void MaskedSpawnPatch(MaskedPlayerEnemy __instance)
    {
        var keepMask = Plugin.ModConfig.MaskChance.Value;
        if (keepMask == 100) return;

        var removeMask = random.Next(0, 100);
        Plugin.Logger.LogDebug($"Checking ({removeMask} > {keepMask})");
        if (removeMask > keepMask)
        {
            Plugin.Logger.LogDebug("Removing mask");
            foreach (var mask in __instance.maskTypes) mask.SetActive(false);
        }
    }

    [HarmonyPatch(typeof(RoundManager), "ResetEnemySpawningVariables"), HarmonyPostfix]
    public static void StartOfGameRandomizer(RoundManager __instance)
    {
        var seed = __instance.playersManager.randomMapSeed;
        random = new Random(seed + 3708);
    }
}