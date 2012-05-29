@echo off
lib\nuget.exe Update -self
lib\nuget.exe pack Twitterizer2\Twitterizer2.csproj -Prop Configuration=Release
lib\nuget.exe pack Twitterizer2\Twitterizer2.csproj -Prop Configuration=Release -Symbols