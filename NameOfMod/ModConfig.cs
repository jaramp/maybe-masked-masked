using System.Collections.Generic;
using BepInEx.Configuration;
using NameOfMod.Networking;
using HarmonyLib;

namespace NameOfMod;

public enum ModEnum { None, First, Second, Third }

public class ModConfig
{
    public SyncedEntry<int> IntegerSetting;
    public SyncedEntry<bool> BooleanSetting;
    public SyncedEntry<float> FloatSetting;
    public SyncedEntry<string> StringSetting;
    public SyncedEntry<ModEnum> EnumSetting;

    public ModConfig(ConfigFile config)
    {
        config.SaveOnConfigSet = false;

        // # Truthy
        BooleanSetting = config.BindSynced("Truthy", "BooleanSetting", false, new ConfigDescription("Sets a boolean value."));

        // # Numeric
        IntegerSetting = config.BindSynced("Numeric", "IntegerSetting", 0, new ConfigDescription("Sets an integer value.", new AcceptableValueRange<int>(0, int.MaxValue)));
        FloatSetting = config.BindSynced("Numeric", "FloatSetting", 0f, new ConfigDescription("Sets a float value.", new AcceptableValueRange<float>(0, 1)));

        // # Misc
        StringSetting = config.BindSynced("Misc", "StringSetting", "", new ConfigDescription("Sets a string value."));
        EnumSetting = config.BindSynced("Misc", "EnumSetting", ModEnum.None, new ConfigDescription("Sets an enum value."));

        ((Dictionary<ConfigDefinition, string>)AccessTools.Property(typeof(ConfigFile), "OrphanedEntries").GetValue(config)).Clear();
        config.Save();
        config.SaveOnConfigSet = true;
    }
}
