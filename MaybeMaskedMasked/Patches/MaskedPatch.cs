using System.Collections;
using System.Linq;
using HarmonyLib;
using Unity.Netcode;
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
        if (removeMask > keepMask)
        {
            __instance.StartCoroutine(HideMasks(__instance));
        }
        else
        {
            // Find correct mask to show
            int maskId = System.Math.Max(0, System.Array.FindIndex(__instance.maskTypes, x => x.activeSelf));
            __instance.StartCoroutine(ShowMask(__instance, maskId));
        }
    }

    private static IEnumerator ShowMask(MaskedPlayerEnemy entity, int maskId)
    {
        Plugin.Logger.LogDebug("Adding mask");
        yield return new UnityEngine.WaitForEndOfFrame();
        // Re-activate mask if other mods interfere, unless a mask is activated
        if (entity.maskTypes.All(x => !x.activeSelf))
        {
            entity.maskTypes[maskId].SetActive(true);
            entity.maskTypeIndex = maskId;
        }
    }

    private static IEnumerator HideMasks(MaskedPlayerEnemy entity)
    {
        Plugin.Logger.LogDebug("Removing mask");
        foreach (var mask in entity.maskTypes) mask.SetActive(false);
        yield return new UnityEngine.WaitForEndOfFrame();
        // Remove mask again if other mods interfere
        foreach (var mask in entity.maskTypes) mask.SetActive(false);
        // Some mods replace the mask texture with an actual mask item
        // Check if a mask was spawned, and if so, remove it
        DestroySpawnedMaskObject(entity);
    }

    private static bool _ignoreSpawnedMaskRemoval = false;
    private static void DestroySpawnedMaskObject(MaskedPlayerEnemy entity)
    {
        if (_ignoreSpawnedMaskRemoval) return;
        try
        {
            HauntedMaskItem[] masks = Object.FindObjectsOfType<HauntedMaskItem>(true);
            foreach (var mask in masks)
            {
                // Verify mask is on Masked's face (held by enemy and approx 2.4f away)
                if (mask == null || !mask.isHeldByEnemy || mask.hasBeenHeld) continue;
                if (Vector3.Distance(mask.transform.position, entity.transform.position) > 5f) continue;

                Plugin.Logger.LogDebug($"Destroying mask {mask.gameObject.name} attached to enemy.");

                // Network-safe destruction
                NetworkObject netObj = mask.GetComponent<NetworkObject>();
                if (netObj != null && netObj.IsSpawned) netObj.Despawn(true);
                else Object.Destroy(mask.gameObject);
                return;
            }
        }
        catch
        {
            Plugin.Logger.LogError("Unexpected error when attempting to remove spawned mask item.");
            Plugin.Logger.LogError("This may have been caused by an incompatibility issue with your version of Lethal Company.");
            Plugin.Logger.LogError("It may have also been caused by an incompatibility with another mod.");
        }
        // No mask spawns detected/removed: disable future checks
        _ignoreSpawnedMaskRemoval = true;
    }

    [HarmonyPatch(typeof(RoundManager), "ResetEnemySpawningVariables"), HarmonyPostfix]
    public static void StartOfGameRandomizer(RoundManager __instance)
    {
        var seed = __instance.playersManager.randomMapSeed;
        random = new System.Random(seed + 3708);
    }
}