# AcadDatabase Class
 

The main class that provides access to the drawing database.


## Properties
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="P_Linq2Acad_AcadDatabase_Blocks.md">Blocks</a></td><td>
Provides access to the elements of the Block table and methods to create, add and import BlockTableRecords.</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="P_Linq2Acad_AcadDatabase_CurrentSpace.md">CurrentSpace</a></td><td>
Provides access to the entities of the currently active space. In addition to the standard LINQ operations this class provides methods to add, import and clear Entities.</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="P_Linq2Acad_AcadDatabase_Database.md">Database</a></td><td>
The drawing database in use.</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="P_Linq2Acad_AcadDatabase_DbObjects.md">DbObjects</a></td><td>
Provides access to all database objects. In addition to the standard LINQ operations this class provides a method to add newly created DBObjects.</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="P_Linq2Acad_AcadDatabase_Groups.md">Groups</a></td><td>
Provides access to the elements of the Group dictionary and methods to create, add and import Groups.</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="P_Linq2Acad_AcadDatabase_Layers.md">Layers</a></td><td>
Provides access to the elements of the Layer table and methods to create, add and import LayerTableRecords.</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="P_Linq2Acad_AcadDatabase_Layouts.md">Layouts</a></td><td>
Provides access to the elements of the Layout dictionary and methods to create, add and import Layouts.</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="P_Linq2Acad_AcadDatabase_Linetypes.md">Linetypes</a></td><td>
Provides access to the elements of the Linetype table and methods to create, add and import LinetypeTableRecords.</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="P_Linq2Acad_AcadDatabase_Materials.md">Materials</a></td><td>
Provides access to the elements of the Material dictionary and methods to create, add and import Materials.</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="P_Linq2Acad_AcadDatabase_ModelSpace.md">ModelSpace</a></td><td>
Provides access to the model space entities. In addition to the standard LINQ operations this class provides methods to add, import and clear Entities.</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="P_Linq2Acad_AcadDatabase_PaperSpace.md">PaperSpace</a></td><td>
Provides access to the paper space layouts.</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="P_Linq2Acad_AcadDatabase_PlotSettings.md">PlotSettings</a></td><td>
Provides access to the elements of the PlotSettings dictionary and methods to create, add and import PlotSettings.</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="P_Linq2Acad_AcadDatabase_RegApps.md">RegApps</a></td><td>
Provides access to the elements of the RegApp table and methods to create, add and import RegAppTableRecords.</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="P_Linq2Acad_AcadDatabase_Styles.md">Styles</a></td><td>
Provides access to all style related tables and dictionaries.</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="P_Linq2Acad_AcadDatabase_SummaryInfo.md">SummaryInfo</a></td><td>
Provies access to the summary info.</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="P_Linq2Acad_AcadDatabase_Ucss.md">Ucss</a></td><td>
Provides access to the elements of the Ucs table and methods to create, add and import UcsTableRecords.</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="P_Linq2Acad_AcadDatabase_Viewports.md">Viewports</a></td><td>
Provides access to the elements of the Viewport table and methods to create, add and import ViewportTableRecords.</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="P_Linq2Acad_AcadDatabase_Views.md">Views</a></td><td>
Provides access to the elements of the View table and methods to create, add and import ViewTableRecords.</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="P_Linq2Acad_AcadDatabase_XRefs.md">XRefs</a></td><td>
Provides access to all XRef elements and methods to attach, overlay, resolve, reload and unload XRefs.</td></tr></table>&nbsp;
<a href="#acaddatabase-class">Back to Top</a>

## Methods
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public method](media/pubmethod.gif "Public method")![Static member](media/static.gif "Static member")</td><td><a href="M_Linq2Acad_AcadDatabase_Active.md">Active()</a></td><td>
Provides access to the drawing database of the active document.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")![Static member](media/static.gif "Static member")</td><td><a href="M_Linq2Acad_AcadDatabase_Active_1.md">Active(Transaction, bool, bool)</a></td><td>
Provides access to the drawing database of the active document. This is an advanced feature, use with caution.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")![Static member](media/static.gif "Static member")</td><td><a href="M_Linq2Acad_AcadDatabase_Create.md">Create(string)</a></td><td>
Provides access to a newly created drawing database.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_Linq2Acad_AcadDatabase_DiscardChanges.md">DiscardChanges()</a></td><td>
Immediately discards all changes and the underlying transaction.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_Linq2Acad_AcadDatabase_Dispose.md">Dispose()</a></td><td>
Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")![Static member](media/static.gif "Static member")</td><td><a href="M_Linq2Acad_AcadDatabase_OpenForEdit.md">OpenForEdit(string, OpenForEditOptions)</a></td><td>
Provides read/write access to the drawing database in the given file.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")![Static member](media/static.gif "Static member")</td><td><a href="M_Linq2Acad_AcadDatabase_OpenReadOnly.md">OpenReadOnly(string, OpenReadOnlyOptions)</a></td><td>
Provides read only access to the drawing database in the given file.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")![Static member](media/static.gif "Static member")</td><td><a href="M_Linq2Acad_AcadDatabase_Use.md">Use(Database)</a></td><td>
Provides access to the given drawing database.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")![Static member](media/static.gif "Static member")</td><td><a href="M_Linq2Acad_AcadDatabase_Use_1.md">Use(Database, Transaction, bool, bool)</a></td><td>
Provides access to the given drawing database. This is an advanced feature, use with caution.</td></tr></table>&nbsp;
<a href="#acaddatabase-class">Back to Top</a>

## See Also


#### Reference
<a href="N_Linq2Acad.md">Linq2Acad Namespace</a><br />
