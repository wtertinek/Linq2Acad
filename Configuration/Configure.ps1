$folder = Read-Host 'Please enter your AutoCAD installation folder'

if (Test-Path $folder)
{
  (Get-Content -Encoding UTF8 Template.csproj.user) | ForEach-Object { $_ -replace "{AcadRootDir}", "$folder" } | Set-Content -Encoding UTF8 ..\Linq2Acad\Linq2Acad.csproj.user
  (Get-Content -Encoding UTF8 Template.csproj.user) | ForEach-Object { $_ -replace "{AcadRootDir}", "$folder" } | Set-Content -Encoding UTF8 ..\Linq2Acad.Examples\Linq2Acad.Examples.csproj.user
  (Get-Content -Encoding UTF8 Template.csproj.user) | ForEach-Object { $_ -replace "{AcadRootDir}", "$folder" } | Set-Content -Encoding UTF8 ..\Linq2Acad.Tests\Linq2Acad.Tests.csproj.user
  (Get-Content -Encoding UTF8 Template.csproj.user) | ForEach-Object { $_ -replace "{AcadRootDir}", "$folder" } | Set-Content -Encoding UTF8 ..\Linq2Acad.Tests.Acad\Linq2Acad.Tests.Acad.csproj.user
  (Get-Content -Encoding UTF8 AcadTestRunner.dll.config) | ForEach-Object { $_ -replace "{AcadRootDir}", "$folder" } | Set-Content -Encoding UTF8 ..\AcadTestRunner\AcadTestRunner.dll.config
  
  Write-Host 'AutoCAD reference path set to' $folder
}
else
{
  Write-Host $folder 'does not exist'
}