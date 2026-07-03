# Enable Acer GPU Automatic Overclocking in Turbo Mode
# Restores the original model INI file contents from backup.

$cfg = "C:\Windows\System32\DriverStore\FileRepository\predatorservice.inf_amd64_c634eb8e856fb962\Predator PHN16-71.ini"
$backup = "$PSScriptRoot\Predator PHN16-71.ini.backup"

if (Test-Path $backup) {
    if (Test-Path $cfg) {
        Write-Host "Restoring original configuration from backup..."
        Copy-Item -Path $backup -Destination $cfg -Force
        
        Write-Host "Restarting AASSvc..."
        Restart-Service -Name AASSvc -Force
        Write-Host "Successfully re-enabled automatic clock offsets!"
    } else {
        Write-Warning "Target configuration file not found at: $cfg"
    }
} else {
    Write-Warning "Backup file not found at: $backup. Cannot restore original settings."
}
pause
