# Batch Scripter for AutoCAD

Batch Scripter is a focused AutoCAD add-in for applying one command script to an ordered collection of drawing or template files. It provides a straightforward interface for selecting files, entering or importing script content, choosing whether changes should be saved, and running the batch.

## Features

- Process multiple DWG and DWT files in the displayed order.
- Enter script content directly or append an existing SCR or TXT file.
- Save each processed drawing or explicitly discard its changes.
- Preserve blank lines that represent Enter responses in AutoCAD commands.
- Remove leading, trailing, and redundant editor spacing that could repeat a command unintentionally.
- Detect missing files and selected drawings that are already open.
- Follow the Windows light or dark application theme.

## Requirements

- AutoCAD 2026 or AutoCAD 2027 for Windows, 64-bit

## Installation

The Autodesk Design and Make Marketplace package installs and registers the application automatically.

For local bundle testing, place `BatchScripter.bundle` under:

```text
C:\Program Files\Autodesk\ApplicationPlugins
```

Restart AutoCAD. Open the **Plug-Ins** ribbon tab and select **Batch Scripter**, or enter the following command:

```text
OW:BatchScripter
```

Developers can also load a compiled DLL directly with AutoCAD's `NETLOAD` command.

## Using Batch Scripter

1. Select **Add Drawings** and choose one or more DWG or DWT files.
2. Enter script content or select **Add Script** to append an SCR or TXT file.
3. Choose whether drawing changes should be saved.
4. Select **Run Script**, review the confirmation, and continue.

Always test new scripts on copies of drawings before using them on production files.

## Script formatting

Blank lines can be meaningful in an AutoCAD script because they represent pressing Enter. For example:

```text
_.ERASE
_ALL

(command "_.REGEN")
```

Batch Scripter preserves required blank responses while removing trailing editor whitespace and redundant blank lines immediately following AutoLISP expressions.

## Privacy

Batch Scripter does not collect or transmit personal information, drawing information, script content, or usage analytics. Processing occurs locally on the user's computer.

## Development

The project builds two release-specific assemblies from the same project:

- AutoCAD 2026 using .NET 8
- AutoCAD 2027 using .NET 10

Source builds require the .NET 8 and .NET 10 SDKs and the managed API assemblies installed with both AutoCAD releases. Building the solution in Visual Studio compiles both targets. To build both targets and prepare the Marketplace bundle from a PowerShell developer prompt, run:

```powershell
.\build-app-store.ps1
```

If the source was downloaded as a ZIP, use **Properties > Unblock** on the ZIP before extracting it. For an already extracted trusted copy, close Visual Studio and run the following command from the repository root to remove Windows' downloaded-file marker:

```powershell
Get-ChildItem -Recurse -File | Unblock-File
```

The default API locations are `C:\Program Files\Autodesk\AutoCAD 2026` and `C:\Program Files\Autodesk\AutoCAD 2027`. Custom locations can be passed to the packaging script:

```powershell
.\build-app-store.ps1 `
  -AutoCAD2026Dir "D:\Autodesk\AutoCAD 2026" `
  -AutoCAD2027Dir "D:\Autodesk\AutoCAD 2027"
```

The compiled assemblies are written to:

```text
Batch Scripter\bin\Release\AutoCAD2026
Batch Scripter\bin\Release\AutoCAD2027
```

The packaging script creates the required partial CUIX ribbon file from `AppStore\Ribbon\BatchScripter` and stages the complete bundle under:

```text
AppStore\artifacts\BatchScripter.bundle
```

The Marketplace upload archive is written to:

```text
AppStore\artifacts\BatchScripter-2.0.0-Autodesk-Marketplace.zip
```

The ignored `artifacts` folder keeps generated DLLs, CUIX files, copied notices, and submission archives out of the source tree.

Marketplace artwork is maintained under `AppStore\Assets`, including the required transparent 120 x 120 PNG. The ribbon uses a separate simplified icon designed specifically for AutoCAD's 16 x 16 and 32 x 32 display sizes.

Certain licensed visual assets required for local source builds are intentionally excluded from the public repository.

## Licence

Batch Scripter source code is released under the [MIT License](LICENSE.txt).

### Third-party notices

**Dazzle Pro Icons**  
Copyright (c) Dazzle-UI, Inc. All Rights Reserved.  
License: Purchased Dazzle-UI Professional Solo License

Batch Scripter uses licensed Dazzle Pro icons and symbols as part of the Batch Scripter end product. The Dazzle assets are proprietary and may not be copied, extracted, reproduced, redistributed, sublicensed, resold, or reused from this site or application.

<https://dazzleui.pro/>

Autodesk and AutoCAD are registered trademarks of Autodesk, Inc. Batch Scripter is not affiliated with, endorsed by, sponsored by, or supported by Autodesk, Inc.
