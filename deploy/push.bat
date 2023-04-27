set AUTOCAD_VERSION=%1
set VERSION=%2
set API_KEY=%3

nuget push %~dp0.\..\packages\Linq2Acad-%AUTOCAD_VERSION%.%VERSION%.nupkg %API_KEY% -Source https://api.nuget.org/v3/index.json