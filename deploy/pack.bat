set VERSION=%1

rem AutoCAD 2015
set NUGET_RESTORE_MSBUILD_ARGS=/p:Configuration=Release_2015
nuget restore ..\Linq2Acad.sln
copy /y Linq2Acad_2015.nuspec Linq2Acad.nuspec
nuget pack -Build -Properties Configuration=Release_2015 -Version %VERSION% -OutputDirectory ..\..\packages

rem AutoCAD 2016
set NUGET_RESTORE_MSBUILD_ARGS=/p:Configuration=Release_2016
nuget restore ..\Linq2Acad.sln
copy /y Linq2Acad_2016.nuspec Linq2Acad.nuspec
nuget pack -Build -Properties Configuration=Release_2016 -Version %VERSION% -OutputDirectory ..\..\packages

rem AutoCAD 2017
set NUGET_RESTORE_MSBUILD_ARGS=/p:Configuration=Release_2017
nuget restore ..\Linq2Acad.sln
copy /y Linq2Acad_2017.nuspec Linq2Acad.nuspec
nuget pack -Build -Properties Configuration=Release_2017 -Version %VERSION% -OutputDirectory ..\..\packages

rem AutoCAD 2018
set NUGET_RESTORE_MSBUILD_ARGS=/p:Configuration=Release_2018
nuget restore ..\Linq2Acad.sln
copy /y Linq2Acad_2018.nuspec Linq2Acad.nuspec
nuget pack -Build -Properties Configuration=Release_2018 -Version %VERSION% -OutputDirectory ..\..\packages

rem AutoCAD 2019
set NUGET_RESTORE_MSBUILD_ARGS=/p:Configuration=Release_2019
nuget restore ..\Linq2Acad.sln
copy /y Linq2Acad_2019.nuspec Linq2Acad.nuspec
nuget pack -Build -Properties Configuration=Release_2019 -Version %VERSION% -OutputDirectory ..\..\packages

rem AutoCAD 2020
set NUGET_RESTORE_MSBUILD_ARGS=/p:Configuration=Release_2020
nuget restore ..\Linq2Acad.sln
copy /y Linq2Acad_2020.nuspec Linq2Acad.nuspec
nuget pack -Build -Properties Configuration=Release_2020 -Version %VERSION% -OutputDirectory ..\..\packages

rem AutoCAD 2021
set NUGET_RESTORE_MSBUILD_ARGS=/p:Configuration=Release_2021
nuget restore ..\Linq2Acad.sln
copy /y Linq2Acad_2021.nuspec Linq2Acad.nuspec
nuget pack -Build -Properties Configuration=Release_2021 -Version %VERSION% -OutputDirectory ..\..\packages

rem AutoCAD 2022
set NUGET_RESTORE_MSBUILD_ARGS=/p:Configuration=Release_2022
nuget restore ..\Linq2Acad.sln
copy /y Linq2Acad_2022.nuspec Linq2Acad.nuspec
nuget pack -Build -Properties Configuration=Release_2022 -Version %VERSION% -OutputDirectory ..\..\packages

rem AutoCAD 2023
set NUGET_RESTORE_MSBUILD_ARGS=/p:Configuration=Release_2023
nuget restore ..\Linq2Acad.sln
copy /y Linq2Acad_2023.nuspec Linq2Acad.nuspec
nuget pack -Build -Properties Configuration=Release_2023 -Version %VERSION% -OutputDirectory ..\..\packages