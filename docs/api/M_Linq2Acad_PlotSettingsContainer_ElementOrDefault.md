# PlotSettingsContainer.ElementOrDefault Method 
 

Returns the PlotSettings with the specified ID or <i>null</i> if the PlotSettings cannot be found.

## Syntax

**C#**<br />
``` C#
public PlotSettings ElementOrDefault(
	ObjectId id,
	bool openForWrite = false
)
```

**VB**<br />
``` VB
Public Function ElementOrDefault ( 
	id As ObjectId,
	Optional openForWrite As Boolean = false
) As PlotSettings
```


#### Parameters
<dl><dt>id</dt><dd>Type: ObjectId<br />The ID of the PlotSettings.</dd><dt>openForWrite (Optional)</dt><dd>Type: bool<br />True, if the object should be opened for-write. By default the object is opened readonly.</dd></dl>

#### Return Value
Type: PlotSettings<br />The PlotSettings with the specified ID.

## See Also


#### Reference
<a href="T_Linq2Acad_PlotSettingsContainer.md">PlotSettingsContainer Class</a><br /><a href="N_Linq2Acad.md">Linq2Acad Namespace</a><br />
