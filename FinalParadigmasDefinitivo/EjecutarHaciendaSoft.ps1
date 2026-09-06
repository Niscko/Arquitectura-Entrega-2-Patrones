Write-Host "===================================================" -ForegroundColor Cyan
Write-Host "         HACIENDASOFT - APLICACIÓN DE ARRANQUE     " -ForegroundColor Yellow
Write-Host "===================================================" -ForegroundColor Cyan
Write-Host ""

$sdkCheck = & dotnet --list-sdks 2>$null

if (-not $sdkCheck) {
    Write-Host ""
    Write-Host "[!] ATENCIÓN: Se requiere instalar el .NET 8.0 SDK." -ForegroundColor Red
    Write-Host "    El SDK es el compilador necesario para ejecutar la aplicación web." -ForegroundColor Yellow
    Write-Host ""
    Write-Host "Página oficial de descarga directa de Microsoft:" -ForegroundColor White
    Write-Host "👉 https://dotnet.microsoft.com/download/dotnet/8.0" -ForegroundColor Cyan
    Write-Host ""
    
    $openUrl = Read-Host "¿Deseas abrir la página de descarga oficial en tu navegador ahora mismo? (S/N)"
    if ($openUrl -eq 'S' -or $openUrl -eq 's' -or $openUrl -eq 'Si' -or $openUrl -eq 'si') {
        Start-Process "https://dotnet.microsoft.com/download/dotnet/8.0"
    }
    exit
}

Write-Host "[OK] SDK detectado correctamente." -ForegroundColor Green
Write-Host ""
Write-Host "Compilando e iniciando servidor web de HaciendaSoft..." -ForegroundColor Cyan
Write-Host ""

Set-Location -Path "$PSScriptRoot\p_mvcHacienda"
dotnet run