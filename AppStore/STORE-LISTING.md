# Batch Scripter for AutoCAD

## Short description

Run an AutoCAD script across an ordered list of drawings with clear save or discard control.

## Description

Batch Scripter is a focused AutoCAD productivity tool for applying the same SCR or text-based command sequence to multiple DWG or DWT files. Add drawings, enter or append a script, choose whether changes should be saved, and run the batch.

The app preserves drawing order and supports command scripts that require a blank line as an Enter response. It also removes trailing editor whitespace that can unintentionally repeat the previous AutoCAD command. Before execution, Batch Scripter checks for missing files and drawings that are already open.

The interface follows AutoCAD's light or dark theme and intentionally stays simple: drawing selection, script content, save control, and one Run action.

## Commands

- `OW:BatchScripter` — opens Batch Scripter.

## Supported products

- AutoCAD 2026
- Windows 64-bit

## Trial and safety note

Always test a new script on copies of drawings. AutoCAD scripts execute commands exactly as supplied, and saved changes may not be reversible after the drawings close.

## Privacy

Batch Scripter does not collect or transmit personal information or analytics. Drawing paths, script content, temporary scripts, and error logs remain on the user's computer.
