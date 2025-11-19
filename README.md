# MaybeMaskedMasked

MaybeMaskedMasked is a very small mod for Lethal Company that adds a configurable chance for the Masked enemy to spawn without wearing a mask.
Compatible with most mods using proper configuration.

## Configuration

The mod is configured using BepInEx's configuration system. You can modify the settings in the `BepInEx/config/MaybeMaskedMasked.cfg` file.

If [LethalConfig](https://thunderstore.io/c/lethal-company/p/AinaVT/LethalConfig/) is installed, you can also configure the mod in-game through the LethalConfig menu.

### Configuration Options

**General**

| Option     | Value     | Default | Description                                                        |
| ---------- | --------- | ------- | ------------------------------------------------------------------ |
| MaskChance | `Integer` | `50`    | Percent chance between 0-100 that a Masked will be wearing a mask. |

The base game equivalent would be setting `MaskChance` to `100` to always spawn Masked enemies with masks.

## Dependencies

- [BepInExPack](https://thunderstore.io/c/lethal-company/p/BepInEx/BepInExPack/)
- (Optional) [LethalConfig](https://thunderstore.io/c/lethal-company/p/AinaVT/LethalConfig/) for in-game configuration.

## Compatibility

|     | Mod                         | Version | Notes                                             |
| --- | --------------------------- | ------- | ------------------------------------------------- |
| 🟢  | **Mirage**                  | 0.0.0   | Overrides Mirage's `Enable mask textures` setting |
| 🟢  | **MaskedInvisFix**          | 0.0.0   |                                                   |
| 🟢  | **TooManyEmotes**           | 0.0.0   |                                                   |
| 🟢  | **MaskFixes**               | 0.0.0   | Works with Tragedy masks                          |
| ⚫  | **Masked Mask**             | 0.0.0   |                                                   |
| ⚫  | **MaskedEnemyOverhaulFork** | 0.0.0   |                                                   |
| ⚫  | **DramaMask**               | 0.0.0   |                                                   |
| 🔴  | **TakeThatMaskOff**         | 0.0.0   | Fundamentally changes mask logic                  |

## Installation

It's recommended to install from [Thunderstore](https://thunderstore.io/c/lethal-company/p/jaramp/MaybeMaskedMasked/)
using a mod manager such as [Gale](https://github.com/Kesomannen/gale).

## Contributing

See [CONTRIBUTING.md](https://github.com/jaramp/maybe-masked-masked/?tab=contributing-ov-file) for information on how to contribute to the mod.
