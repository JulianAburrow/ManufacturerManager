param (
    [string]$AppPoolName = "ManufacturerManagerDev",
    [string]$SiteName = "ManufacturerManagerDev",
    [string]$PublishDir = "C:\inetpub\ManufacturerManagerDev"
)

Import-Module WebAdministration
$ScriptFailed = $false
$LogPath = "$PublishDir\PostPublish.log"

function Log($message) {
    $timestamp = Get-Date -Format "yyyy-MM-dd HH:mm:ss"
    "$timestamp - $message" | Out-File -Append $LogPath
    Write-Host $message
}

Log "PostPublish started."

# Start App Pool
try {
    Start-WebAppPool -Name $AppPoolName
    Log "App Pool '$AppPoolName' started."
} catch {
    Log "ERROR starting App Pool: $_"
    $ScriptFailed = $true
}

# Start Website
try {
    Start-Website -Name $SiteName
    Log "Website '$SiteName' started."
} catch {
    Log "ERROR starting Website: $_"
    $ScriptFailed = $true
}

# Run xUnit tests
try {
    Log "Running xUnit tests..."
    dotnet test "C:\Julians Work\Study\ManufacturerManager\TestsUnit\TestsUnit.csproj"
    if ($LASTEXITCODE -ne 0) {
        Log "xUnit tests failed with exit code $LASTEXITCODE"
        $ScriptFailed = $true
    } else {
        Log "xUnit tests passed."
    }
} catch {
    Log "ERROR running xUnit tests: $_"
    $ScriptFailed = $true
}

$maxAttempts = 10
$attempt = 0
do {
    try {
        $response = Invoke-WebRequest -Uri "https://localhost:8090/" -UseBasicParsing -TimeoutSec 5
        if ($response.StatusCode -eq 200) {
            Write-Host "Site is ready."
            break
        }
    } catch {
        Write-Host "Waiting for site to respond..."
        Start-Sleep -Seconds 3
        $attempt++
    }
} while ($attempt -lt $maxAttempts)

if ($attempt -eq $maxAttempts) {
    Write-Host "Site did not respond in time."
}

# Set BASE_URL for Playwright
$env:BASE_URL = "https://localhost:8090"
Log "Set BASE_URL to $env:BASE_URL"

# Run Playwright tests
try {
    Log "Running Playwright tests..."
    dotnet test "C:\Julians Work\Study\ManufacturerManager\TestsPlaywright\TestsPlaywright.csproj"
    if ($LASTEXITCODE -ne 0) {
        Log "Playwright tests failed."
        $ScriptFailed = $true
    } else {
        Log "Playwright tests passed."
    }
} catch {
    Log "ERROR running Playwright tests: $_"
    $ScriptFailed = $true
}

# Remove BASE_URL
Remove-Item Env:\BASE_URL
Log "Removed BASE_URL environment variable."

# Clean up lingering processes
try {
    Get-Process testhost -ErrorAction SilentlyContinue | Stop-Process -Force
    Log "Stopped lingering testhost processes."
} catch {
    Log "ERROR cleaning up testhost: $_"
    $ScriptFailed = $true
}

Log "PostPublish completed."
if ($ScriptFailed) { exit 1 }

# Trigger Jenkins job after successful publish
$jenkinsUrl = "http://localhost:8080/job/ManufacturerManager-DeployDevToTest/buildWithParameters?DryRun=false"
$jenkinsUser = "julianaburrow"
$jenkinsToken = "113abbf294bdd2bc810f2893b954cc0deb"

$headers = @{
    Authorization = "Basic " + [Convert]::ToBase64String([Text.Encoding]::ASCII.GetBytes("${jenkinsUser}:${jenkinsToken}"))
}

try {
    Invoke-RestMethod -Uri $jenkinsUrl -Method Post -Headers $headers
    Write-Host "Triggered Jenkins deployment to Test successfully."
} catch {
    Write-Host "Failed to trigger Jenkins job: $_"
}