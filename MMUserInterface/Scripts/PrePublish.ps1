# PrePublish.ps1

Write-Host "Stopping IIS App Pool before publish..."
Import-Module WebAdministration
Stop-WebAppPool -Name "ManufacturerManager"