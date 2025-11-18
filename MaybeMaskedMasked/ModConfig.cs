using System.Collections.Generic;
using BepInEx.Configuration;
using MaybeMaskedMasked.Networking;
using HarmonyLib;

namespace MaybeMaskedMasked;

public enum ModEnum { None, First, Second, Third }

public class ModConfig
{
    public SyncedEntry<int> MaskChance;

    public ModConfig(ConfigFile config)
    {
        config.SaveOnConfigSet = false;

        // # General
        MaskChance = config.BindSynced("General", "MaskChance", 100, new ConfigDescription("Percent chance for a Masked to be wearing a mask.", new AcceptableValueRange<int>(0, 100)));

        ((Dictionary<ConfigDefinition, string>)AccessTools.Property(typeof(ConfigFile), "OrphanedEntries").GetValue(config)).Clear();
        config.Save();
        config.SaveOnConfigSet = true;
    }
}
