# PlotSettingsContainer.Import Method (IEnumerable(PlotSettings), bool)
 

Imports the specified PlotSettings into the current database.

## Syntax

**C#**<br />
``` C#
public IReadOnlyCollection<ImportResult<PlotSettings>> Import(
	IEnumerable<PlotSettings> elements,
	bool replaceIfDuplicate = false
)
```

**VB**<br />
``` VB
Public Function Import ( 
	elements As IEnumerable(Of PlotSettings),
	Optional replaceIfDuplicate As Boolean = false
) As IReadOnlyCollection(Of ImportResult(Of PlotSettings))
```


### Parameters
<dl><dt>elements</dt><dd>Type: IEnumerable(PlotSettings)<br />The elements to import.</dd><dt>replaceIfDuplicate (Optional)</dt><dd>Type: bool<br />true, if the the imported PlotSettings should be replaced if it is already present; otherwise, false.</dd></dl>

### Return Value
Type: IReadOnlyCollection(<a href="T_Linq2Acad_ImportResult_1.md#ImportResultT-Class">ImportResult</a>(PlotSettings))<br />An object that represents the result of an import operation.
<a href="#PlotSettingsContainerImport-Method-IEnumerablePlotSettings-bool">Back to Top</a>

## See Also


#### Reference
<a href="T_Linq2Acad_PlotSettingsContainer.md#PlotSettingsContainer-Class">PlotSettingsContainer Class</a><br /><a href="N_Linq2Acad.md#Linq2Acad-Namespace">Linq2Acad Namespace</a><br />
