set AUTOCAD_VERSION=%1
set VERSION=%2

set NUGET_RESTORE_MSBUILD_ARGS=/p:Configuration=Release_%AUTOCAD_VERSION%
nuget restore %~dp0.\..\src\Linq2Acad.sln
copy /y %~dp0.\Linq2Acad_%AUTOCAD_VERSION%.nuspec %~dp0.\..\src\Linq2Acad\Linq2Acad.nuspec
nuget pack -Build -Properties Configuration=Release_%AUTOCAD_VERSION% -Version %VERSION% -OutputDirectory %~dp0.\..\packages %~dp0.\..\src\Linq2Acad\Linq2Acad.csproj
del %~dp0.\..\src\Linq2Acad\Linq2Acad.nuspec