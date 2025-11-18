<#
.SYNOPSIS
  Simple template initializer for mod projects.
.DESCRIPTION
  Replaces placeholders in filenames and file contents:
    - "NameOfMod"
    - "NameOfTeam"
    - "github-account-name"
    - "github-repository-name"
  Also replaces the project description in .csproj and Thunderstore/manifest.json.
#>

# Prompt for values
$modName        = Read-Host "Enter mod name (replaces 'NameOfMod')"
$modDescription = Read-Host "Enter mod description"
$teamName       = Read-Host "Enter team name (replaces 'NameOfTeam')"
$githubAccount  = Read-Host "Enter GitHub account name"
$githubRepo     = Read-Host "Enter GitHub repository name"

# Build replacement map
$replacements = @{
    "NameOfMod"              = $modName
    "NameOfTeam"             = $teamName
    "github-account-name"    = $githubAccount
    "github-repository-name" = $githubRepo
}

# Try to load .gitignore patterns (optional)
$ignorePatterns = @()
if (Test-Path ".gitignore") {
    $ignorePatterns = Get-Content ".gitignore" | Where-Object {
        $_ -and ($_ -notmatch '^\s*#')
    }
}

function ShouldIgnore($path) {
    foreach ($pattern in $ignorePatterns) {
        $glob = $pattern.Trim().Replace("/", "\")
        if (-not $glob) { continue }
        if ($path -like "*$glob*") { return $true }
    }
    return $false
}

# --- Replace file contents ---
Write-Host "`nUpdating file contents..."
Get-ChildItem -Recurse -File | ForEach-Object {
    $file = $_.FullName
    if (ShouldIgnore $file) { return }
    if ($file -eq $PSCommandPath) { return }

    $text = Get-Content $file -Raw
    $updated = $false

    foreach ($k in $replacements.Keys) {
        if ($text -match [regex]::Escape($k)) {
            $text = $text -replace [regex]::Escape($k), $replacements[$k]
            $updated = $true
        }
    }

    # Special case: project description
    if ($file -match "\\(NameOfMod|$modName)\\(NameOfMod|$modName)\.csproj$" -or
        $file -match "Thunderstore\\manifest\.json$") {
        $text = $text -replace '(?<=<Description>)(.*?)(?=</Description>)', $modDescription
        $text = $text -replace '(?<=("description"\s*:\s*"))(.*?)(?=")', $modDescriptio
        $updated = $true
    }

    if ($updated) {
        Set-Content -Path $file -Value $text -NoNewline
        Write-Host "  Updated $file"
    }
}

# --- Rename files and folders ---
Write-Host "`nRenaming files and folders..."
Get-ChildItem -Recurse | Sort-Object { $_.FullName.Split('\').Count } -Descending | ForEach-Object {
    $item = $_
    if (ShouldIgnore $item.FullName) { return }
    if ($item.FullName -eq $PSCommandPath) { return }

    if ($item.Name -match "NameOfMod") {
        $newName = $item.Name -replace "NameOfMod", $modName
        if ($null -ne $item.DirectoryName) {
            $newPath = Join-Path $item.DirectoryName $newName
            Rename-Item -Path $item.FullName -NewName $newName
            Write-Host "  Renamed: $($item.FullName) → $newPath"
        } else {
            Rename-Item -Path $item.FullName -NewName $newName
            Write-Host "  Renamed: $($item.FullName) → $newName"
        }
    }
}

Write-Host "`n✅ Template initialized successfully!"
