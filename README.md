# Batch Scripter for AutoCAD

Batch Scripter applies one AutoCAD script to an ordered list of DWG or DWT files. The interface is intentionally focused: add drawings, enter or append script content, choose whether to save changes, and run.

## Features

- Add and remove multiple drawing files while preserving their displayed order.
- Enter script content directly or append an existing SCR/TXT file.
- Save changes with `QSAVE`, or close each drawing and explicitly discard changes.
- Preserve blank lines that represent Enter responses in command sequences.
- Ignore leading/trailing editor spacing and visual blank lines after AutoLISP expressions.
- Detect selected drawings that are already open or no longer exist.
- Follow the Windows light or dark application theme, including the title bar, app dialogs, scroll bars, footer, and social icons.

## Use

1. Install the Autodesk App Store package, or place the generated `BatchScripter.bundle` under `%PROGRAMFILES%\Autodesk\ApplicationPlugins`.
2. Start AutoCAD.
3. Run `OW:BatchScripter`.
4. Add drawings and script content.
5. Choose whether to save drawing changes.
6. Select **Run Script** and confirm.

Test every new script on copies of drawings before using it on production files.

## Blank-line behaviour

A blank line after a command response is preserved because it represents pressing Enter in AutoCAD:

```text
_.ERASE
_ALL

(command "_.REGEN")
```

Blank visual spacing immediately after an AutoLISP expression is ignored, and trailing blank lines are removed. This prevents an extra Enter from repeating or relaunching the previous command.

## Development

This release targets the installed AutoCAD 2026 API:

| AutoCAD | Target runtime |
| --- | --- |
| 2026 | .NET 8 |

Build the release and create the submission bundle with:

```powershell
.\build-app-store.ps1
```

See [APP-STORE-READINESS.md](APP-STORE-READINESS.md) for the implementation comparison, packaging details, and release QA checklist.

The build also requires the licensed Dazzle icon sources under `Batch Scripter\dazzleicons`. That complete folder is intentionally excluded from Git and must not be committed to the public repository.

## Local files

- Generated scripts: `%TEMP%\BatchScripter`
- Error logs: `%LOCALAPPDATA%\Batch Scripter\Logs`

## Licence

Released under the [MIT License](LICENSE.txt).

The GitHub and LinkedIn icons are licensed Dazzle Pro assets. See [THIRD-PARTY-NOTICES.txt](THIRD-PARTY-NOTICES.txt). The source assets are excluded from the public repository and embedded only in the compiled end product.

Autodesk and AutoCAD are registered trademarks of Autodesk, Inc. Batch Scripter is not affiliated with, endorsed by, sponsored by, or supported by Autodesk, Inc.
