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

namespace Linq2Acad
{
    public class BlockReferenceBuilder
    {
        Point3d insertionPoint;

        public BlockReferenceBuilder NewBlockReference(BlockTableRecord blockTableRecord, Point3d insertionPoint)
        {
            this.insertionPoint = insertionPoint;
            return this;
        }

        public BlockReferenceBuilder WithDefaultAttributes()
        {
            var blockDef = (BlockTableRecord)acTrans.GetObject(acBlkTbl["TestBlock"], OpenMode.ForRead);
            foreach (ObjectId id in blockDef)
            {
                if (id.ObjectClass == RXObject.GetClass(typeof(AttributeDefinition)))
                {
                    var attDef = (AttributeDefinition)acTrans.GetObject(id, OpenMode.ForRead);
                    if (!attDef.Constant)
                    {
                        var attRef = new AttributeReference();
                        attRef.SetAttributeFromBlock(attDef, bref.BlockTransform);
                        bref.AttributeCollection.AppendAttribute(attRef);
                        acTrans.AddNewlyCreatedDBObject(attRef, true);                        
                    }
                }
            }
            return this;
        }

        public BlockReferenceBuilder WithAttributes(Dictionary<string, string> attributeTagValues)
        {

            return this;
        }

        public BlockReferenceBuilder WithAttributesThrow(Dictionary<string, string> attributeTagValues)
        {

            return this;
        }

        public BlockReference Build()
        {
            throw new NotImplementedException();
        }
    }
}

