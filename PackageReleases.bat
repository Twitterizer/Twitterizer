@Echo off

set twitterizerver=
set /P twitterizerver=What is the version number? %=%

FOR /f %%a IN ('dir /b /ad .\Release\') DO (
PUSHD "Release\%%a\" 
"%ProgramFiles%\7-Zip\7z.exe" a -r "..\%%a-%twitterizerver%.zip" *.*
POPD
)