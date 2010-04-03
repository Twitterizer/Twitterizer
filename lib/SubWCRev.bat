REM @Echo Off

if "%PROCESSOR_ARCHITECTURE%" == "AMD64" goto 64bit
if "%PROCESSOR_ARCHITECTURE%" == "IA64" goto 64bit
if "%PROCESSOR_ARCHITEW6432%" == "AMD64" goto 64bit
if "%PROCESSOR_ARCHITEW6432%" == "IA64" goto 64bit

:32bit
echo "%~dp0SubWCRev_x32.exe" %1 %2 %3
"%~dp0SubWCRev_x32.exe" %1 %2 %3

goto end

:64bit
echo "%~dp0SubWCRev_x64.exe" %1 %2 %3
"%~dp0SubWCRev_x64.exe" %1 %2 %3

:end