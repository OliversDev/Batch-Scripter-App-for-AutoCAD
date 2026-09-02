param(
    [string]$AutoCAD2026Dir = "C:\Program Files\Autodesk\AutoCAD 2026",
    [string]$AutoCAD2027Dir = "C:\Program Files\Autodesk\AutoCAD 2027"
)

$ErrorActionPreference = "Stop"
$repositoryRoot = $PSScriptRoot
$project = Join-Path $repositoryRoot "Batch Scripter\Batch Scripter App for AutoCAD.csproj"
$bundleTemplate = Join-Path $repositoryRoot "AppStore\BatchScripter.bundle"
$artifactsRoot = Join-Path $repositoryRoot "AppStore\artifacts"
$bundle = Join-Path $artifactsRoot "BatchScripter.bundle"
$ribbonSource = Join-Path $repositoryRoot "AppStore\Ribbon\BatchScripter"
$outputZip = Join-Path $artifactsRoot "BatchScripter-2.0.0-Autodesk-Marketplace.zip"

function Assert-AutoCADApi {
    param(
        [string]$Version,
        [string]$Directory
    )

    foreach ($fileName in @("AcCoreMgd.dll", "AcDbMgd.dll", "AcMgd.dll")) {
        $apiFile = Join-Path $Directory $fileName
        if (-not (Test-Path -LiteralPath $apiFile -PathType Leaf)) {
            throw "AutoCAD $Version API file was not found: '$apiFile'."
        }
    }
}

Assert-AutoCADApi -Version "2026" -Directory $AutoCAD2026Dir
Assert-AutoCADApi -Version "2027" -Directory $AutoCAD2027Dir

if (-not (Test-Path -LiteralPath (Join-Path $bundleTemplate "PackageContents.xml") -PathType Leaf)) {
    throw "The bundle template is incomplete: '$bundleTemplate'."
}

New-Item -ItemType Directory -Path $artifactsRoot -Force | Out-Null

if (Test-Path -LiteralPath $bundle) {
    Remove-Item -LiteralPath $bundle -Recurse -Force
}

Copy-Item -LiteralPath $bundleTemplate -Destination $bundle -Recurse -Force

dotnet build $project `
    --configuration Release `
    --framework net8.0-windows `
    --property:AutoCAD2026Dir="$AutoCAD2026Dir"

if ($LASTEXITCODE -ne 0) {
    throw "The AutoCAD 2026 / .NET 8 build failed."
}

dotnet build $project `
    --configuration Release `
    --framework net10.0-windows `
    --property:AutoCAD2027Dir="$AutoCAD2027Dir"

if ($LASTEXITCODE -ne 0) {
    throw "The AutoCAD 2027 / .NET 10 build failed."
}

foreach ($version in @("2026", "2027")) {
    $sourceDll = Join-Path $repositoryRoot "Batch Scripter\bin\Release\AutoCAD$version\Batch Scripter App for AutoCAD.dll"
    $destinationDir = Join-Path $bundle "Contents\$version"
    New-Item -ItemType Directory -Path $destinationDir -Force | Out-Null
    Copy-Item -LiteralPath $sourceDll -Destination (Join-Path $destinationDir "Batch Scripter App for AutoCAD.dll") -Force
}

$resourcesDir = Join-Path $bundle "Contents\Resources"
$helpDir = Join-Path $bundle "Contents\Help"
New-Item -ItemType Directory -Path $resourcesDir -Force | Out-Null
New-Item -ItemType Directory -Path $helpDir -Force | Out-Null

$requiredRibbonFiles = @(
    "[Content_Types].xml",
    "_rels\.rels",
    "Header.cui",
    "MenuGroup.cui",
    "Menu_Package_Info.xml",
    "RibbonRoot.cui",
    "AcceleratorRoot.cui",
    "DigitizerButtonRoot.cui",
    "DoubleClickRoot.cui",
    "ImageMenuRoot.cui",
    "MouseButtonRoot.cui",
    "OverrideRoot.cui",
    "PopMenuRoot.cui",
    "QuickAccessToolbarRoot.cui",
    "QuickPropertiesRoot.cui",
    "RolloverTooltipRoot.cui",
    "ScreenMenuRoot.cui",
    "TabletMenuRoot.cui",
    "ToolbarRoot.cui",
    "WorkspaceRoot.cui",
    "BatchScripter16.png",
    "BatchScripter32.png"
)

foreach ($fileName in $requiredRibbonFiles) {
    $ribbonFile = Join-Path $ribbonSource $fileName
    if (-not (Test-Path -LiteralPath $ribbonFile -PathType Leaf)) {
        throw "Required ribbon source file was not found: '$ribbonFile'."
    }
}

$ribbonWorkRoot = Join-Path ([System.IO.Path]::GetTempPath()) ("BatchScripterCuix_" + [Guid]::NewGuid().ToString("N").Substring(0, 8))
$ribbonPackageRoot = Join-Path $ribbonWorkRoot "Package"
$ribbonZip = Join-Path $ribbonWorkRoot "BatchScripter.zip"
$ribbonCuix = Join-Path $resourcesDir "BatchScripter.cuix"

try {
    New-Item -ItemType Directory -Path $ribbonPackageRoot -Force | Out-Null

    foreach ($fileName in $requiredRibbonFiles) {
        $sourceFile = Join-Path $ribbonSource $fileName
        $destinationFile = Join-Path $ribbonPackageRoot $fileName
        $destinationFolder = Split-Path -Parent $destinationFile
        New-Item -ItemType Directory -Path $destinationFolder -Force | Out-Null
        Copy-Item -LiteralPath $sourceFile -Destination $destinationFile -Force
    }

    Compress-Archive -Path (Join-Path $ribbonPackageRoot "*") -DestinationPath $ribbonZip -CompressionLevel Optimal
    Move-Item -LiteralPath $ribbonZip -Destination $ribbonCuix -Force
}
finally {
    if (Test-Path -LiteralPath $ribbonWorkRoot) {
        Remove-Item -LiteralPath $ribbonWorkRoot -Recurse -Force
    }
}

Copy-Item `
    -LiteralPath (Join-Path $repositoryRoot "Batch Scripter\Logo_BW_Small.ico") `
    -Destination (Join-Path $resourcesDir "BatchScripter.ico") `
    -Force

Copy-Item `
    -LiteralPath (Join-Path $repositoryRoot "LICENSE.txt") `
    -Destination (Join-Path $helpDir "LICENSE.txt") `
    -Force

Copy-Item `
    -LiteralPath (Join-Path $repositoryRoot "THIRD-PARTY-NOTICES.txt") `
    -Destination (Join-Path $helpDir "THIRD-PARTY-NOTICES.txt") `
    -Force

[xml](Get-Content -LiteralPath (Join-Path $bundle "PackageContents.xml") -Raw) | Out-Null

if (Test-Path -LiteralPath $outputZip) {
    Remove-Item -LiteralPath $outputZip -Force
}

Compress-Archive -Path $bundle -DestinationPath $outputZip -CompressionLevel Optimal
Write-Host "Created $outputZip"
