# DbObjectsExtensions.UpgradeOpen(*T*) Method 
 

Upgrades the objects to open OpenMode.ForWrite

## Syntax

**C#**<br />
``` C#
public static IEnumerable<T> UpgradeOpen<T>(this IEnumerable<T> source)
where T : DBObject

```

**VB**<br />
``` VB
<ExtensionAttribute>Public Shared Function UpgradeOpen(Of T As DBObject) (source As IEnumerable(Of T)
) As IEnumerable(Of T)
```


### Parameters
<dl><dt>source</dt><dd>Type: IEnumerable(<i>T</i>)<br />The IEnumerable<DBObject> instance.</dd></dl>


### Return Value
Type: IEnumerable(*T*)<br />The given elements in OpenMode.ForWrite.

#### Usage Note
In Visual Basic and C#, you can call this method as an instance method on any object of type IEnumerable(*T*). When you use instance method syntax to call this method, omit the first parameter. For more information, see <a href="https://docs.microsoft.com/dotnet/visual-basic/programming-guide/language-features/procedures/extension-methods" target="_blank" rel="noopener noreferrer">Extension Methods (Visual Basic)</a> or <a href="https://docs.microsoft.com/dotnet/csharp/programming-guide/classes-and-structs/extension-methods" target="_blank" rel="noopener noreferrer">Extension Methods (C# Programming Guide)</a>.
<a href="#DbObjectsExtensionsUpgradeOpenT-Method">Back to Top</a>

## See Also


#### Reference
<a href="T_Linq2Acad_DbObjectsExtensions.md#DbObjectsExtensions-Class">DbObjectsExtensions Class</a><br /><a href="N_Linq2Acad.md#Linq2Acad-Namespace">Linq2Acad Namespace</a><br />
