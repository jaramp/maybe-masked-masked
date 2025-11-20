# MaybeMaskedMasked

MaybeMaskedMasked is a small mod that adds a configurable chance for the Masked enemy to spawn without wearing a mask.

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

The following mods also modify Masked enemies. They've been tested for compatibility with MaybeMaskedMasked using Lethal Company v73.

|     | Mod                         | Version | Notes                                                                              |
| --- | --------------------------- | ------- | ---------------------------------------------------------------------------------- |
| 🟢  | **DramaMask**               | 2.1.4   |                                                                                    |
| 🟢  | **MaskedEnemyOverhaulFork** | 3.4.0   | Overrides `Remove Mask From Masked Enemy`, works with `Reveal Mask When Attacking` |
| 🟢  | **MaskedInvisFix**          | 0.0.2   |                                                                                    |
| 🟢  | **MaskFixes**               | 1.5.2   | Works with `Tragedy Chance`                                                        |
| 🟢  | **Mirage**                  | 1.28.0  | Overrides `Enable mask textures` setting                                           |
| 🟢  | **TooManyEmotes**           | 2.3.13  |                                                                                    |
| 🟠  | **TakeThatMaskOff**         | 2.1.6   | Masks always drop on death, even when visually removed                             |
| 🔴  | **Masked Mask**             | 1.1.2   | Multiple issues; do not use these mods together                                    |

## Installation

It's recommended to install from [Thunderstore](https://thunderstore.io/c/lethal-company/p/jaramp/MaybeMaskedMasked/)
using a mod manager such as [Gale](https://github.com/Kesomannen/gale).

## Contributing

See [CONTRIBUTING.md](https://github.com/jaramp/maybe-masked-masked/?tab=contributing-ov-file) for information on how to contribute to the mod.
