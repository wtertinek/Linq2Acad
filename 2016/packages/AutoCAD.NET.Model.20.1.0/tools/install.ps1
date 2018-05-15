param($installPath, $toolsPath, $package, $project)
foreach ($reference in $project.Object.References)
{
    if($reference.Name -eq "acdbmgd" -Or
       $reference.Name -eq "acdbmgdbrep" )
    {
        $reference.CopyLocal = $false;
    }
}
