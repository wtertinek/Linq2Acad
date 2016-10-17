function Update-File ($targetFileName, $tempalteFileName, $variableName, $value)
{
  if (Test-Path $targetFileName)
  {
    $srcFileName = $targetFileName
  }
  else
  {
    $srcFileName = $tempalteFileName
  }
  
  (Get-Content -Encoding UTF8 $srcFileName) | ForEach-Object { $_ -replace $variableName, $value } | Set-Content -Encoding UTF8 $targetFileName
}

$acadRootDir = Read-Host 'Please enter your AutoCAD installation folder'

if (Test-Path $acadRootDir)
{
  if (Test-Path ..\Linq2Acad\Linq2Acad.csproj.user)
  {
    Remove-Item ..\Linq2Acad\Linq2Acad.csproj.user
  }
  if (Test-Path ..\Linq2Acad.SampleCode.CS\Linq2Acad.SampleCode.CS.csproj.user)
  {
    Remove-Item ..\Linq2Acad.SampleCode.CS\Linq2Acad.SampleCode.CS.csproj.user
  }
  if (Test-Path ..\Linq2Acad.SampleCode.VB\Linq2Acad.SampleCode.VB.vbproj.user)
  {
    Remove-Item ..\Linq2Acad.SampleCode.VB\Linq2Acad.SampleCode.VB.vbproj.user
  }
  if (Test-Path ..\Linq2Acad.Tests\Linq2Acad.Tests.csproj.user)
  {
    Remove-Item ..\Linq2Acad.Tests\Linq2Acad.Tests.csproj.user
  }
  
  Update-File ..\Linq2Acad\Linq2Acad.csproj.user "DefaultTemplate.csproj.user" "{AcadRootDir}" "$acadRootDir"
  Update-File ..\Linq2Acad.SampleCode.CS\Linq2Acad.SampleCode.CS.csproj.user "DefaultTemplate.csproj.user" "{AcadRootDir}" "$acadRootDir"
  Update-File ..\Linq2Acad.SampleCode.VB\Linq2Acad.SampleCode.VB.vbproj.user "DefaultTemplate.vbproj.user" "{AcadRootDir}" "$acadRootDir"
  Update-File ..\Linq2Acad.Tests\Linq2Acad.Tests.csproj.user "TestsTemplate.csproj.user" "{AcadRootDir}" "$acadRootDir"

  if (Test-Path ..\Linq2Acad.Tests\Linq2Acad.Tests/Libraries/AcadTestRunner.dll.config)
  {
    Remove-Item ..\Linq2Acad.Tests\Linq2Acad.Tests/Libraries/AcadTestRunner.dll.config
  }
  
  Update-File ..\Linq2Acad.Tests\Linq2Acad.Tests/Libraries/AcadTestRunner.dll.config "AcadTestRunner.dll.config" "{AcadRootDir}" "$acadRootDir"
  
  Write-Host 'AutoCAD reference path set to' $acadRootDir
}
else
{
  Write-Host $acadRootDir 'does not exist'
}
