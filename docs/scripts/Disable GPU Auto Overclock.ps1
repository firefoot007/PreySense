# Disable Acer GPU Automatic Overclocking in Turbo Mode
# Backs up the model INI file and wipes its contents to prevent the Acer service from forcing offsets.

$cfg = "C:\Windows\System32\DriverStore\FileRepository\predatorservice.inf_amd64_c634eb8e856fb962\Predator PHN16-71.ini"
$backup = "$PSScriptRoot\Predator PHN16-71.ini.backup"

if (Test-Path $cfg) {
    Write-Host "Taking ownership of $cfg..."
    takeown /f $cfg
    icacls $cfg /grant Administrators:F

    Write-Host "Creating backup to $backup..."
    Copy-Item -Path $cfg -Destination $backup -Force

    Write-Host "Wiping file contents to disable automatic overclock..."
    Clear-Content $cfg

    Write-Host "Restarting AASSvc..."
    Restart-Service -Name AASSvc -Force
    Write-Host "Successfully disabled automatic clock offsets!"
} else {
    Write-Warning "Configuration file not found at: $cfg"
}
pause
