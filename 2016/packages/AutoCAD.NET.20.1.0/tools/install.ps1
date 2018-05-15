param($installPath, $toolsPath, $package, $project)
foreach ($reference in $project.Object.References)
{
    if($reference.Name -eq "acmgd" -Or
	   $reference.Name -eq "acwindows" -Or
	   $reference.Name -eq "accui" -Or
	   $reference.Name -eq "acdx" -Or
	   $reference.Name -eq "acmr" -Or
	   $reference.Name -eq "actcmgd" -Or
	   $reference.Name -eq "adwindows" ) 
	{
		$reference.CopyLocal = $false;
	}
}
