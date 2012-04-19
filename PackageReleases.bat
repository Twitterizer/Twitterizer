@Echo off

REM Ask for the version number of the release
set twitterizerver=
set /P twitterizerver=What is the version number? %=%

REM Loop through each directory (one for each package) and zip them up
FOR /f %%a IN ('dir /b /ad .\Release\') DO (
IF EXIST "%%a-%twitterizerver%.zip" del /Q "%%a-%twitterizerver%.zip"
PUSHD "Release\%%a" 
"%ProgramFiles%\7-Zip\7z.exe" a -r "..\%%a-%twitterizerver%.zip" *.*
POPD
)

REM Check if the source directory is there from a previous failed attempt and delete it
IF EXIST "Release\Twitterizer%twitterizerver%-source" RD /S /Q "Release\Twitterizer%twitterizerver%-source"

REM Make our source directory
mkdir "Release\Twitterizer%twitterizerver%-source"

REM Export our working copy to the source directory
svn export --force . "Release\Twitterizer%twitterizerver%-source"

REM Zip up the source code
PUSHD "Release\Twitterizer%twitterizerver%-source"
"%ProgramFiles%\7-Zip\7z.exe" a -r "..\Twitterizer%twitterizerver%-source.zip" *.*
POPD

REM Cleanup
RD /S /Q "Release\Twitterizer%twitterizerver%-source"

REM Prepare example application package
IF EXIST "Release\Twitterizer%twitterizerver%-examples" RD /S /Q "Release\Twitterizer%twitterizerver%-examples"
svn export "ExampleApplications" "Release\Twitterizer%twitterizerver%-examples"
PUSHD "Release\Twitterizer%twitterizerver%-examples"
"%ProgramFiles%\7-Zip\7z.exe" a -r "..\Twitterizer%twitterizerver%-examples.zip" *.*
POPD