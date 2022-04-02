# DbObjectsExtensions.ForEach(*T*) Method 
 

Performs the specified action on each element of the IEnumerable<DBObject>.

## Syntax

**C#**<br />
``` C#
public static void ForEach<T>(
	this IEnumerable<T> elements,
	Action<T> action
)
where T : DBObject

```

**VB**<br />
``` VB
<ExtensionAttribute>
Public Shared Sub ForEach(Of T As DBObject) ( 
	elements As IEnumerable(Of T),
	action As Action(Of T)
)
```


### Parameters
<dl><dt>elements</dt><dd>Type: IEnumerable(<i>T</i>)<br />The IEnumerable<DBObject> instance.</dd><dt>action</dt><dd>Type: <a href="https://docs.microsoft.com/dotnet/api/system.action-1" target="_blank" rel="noopener noreferrer">System.Action</a>(<i>T</i>)<br />The action to execute.</dd></dl>


#### Usage Note
In Visual Basic and C#, you can call this method as an instance method on any object of type IEnumerable(*T*). When you use instance method syntax to call this method, omit the first parameter. For more information, see <a href="https://docs.microsoft.com/dotnet/visual-basic/programming-guide/language-features/procedures/extension-methods" target="_blank" rel="noopener noreferrer">Extension Methods (Visual Basic)</a> or <a href="https://docs.microsoft.com/dotnet/csharp/programming-guide/classes-and-structs/extension-methods" target="_blank" rel="noopener noreferrer">Extension Methods (C# Programming Guide)</a>.
<a href="#DbObjectsExtensionsForEachT-Method">Back to Top</a>

## See Also


#### Reference
<a href="T_Linq2Acad_DbObjectsExtensions.md#DbObjectsExtensions-Class">DbObjectsExtensions Class</a><br /><a href="N_Linq2Acad.md#Linq2Acad-Namespace">Linq2Acad Namespace</a><br />
