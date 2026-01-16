param (
    [string]$AppPoolName = "ManufacturerManagerDev",
    [string]$SiteName = "ManufacturerManagerDev"
)

Import-Module WebAdministration
$ScriptFailed = $false
$LogPath = "C:\inetpub\Websites\ManufacturerManager\Dev\PrePublish.log"

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

# Kill only the ManufacturerDev worker process
try {
    $workerProcesses = Get-ChildItem "IIS:\AppPools\$AppPoolName\workerProcesses"

    if ($workerProcesses) {
        foreach ($wp in $workerProcesses) {
            Stop-Process -Id $wp.ProcessId -Force
            Log "Stopped w3wp for App Pool '$AppPoolName' (PID $($wp.ProcessId))."
        }
    } else {
        Log "No worker process running for App Pool '$AppPoolName'."
    }
}
catch {
    Log "ERROR stopping ManufacturerDev w3wp: $_"
    $ScriptFailed = $true
}

Log "PrePublish completed."
if ($ScriptFailed) { exit 1 }