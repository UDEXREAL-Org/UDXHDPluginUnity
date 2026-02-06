# UDXHDPluginUnity

A Unity Package Manager (UPM) compatible hand driving system designed for XR and interactive applications.

This package is distributed via Git and can be directly integrated into Unity projects using Unity Package Manager.

---

## Features

- Modular hand driving architecture  
- Runtime / Editor separation via Assembly Definitions  
- UPM-compliant package structure  
- Sample scenes for quick integration  
- Compatible with Unity 2021 LTS and later  

---

## Requirements

- Unity **2021.3 LTS** or newer  
- Unity **2022.x** supported  
- Optional dependencies depending on use case:
  - XR Interaction Toolkit

---

## Installation

### Install via Unity Package Manager (Git URL)

1. Open **Unity**
2. Navigate to **Window → Package Manager**
3. Click **“+” → Add package from git URL**
4. Enter: https://github.com/UDEXREAL-Org/UDXHDPluginUnity/commits/release_2.2.3

> Replace the version tag with the desired release.

---

## Package Structure

HandDriverUPM/
├─ Runtime/
│ ├─ Scripts/
│ └─ HandDriver.Runtime.asmdef
├─ Editor/
│ └─ HandDriver.Editor.asmdef
├─ Samples~
│ └─ DemoScene/
├─ ThirdParty/
├─ package.json
├─ README.md
└─ CHANGELOG.md


### Folder Overview

| Folder | Description |
|------|------------|
| Runtime | Core runtime logic |
| Editor | Editor-only tools and inspectors |
| Samples~ | Example scenes and usage demos |
| ThirdParty | Embedded third-party libraries |

---

## Samples

This package includes sample scenes to demonstrate basic setup and usage.

To import samples:

1. Open **Package Manager**
2. Select **HandDriver**
3. Click **Import** under the *Samples* section

---

## Dependencies

This package may include or reference the following libraries:

- **TouchSocket** – Runtime networking support (embedded if required)
- **NaughtyAttributes** – Editor-only inspector enhancements

> Editor-only dependencies are isolated and will not affect runtime builds.

---

## Versioning

This project follows **Semantic Versioning**:

Each released version corresponds to a Git tag and can be referenced directly by Unity Package Manager.

---

## Compatibility

| Unity Version | Status |
|--------------|--------|
| 2021.3 LTS | ✅ Supported |
| 2022.x | ✅ Supported |
| 2023.x | ⚠️ Not fully tested |

---

## Known Issues

- None currently reported.

Please ensure:
- Unity version compatibility
- Correct Git tag reference
- All required dependencies are resolved

---

## Contributing

Contributions are welcome.

If you wish to contribute:
1. Fork the repository
2. Create a feature branch
3. Submit a pull request with a clear description

---

## License

This project is licensed under the **MIT License**.

See the `LICENSE` file for details.

---

## Author
 
GitHub: [https://github.com/yourname](https://github.com/UDEXREAL-Org)

---

## Changelog

All notable changes are documented in [CHANGELOG.md](./CHANGELOG.md).
