# <img src="app/appicon.svg" alt="PreySense logo" width="42" height="42" align="left"> PreySense

PreySense is a lightweight Windows utility for Acer Predator laptops, forked from G-Helper. It provides quick, direct control over performance modes, fans, GPU overclocking, RGB lighting, display options, and a custom hardware overlay,completely bypassing the bloated official Predator Sense software.

This project is experimental and hardware-specific. It has been developed and tested on limited Acer Predator models, so compatibility with other Acer laptops is not guaranteed.

<p align="center">
  <img src="docs/pics/Prey Sense.png" alt="Prey Sense Interface" width="380"><br>
  <b>PreySense Interface</b>
</p>

## Features

- **Performance Modes**: Cycle between **Eco**, **Silent**, **Balanced**, **Performance**, and **Turbo**.
- **Per-Mode Customization**: Mode-based CPU power limits, GPU offsets, and custom fan curves. Use Ctrl for point snapping in fan curves.
- **CPU & GPU Tuning**:
  - Direct CPU power limit (PL1 / PL2) controls.
  - NVIDIA GPU core and memory clock overclocking offsets.
- **GPU Mode Switching**: Toggles between **Endurance** (iGPU only), **Standard** (iGPU + dGPU), and **Ultimate** (dGPU exclusive). Includes an automatic iGPU switch toggle on battery.
- **Predator Key Integration**: Full physical mode-switch key and custom shortcuts support. Use Predator Key + 1-5 to switch performance modes.
- **Display Configurations**: Automatic display refresh-rate switching, LCD overdrive controls, and refresh-rate color profiles.
- **Battery Management**: Charge limit controls to preserve battery lifespan.
- **Keyboard RGB Control**: RGB control for keyboard.
- **Compact Hardware Overlay**: A styled HUD showing real-time CPU/GPU temperatures, fan RPMs, power draw, RAM/VRAM utilization, FPS counter, and power graphs.
<p align="center">
  <img src="docs/pics/Overlay.png" alt="Prey Sense Overlay" width="380"><br>
  <b>Hardware Performance Overlay</b>
</p>

## Requirements

- **OS**: Windows 10 or Windows 11 x64.
- **Hardware**: Acer Predator laptop with compatible Acer WMI and AcerService interfaces.
- **Runtime**: Microsoft [.NET 10 Windows Desktop Runtime x64](https://dotnet.microsoft.com/download/dotnet/10.0).
- **CPU Tuning**: [PawnIO](https://pawnio.eu/) driver installed for low-level CPU MSR/power limit access.
- **RGB Tuning**: Predator Sense service components installed for Keyboard RGB control.

## Download & Running

1. Download the latest release.
2. Run `PreySense.exe` as administrator.

## Building From Source

Prerequisite: Install the [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0).

```powershell
dotnet build app\PreySense.csproj
```

## Technical Documentation

Detailed documentation on WMI calls, offsets, and lighting controls can be found under the `docs` folder:

- [Acer WMI Documentation](docs/acer_wmi_documentation.md)
- [Acer Service RGB Protocol](docs/acer_service_rgb.md)
- [Discovered WMI Offsets](docs/discovered_offsets.md)

### Registry State

User profiles, custom fan curves, and application states are stored under:

```text
HKCU\SOFTWARE\PreySense
```

## Contributing

Contributions are welcome, especially for:

- Hardware compatibility reports for additional Acer Predator models.
- WMI and AcerService packet documentation.
- Safe fallbacks for unsupported hardware configurations.
- UI/UX polish, styling, and accessibility.

Please include your **laptop model, BIOS version, Windows version, and GPU mode** when reporting issues or submitting changes.

## Disclaimer

PreySense controls low-level laptop hardware behavior (fans, power limits, clocks). Use it at your own risk. Incorrect settings may cause system instability or unexpected behavior.
