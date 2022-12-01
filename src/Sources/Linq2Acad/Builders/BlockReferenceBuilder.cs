using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.Geometry;
using System.Runtime.Remoting.Messaging;
using Autodesk.AutoCAD.BoundaryRepresentation;
using Autodesk.AutoCAD.Runtime;
using System.Data.Common;

namespace Linq2Acad
{
    public class BlockReferenceBuilder
    {
        Point3d insertionPoint;
        BlockTableRecord blockTableRecord;
        private readonly Database db;
        private readonly Transaction tr;

        BlockReference blockReference;

        public BlockReferenceBuilder(Database db, Transaction tr)
        {
            this.db = db;
            this.tr = tr;
        }

        public BlockReferenceBuilder NewBlockReference(BlockTableRecord blockTableRecord, Point3d insertionPoint)
        {
            this.insertionPoint = insertionPoint;
            this.blockTableRecord = blockTableRecord;
            this.blockReference = new BlockReference(insertionPoint, blockTableRecord.ObjectId);

            return this;
        }

        public BlockReferenceBuilder WithDefaultAttributes()
        {   
            // block reference cannot be null
            foreach (ObjectId id in blockTableRecord)
            {
                if (id.ObjectClass == RXObject.GetClass(typeof(AttributeDefinition)))
                {
                    var attDef = (AttributeDefinition)tr.GetObject(id, OpenMode.ForRead);
                    if (!attDef.Constant)
                    {
                        var attRef = new AttributeReference();
                        attRef.SetAttributeFromBlock(attDef, blockReference.BlockTransform);
                        blockReference.AttributeCollection.AppendAttribute(attRef);
                        tr.AddNewlyCreatedDBObject(attRef, true);                        
                    }
                }
            }
            return this;
        }

        public BlockReferenceBuilder WithAttributes(Dictionary<string, string> attributeTagValues)
        {
            // using sensible defaults.
            Dictionary<string, string> legalTagsAndValues = getLegalAttributesAndTags(attributeTagValues);

            foreach (ObjectId id in blockTableRecord)
            {
                if (id.ObjectClass == RXObject.GetClass(typeof(AttributeDefinition)))
                {
                    var attDef = (AttributeDefinition)tr.GetObject(id, OpenMode.ForRead);
                    if (!attDef.Constant)
                    {
                        var attRef = new AttributeReference();
                        attRef.SetAttributeFromBlock(attDef, blockReference.BlockTransform);
                        blockReference.AttributeCollection.AppendAttribute(attRef);
                        tr.AddNewlyCreatedDBObject(attRef, true);
                    }
                }
            }
            return this;            
        }

        public BlockReferenceBuilder WithAttributesThrow(Dictionary<string, string> attributeTagValues)
        {

            return this;
        }

        public BlockReference Build()
        {            
        }

        private Dictionary<string, string> getLegalAttributesAndTags(Dictionary<string, string> tagValueDicctionary)
        {
            // tags cannot contain ! exclamation points or
            // white spaces

            return tagValueDicctionary.Where(kvp => kvp.Key.Contains("!") || kvp.Key.Any(Char.IsWhiteSpace))
                                     .ToDictionary(kvp => kvp.Key.ToUpper(), kvp => kvp.Value );
        }
    }
}

