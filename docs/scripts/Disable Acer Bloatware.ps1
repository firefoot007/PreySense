$services = @(
    "AcerCCAgentSvis", 
    "AcerDeviceEnablingServiceV2", 
    "AcerDIAgentSvis", 
    "AICO_svc", 
    "AICO2_svc"
)

foreach ($service in $services) {
    if (Get-Service -Name $service -ErrorAction SilentlyContinue) {
        Write-Host "Stopping and disabling $service..."
        Stop-Service -Name $service -Force -ErrorAction SilentlyContinue
        Set-Service -Name $service -StartupType Disabled
    }
}
pause