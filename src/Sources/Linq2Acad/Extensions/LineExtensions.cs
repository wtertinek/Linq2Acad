using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.Geometry;
using System.Runtime.Remoting.Messaging;

namespace Linq2Acad
{
    /// <summary>
    /// Extension methods for instances of Line.
    /// </summary>
    public static class LineExtensions
    {
        /// <summary>
        /// Modifies the start point of the line.
        /// </summary>
        /// <param name="line"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns> Line </returns>
        /// <code>
        ///     line.From(2);           // line.StartPoint = new Point3d(2, 0, 0);
        ///     line.From(x: 2);        // line.StartPoint = new Point3d(2, 0, 0);
        ///     line.From(x: 2, y: 3);  // line.StartPoint = new Point3d(2, 3, 0);
        ///     line.From(y: 3);        // line.StartPoint = new Point3d(0, 3, 0);
        ///     line.From(2, 3);        // line.StartPoint = new Point3d(2, 3, 0);
        ///     line.From(2, 3, z: 4);  // line.StartPoint = new Point3d(2, 3, 4);
        /// </code>
        public static Line From(this Line line, double x = 0, double y = 0, double z = 0)
        {
            line.StartPoint = new Point3d(x, y, z);
            return line;
        }

        /// <summary>
        /// Sets the start point of a line.
        /// Equivalent to: line.StartPoint = point3d;
        /// </summary>
        /// <param name="line"></param>
        /// <param name="startPoint"></param>
        /// <returns></returns>
        public static Line From(this Line line, Point3d startPoint)
        {
            line.StartPoint = startPoint;
            return line;
        }

        /// <summary>
        /// Sets the EndPoint of a line. 
        /// Equivalent to: line.EndPoint = point3d;
        /// </summary>
        /// <param name="line"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        public static Line To(this Line line, Point3d endPoint)
        {
            line.EndPoint = endPoint;
            return line;
        }


        /// <summary>
        /// Modifies the end point of the line.
        /// </summary>
        /// <param name="line"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns> Line </returns>
        /// <code>
        ///     line.To(2);           // line.EndPoint = new Point3d(2, 0, 0);
        ///     line.To(x: 2);        // line.EndPoint = new Point3d(2, 0, 0);
        ///     line.To(x: 2, y: 3);  // line.EndPoint = new Point3d(2, 3, 0);
        ///     line.To(y: 3);        // line.EndPoint = new Point3d(0, 3, 0);
        ///     line.To(2, 3);        // line.EndPoint = new Point3d(2, 3, 0);
        ///     line.To(2, 3, z: 4);  // line.EndPoint = new Point3d(2, 3, 4);
        /// </code>
        public static Line To(this Line line, double x = 0, double y = 0, double z = 0)
        {
            line.EndPoint = new Point3d(x, y, z);
            return line;
        }
    }
}

