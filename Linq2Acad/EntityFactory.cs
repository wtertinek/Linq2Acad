using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad
{
  public static class EntityFactory
  {
    public static EntityBuilder Create()
    {
      return new EntityBuilder();
    }

    public static Line CreateLine(double x1, double y1, double z1,
                                  double x2, double y2, double z2)
    {
      return CreateLine(new Point3d(x1, y1, z1), new Point3d(x2, y2, z2));
    }

    public static Line CreateLine(Point3d point1, Point3d point2)
    {
      return new Line(point1, point2);
    }

    #region Nested class EntityBuilder

    public class EntityBuilder
    {
      private List<Entity> entities;

      internal EntityBuilder()
      {
        entities = new List<Entity>();
      }

      public EntityBuilder Line(double x1, double y1, double z1,
                                double x2, double y2, double z2)
      {
        entities.Add(EntityFactory.CreateLine(x1, y1, z1, x2, y2, z2));
        return this;
      }

      public EntityBuilder Line(Point3d point1, Point3d point2)
      {
        entities.Add(EntityFactory.CreateLine(point1, point2));
        return this;
      }

      public IEnumerable<Entity> Build()
      {
        return entities;
      }
    }

    #endregion
  }
}
