using System.Collections;
using System.Linq;
using HarmonyLib;
using UnityEngine;

namespace MaybeMaskedMasked.Patches;

[HarmonyPatch]
internal class MaskedPatch
{
    private static System.Random random;

    [HarmonyPatch(typeof(MaskedPlayerEnemy), "Start"), HarmonyPrefix]
    public static void MaskedSpawnPatch(MaskedPlayerEnemy __instance)
    {
        var keepMask = Plugin.ModConfig.MaskChance.Value;
        var removeMask = random.Next(0, 100);
        if (keepMask > removeMask)
        {
            try
            {
                // Find correct mask to show
                int maskId = System.Math.Max(0, System.Array.FindIndex(__instance.maskTypes, x => x.activeSelf));
                __instance.StartCoroutine(ShowMask(__instance, maskId));
            }
            catch
            {
                Plugin.Logger.LogWarning("Unable to verify mask status. This may be caused by an incompatible version of the game.");
            }
        }
        else
        {
            __instance.StartCoroutine(HideMasks(__instance));
        }
    }

    private static IEnumerator ShowMask(MaskedPlayerEnemy entity, int maskId)
    {
        yield return new WaitForEndOfFrame();
        try
        {
            // Re-activate mask if other mods interfere, unless a mask is activated
            if (entity.maskTypes.All(x => !x.activeSelf))
            {
                entity.maskTypes[maskId].SetActive(true);
                entity.maskTypeIndex = maskId;
            }
        }
        catch
        {
            Plugin.Logger.LogWarning("Unable to verify mask status. This may be caused by an incompatible version of the game, or another mod.");
        }

    }

    private static IEnumerator HideMasks(MaskedPlayerEnemy entity)
    {
        foreach (var mask in entity.maskTypes) mask.SetActive(false);
        yield return new WaitForEndOfFrame();
        // Remove mask again if other mods interfere
        foreach (var mask in entity.maskTypes) mask.SetActive(false);
    }

    [HarmonyPatch(typeof(RoundManager), "ResetEnemySpawningVariables"), HarmonyPostfix]
    public static void StartOfRoundRandomizer(RoundManager __instance)
    {
        var seed = __instance.playersManager.randomMapSeed;
        random = new System.Random(seed + 3708);
    }
}