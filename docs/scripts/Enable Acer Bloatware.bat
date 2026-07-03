$services = @(
    "AcerCCAgentSvis", 
    "AcerDeviceEnablingServiceV2", 
    "AcerDIAgentSvis", 
    "AASSvc", 
    "ASMSvc", 
    "AICO_svc", 
    "AICO2_svc"
)

foreach ($service in $services) {
    if (Get-Service -Name $service -ErrorAction SilentlyContinue) {
        Write-Host "Starting and enabling $service..."
        Set-Service -Name $service -StartupType Automatic
        Start-Service -Name $service -ErrorAction SilentlyContinue
    }
}
Write-Host "Reboot computer to apply changes"
pause