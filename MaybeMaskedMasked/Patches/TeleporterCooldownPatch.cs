using System.Reflection;
using HarmonyLib;
using UnityEngine;

namespace MaybeMaskedMasked.Patches;

[HarmonyPatch(typeof(ShipTeleporter))]
public static class TeleporterCooldownPatch
{
    private static readonly FieldInfo cooldownTimeField = typeof(ShipTeleporter).GetField("cooldownTime", BindingFlags.Instance | BindingFlags.NonPublic);

    static TeleporterCooldownPatch()
    {
        Plugin.ModConfig.IntegerSetting.OnChanged += UpdateAllTeleporterCooldowns;
    }

    [HarmonyPatch("Awake"), HarmonyPostfix]
    public static void AwakePostfix(ShipTeleporter __instance)
    {
        __instance.cooldownAmount = Plugin.ModConfig.IntegerSetting.Value;
    }

    private static void UpdateAllTeleporterCooldowns(int oldValue, int newValue)
    {
        if (oldValue == newValue) return;

        foreach (ShipTeleporter tp in Object.FindObjectsOfType<ShipTeleporter>())
        {
            tp.cooldownAmount = Plugin.ModConfig.IntegerSetting.Value;
            cooldownTimeField?.SetValue(tp, Mathf.Min(tp.cooldownAmount, (float)(cooldownTimeField?.GetValue(tp) ?? 0)));
        }
    }

}