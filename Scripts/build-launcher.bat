cd ..\Launcher\src\SqueezeLauncher

dotnet build -r win10-x64

copy bin\Debug\netcoreapp1.0\win10-x64\* ..\..\..\Builds\TestInstall

pause