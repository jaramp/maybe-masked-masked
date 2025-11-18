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
        if (removeMask > keepMask)
        {
            __instance.maskTypes[0].SetActive(false);
            __instance.maskTypes[1].SetActive(false);
        }
    }

    [HarmonyPatch(typeof(RoundManager), "ResetEnemySpawningVariables"), HarmonyPostfix]
    public static void StartOfGameRandomizer(RoundManager __instance)
    {
        var seed = __instance.playersManager.randomMapSeed;
        random = new Random(seed + 3708);
    }
}