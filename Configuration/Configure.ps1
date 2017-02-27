function Update-File ($targetFileName, $tempalteFileName, $variableName, $value)
{
  if (Test-Path "$targetFileName")
  {
    Remove-Item "$targetFileName"
    $srcFileName = $tempalteFileName
  }
  
  (Get-Content -Encoding UTF8 $srcFileName) | ForEach-Object { $_ -replace $variableName, $value } | Set-Content -Encoding UTF8 $targetFileName
}

$acadRootDir = Read-Host 'Please enter your AutoCAD installation folder'

if (Test-Path $acadRootDir)
{  
  if (-Not (Test-Path $(Join-Path -Path "$($acadRootDir)" -ChildPath "AcCoreMgd.dll")))
  {
    Write-Host $(Join-Path -Path "$($acadRootDir)" -ChildPath "AcCoreMgd.dll") not found
  }
  elseif (-Not (Test-Path $(Join-Path -Path "$acadRootDir" -ChildPath "AcDbMgd.dll")))
  {
    Write-Host $(Join-Path -Path "$acadRootDir" -ChildPath "AcDbMgd.dll") not found
  }
  elseif (-Not (Test-Path $(Join-Path -Path "$acadRootDir" -ChildPath "AcMgd.dll")))
  {
    Write-Host $(Join-Path -Path "$acadRootDir" -ChildPath "AcMgd.dll") not found
  }
  else
  {
    Update-File "$($PSScriptRoot)\..\Linq2Acad\Linq2Acad.csproj.user" "$($PSScriptRoot)\DefaultTemplate.csproj.user" "{AcadRootDir}" "$acadRootDir"
    Update-File "$($PSScriptRoot)\..\Linq2Acad.SampleCode.CS\Linq2Acad.SampleCode.CS.csproj.user" "$($PSScriptRoot)\DefaultTemplate.csproj.user" "{AcadRootDir}" "$acadRootDir"
    Update-File "$($PSScriptRoot)\..\Linq2Acad.SampleCode.VB\Linq2Acad.SampleCode.VB.vbproj.user" "$($PSScriptRoot)\DefaultTemplate.csproj.user" "{AcadRootDir}" "$acadRootDir"
    Update-File "$($PSScriptRoot)\..\Linq2Acad.Tests\Linq2Acad.Tests.csproj.user" "$($PSScriptRoot)\DefaultTemplate.csproj.user" "{AcadRootDir}" "$acadRootDir"
    
    Write-Host ""
    Write-Host "Reference path ""$acadRootDir"" added"
  }
}
else
{
  Write-Host Directory $acadRootDir does not exist
}
