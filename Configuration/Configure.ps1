$folder = Read-Host 'Please enter your AutoCAD installation folder'

if (Test-Path $folder)
{
  function Update-File ($targetFileName)
  {
    if (Test-Path $targetFileName)
    {
      $srcFileName = $targetFileName
    }
    else
    {
      $srcFileName = "Template.csproj.user"
    }
    
    (Get-Content -Encoding UTF8 $srcFileName) | ForEach-Object { $_ -replace "{AcadRootDir}", "$folder" } | Set-Content -Encoding UTF8 $targetFileName
  }
  
  Update-File ..\AcadTestRunner\AcadTestRunner.csproj.user
  Update-File ..\AcadTestRunner.Assert\AcadTestRunner.Assert.csproj.user
  Update-File ..\Linq2Acad\Linq2Acad.csproj.user
  Update-File ..\Linq2Acad.Examples\Linq2Acad.Examples.csproj.user
  Update-File ..\Linq2Acad.Tests\Linq2Acad.Tests.csproj.user
  (Get-Content -Encoding UTF8 AcadTestRunner.dll.config) | ForEach-Object { $_ -replace "{AcadRootDir}", "$folder" } | Set-Content -Encoding UTF8 ..\AcadTestRunner\AcadTestRunner.dll.config
  
  Write-Host 'AutoCAD reference path set to' $folder
}
else
{
  Write-Host $folder 'does not exist'
}