# NameOfMod

NameOfMod is a mod for Lethal Company that adds a fully-configurable feature set to the game.

## Features

- **Feature One:** The first feature of the mod.
- **Feature Two:** The second feature of the mod.

## Configuration

The mod is configured using BepInEx's configuration system. You can modify the settings in the `BepInEx/config/NameOfMod.cfg` file.

If [LethalConfig](https://thunderstore.io/c/lethal-company/p/AinaVT/LethalConfig/) is installed, you can also configure the mod in-game through the LethalConfig menu.

### Configuration Options

**General**

| Option     | Value     | Default | Description                    |
| ---------- | --------- | ------- | ------------------------------ |
| FeatureOne | `Boolean` | `false` | Toggles the first mod feature. |

**Misc**

| Option            | Value     | Default | Description                                   |
| ----------------- | --------- | ------- | --------------------------------------------- |
| FeatureTwoTime    | `Numeric` | `10`    | Time (in seconds) for the second mod feature. |
| FeatureTwoSetting | `Text`    |         | Sets the text for the second mod feature.     |

As an example, if you wanted a to set the second feature to "Test" every 5 seconds:

```ini
FeatureTwoTime = 5
FeatureTwoSetting = Test
```

## Configuration Details

Here's a section to explain a little more in-depth on how these features work and how to configure them.
Any frequently-asked questions may be appropriate to put here as well.

## Dependencies

- [BepInExPack](https://thunderstore.io/c/lethal-company/p/BepInEx/BepInExPack/)
- (Optional) [LethalConfig](https://thunderstore.io/c/lethal-company/p/AinaVT/LethalConfig/) for in-game configuration.

## Installation

It's recommended to install from [Thunderstore](https://thunderstore.io/c/lethal-company/p/NameOfTeam/NameOfMod/)
using a mod manager such as [Gale](https://github.com/Kesomannen/gale).

## Contributing

See [CONTRIBUTING.md](https://github.com/github-account-name/github-repository-name/?tab=contributing-ov-file) for information on how to contribute to the mod.
