@echo off
cls

if not exist .paket (
  @echo "Installing Paket"
  mkdir .paket
  c:\extras\curl https://github.com/fsprojects/Paket/releases/download/1.4.0/paket.bootstrapper.exe -L --insecure -o .paket\paket.bootstrapper.exe
  
  .paket\paket.bootstrapper.exe prerelease
  if errorlevel 1 (
    exit /b %errorlevel%
  )
)

if not exist paket.lock (
  @echo "Installing dependencies"
  .paket\paket.exe install
) else (
  @echo "Restoring dependencies"
  .paket\paket.exe restore
)

rem curl is best, but if not available we could revert to native powershell a al...
rem from a good example here: https://raw.githubusercontent.com/technomancy/leiningen/stable/bin/lein.bat
rem call powershell -? >nul 2>&1
rem if NOT ERRORLEVEL 1 (
rem     powershell -Command "& {param($a,$f) (new-object System.Net.WebClient).DownloadFile($a, $f)}" ""%2"" ""%1""
rem     goto EOF
rem )