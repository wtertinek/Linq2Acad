# PlotSettingsContainer.Import Method (PlotSettings, bool)
 

Imports the specified PlotSettings into the current database.

## Syntax

**C#**<br />
``` C#
public ImportResult<PlotSettings> Import(
	PlotSettings element,
	bool replaceIfDuplicate = false
)
```

**VB**<br />
``` VB
Public Function Import ( 
	element As PlotSettings,
	Optional replaceIfDuplicate As Boolean = false
) As ImportResult(Of PlotSettings)
```


### Parameters
<dl><dt>element</dt><dd>Type: PlotSettings<br />The PlotSettings to import.</dd><dt>replaceIfDuplicate (Optional)</dt><dd>Type: bool<br />true, if the the imported PlotSettings should be replaced if it is already present; otherwise, false.</dd></dl>

### Return Value
Type: <a href="T_Linq2Acad_ImportResult_1.md#ImportResultT-Class">ImportResult</a>(PlotSettings)<br />An object that represents the result of an import operation.
<a href="#PlotSettingsContainerImport-Method-PlotSettings-bool">Back to Top</a>

## See Also


#### Reference
<a href="T_Linq2Acad_PlotSettingsContainer.md#PlotSettingsContainer-Class">PlotSettingsContainer Class</a><br /><a href="N_Linq2Acad.md#Linq2Acad-Namespace">Linq2Acad Namespace</a><br />
