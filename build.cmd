@echo off
cls

.paket\paket.bootstrapper.exe
if errorlevel 1 (
  exit /b %errorlevel%
)

.paket\paket.exe restore --force
if errorlevel 1 (
  exit /b %errorlevel%
)