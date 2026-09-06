@echo off
title HaciendaSoft - Aplicacion de Arranque
color 0A
cls
echo ===================================================
echo             HACIENDASOFT - LANZADOR
echo ===================================================
echo.

powershell -NoProfile -ExecutionPolicy Bypass -File "%~dp0EjecutarHaciendaSoft.ps1"

pause