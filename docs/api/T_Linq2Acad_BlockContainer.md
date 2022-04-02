# BlockContainer Class
 

A container class that provides access to the elements of the Block table. In addition to the standard LINQ operations this class provides methods to create, add and import BlockTableRecords.


## Methods
&nbsp;<table><tr><th></th><th>Class</th><th>Description</th></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_Linq2Acad_BlockContainer_Add.md#BlockContainerAdd-Method-BlockTableRecord">Add(BlockTableRecord)</a></td><td>
Adds a newly created BlockTableRecord.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_Linq2Acad_BlockContainer_AddRange.md#BlockContainerAddRange-Method-IEnumerableBlockTableRecord">AddRange(IEnumerable(BlockTableRecord))</a></td><td>
Adds a range of newly created BlockTableRecords.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_Linq2Acad_BlockContainer_AsEntityContainers.md#BlockContainerAsEntityContainers-Method">AsEntityContainers()</a></td><td>
Converts each Block into an EntityContainer that allows querying for entities.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_Linq2Acad_BlockContainer_Contains_1.md#BlockContainerContains-Method-BlockTableRecord">Contains(BlockTableRecord)</a></td><td>
Determines whether a sequence contains the specified BlockTableRecord.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_Linq2Acad_BlockContainer_Contains.md#BlockContainerContains-Method-ObjectId">Contains(ObjectId)</a></td><td>
Determines whether a sequence contains the BlockTableRecord with the specified ID.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_Linq2Acad_BlockContainer_Contains_2.md#BlockContainerContains-Method-string">Contains(string)</a></td><td>
Determines whether a sequence contains the BlockTableRecord with the specified name.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_Linq2Acad_BlockContainer_Create_1.md#BlockContainerCreate-Method-IEnumerablestring">Create(IEnumerable(string))</a></td><td>
Creates a colletion of new BlockTableRecords.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_Linq2Acad_BlockContainer_Create_2.md#BlockContainerCreate-Method-string">Create(string)</a></td><td>
Creates a new BlockTableRecord with the specified name.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_Linq2Acad_BlockContainer_Create.md#BlockContainerCreate-Method-string-IEnumerableEntity">Create(string, IEnumerable(Entity))</a></td><td>
Creates a new BlockTableRecord with the specified name and adds the Entities to it.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_Linq2Acad_BlockContainer_Element.md#BlockContainerElement-Method-ObjectId-bool">Element(ObjectId, [bool])</a></td><td>
Returns the BlockTableRecord with the specified ID.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_Linq2Acad_BlockContainer_Element_1.md#BlockContainerElement-Method-string-bool">Element(string, [bool])</a></td><td>
Returns the BlockTableRecord with the specified name.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_Linq2Acad_BlockContainer_ElementAsEntityContainer.md#BlockContainerElementAsEntityContainer-Method">ElementAsEntityContainer(ObjectId)</a></td><td>
Converts the Block with the given ObjectId into an EntityContainer that allows querying for entities.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_Linq2Acad_BlockContainer_ElementOrDefault.md#BlockContainerElementOrDefault-Method-ObjectId-bool">ElementOrDefault(ObjectId, [bool])</a></td><td>
Returns the BlockTableRecord with the specified ID or <i>null</i> if the BlockTableRecord cannot be found.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_Linq2Acad_BlockContainer_ElementOrDefault_1.md#BlockContainerElementOrDefault-Method-string-bool">ElementOrDefault(string, [bool])</a></td><td>
Returns the BlockTableRecord with the specified name or <i>null</i> if the BlockTableRecord cannot be found.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_Linq2Acad_BlockContainer_Import_2.md#BlockContainerImport-Method-BlockTableRecord-bool">Import(BlockTableRecord, [bool])</a></td><td>
Imports the specified BlockTableRecord into the current database.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_Linq2Acad_BlockContainer_Import_1.md#BlockContainerImport-Method-IEnumerableBlockTableRecord-bool">Import(IEnumerable(BlockTableRecord), [bool])</a></td><td>
Imports the specified BlockTableRecords into the current database.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_Linq2Acad_BlockContainer_Import.md#BlockContainerImport-Method-string-string">Import(string, string)</a></td><td>
Creates a new block and imports all model space entities from the given drawing file to it.</td></tr></table>
<a href="#blockcontainer-class">Back to Top</a>

## See Also


#### Reference
<a href="N_Linq2Acad.md#Linq2Acad-Namespace">Linq2Acad Namespace</a><br />
