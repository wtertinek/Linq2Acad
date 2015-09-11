@ECHO OFF

IF EXIST accoremgd.dll del accoremgd.dll
mklink accoremgd.dll "C:\Program Files\Autodesk\AutoCAD %1\accoremgd.dll"

IF EXIST acdbmgd.dll del acdbmgd.dll
mklink acdbmgd.dll "C:\Program Files\Autodesk\AutoCAD %1\acdbmgd.dll"

IF EXIST acmgd.dll del acmgd.dll
mklink acmgd.dll "C:\Program Files\Autodesk\AutoCAD %1\acmgd.dll"