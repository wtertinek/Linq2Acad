# DbObjectContainer Class
 

A container class that provides access to all database objects. In addition to the standard LINQ operations this class provides a method to add newly created DBObjects.


## Methods
&nbsp;<table><tr><th></th><th>Class</th><th>Description</th></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_Linq2Acad_DbObjectContainer_AddNewlyCreatedDBObject.md#DbObjectContainerAddNewlyCreatedDBObject-Method">AddNewlyCreatedDBObject(DBObject)</a></td><td>
Adds the given object to the underlaying transaction. This is only needed for objects that are not stored in containers (e.g. AttributeReference).</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_Linq2Acad_DbObjectContainer_Element__1.md#DbObjectContainerElementT-Method">Element(T)(ObjectId, [bool])</a></td><td>
Returns the database object with the given ObjectId.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_Linq2Acad_DbObjectContainer_ElementOrDefault__1.md#DbObjectContainerElementOrDefaultT-Method">ElementOrDefault(T)(ObjectId, [bool])</a></td><td>
Returns the database object with the given ObjectId, or <i>null</i> if the element does not exist.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_Linq2Acad_DbObjectContainer_Elements__1_1.md#DbObjectContainerElementsT-Method-IEnumerableObjectId-bool">Elements(T)(IEnumerable(ObjectId), [bool])</a></td><td>
Returns the database objects with the given ObjectIds.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_Linq2Acad_DbObjectContainer_Elements__1.md#DbObjectContainerElementsT-Method-ObjectIdCollection-bool">Elements(T)(ObjectIdCollection, [bool])</a></td><td>
Returns the database objects with the given ObjectIds.</td></tr></table>
<a href="#dbobjectcontainer-class">Back to Top</a>

## See Also


#### Reference
<a href="N_Linq2Acad.md#Linq2Acad-Namespace">Linq2Acad Namespace</a><br />
