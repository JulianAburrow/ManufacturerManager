# PostPublish.ps1

param (
    [string]$PublishDir = "C:\inetpub\wwwroot\ManufacturerManager"
)

function Wait-ForApp {
    param (
        [string]$url,
        [int]$maxRetries = 6,
        [int]$delaySeconds = 10
    )

    $attempt = 0
    while ($attempt -lt $maxRetries) {
        try {
            $response = Invoke-WebRequest -Uri $url -UseBasicParsing -TimeoutSec 5
            if ($response.StatusCode -eq 200) {
                Write-Host "App is responding at $url"
                return $true
            }
        } catch {
            Write-Host "Waiting for app to respond... (attempt $($attempt + 1))"
        }
        Start-Sleep -Seconds $delaySeconds
        $attempt++
    }

    Write-Error "App did not respond at $url after $maxRetries attempts."
    exit 1
}

Write-Host "Restarting IIS App Pool after publish..."
Import-Module WebAdministration
Start-WebAppPool -Name "ManufacturerManager"

Write-Host "Validating published app at $PublishDir..."

$timestamp = Get-Date -Format "yyyyMMdd-HHmmss"
$resultsDir = "C:\inetpub\wwwroot\ManufacturerManager\TestResults\$timestamp"

if (-not (Test-Path $resultsDir)) {
    Write-Host "Creating test results directory..."
    New-Item -ItemType Directory -Path $resultsDir -Force | Out-Null
}

# Run xUnit tests
Write-Host "Running xUnit tests..."
dotnet test "C:\Julians Work\Study\ManufacturerManager\TestsUnit\TestsUnit.csproj" `
    --logger trx `
    --results-directory $resultsDir

if ($LASTEXITCODE -ne 0) {
    Write-Error "xUnit tests failed with exit code $LASTEXITCODE"
    exit 1
}

# Run Playwright tests
Write-Host "Running Playwright .NET tests..."

# Optional: Set base URL for IIS-hosted app
$env:BASE_URL = "https://localhost:8080/"

Wait-ForApp -url $env:BASE_URL

# Run tests
dotnet test "C:\Julians Work\Study\ManufacturerManager\TestsPlaywright\TestsPlaywright.csproj" --no-build

# Check result
if ($LASTEXITCODE -ne 0) {
    Write-Error "Playwright tests failed."
    exit 1
}
# Log success
Write-Host "All tests passed. Ready for promotion."

# Optional: Trigger TeamCity or Octopus
# You could call a REST API, CLI tool, or drop a file to signal readiness