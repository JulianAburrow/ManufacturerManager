param (
    [string]$AppPoolName = "ManufacturerManager",
    [string]$SiteName = "ManufacturerManager"
)

Import-Module WebAdministration
$ScriptFailed = $false
$LogPath = "C:\inetpub\ManufacturerManager\PrePublish.log"

function Log($message) {
    $timestamp = Get-Date -Format "yyyy-MM-dd HH:mm:ss"
    "$timestamp - $message" | Out-File -Append $LogPath
    Write-Host $message
}

Log "PrePublish started."

# Stop App Pool
try {
    $state = (Get-WebAppPoolState -Name $AppPoolName).Value
    if ($state -eq "Started") {
        Stop-WebAppPool -Name $AppPoolName
        Log "App Pool '$AppPoolName' stopped."
    } else {
        Log "App Pool '$AppPoolName' already stopped."
    }
} catch {
    Log "ERROR stopping App Pool: $_"
    $ScriptFailed = $true
}

# Stop Website
try {
    $siteState = (Get-WebSiteState -Name $SiteName).Value
    if ($siteState -eq "Started") {
        Stop-Website -Name $SiteName
        Log "Website '$SiteName' stopped."
    } else {
        Log "Website '$SiteName' already stopped."
    }
} catch {
    Log "ERROR stopping Website: $_"
    $ScriptFailed = $true
}

# Kill lingering w3wp
try {
    Get-Process w3wp -ErrorAction SilentlyContinue | Stop-Process -Force
    Log "Stopped lingering w3wp processes."
} catch {
    Log "ERROR stopping w3wp: $_"
    $ScriptFailed = $true
}

Log "PrePublish completed."
if ($ScriptFailed) { exit 1 }