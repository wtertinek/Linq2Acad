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

    public class LineBuilder
    {        
        double startX = 0;
        double startY = 0;
        double startZ = 0;

        double endX = 0;
        double endY = 0;
        double endZ = 0;

        /// <summary>
        /// StartPoint of the lines's x, y, z coordinates, assuming WCS (World Coordinate System)
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns> LineBuilder </returns>
        public LineBuilder From(double x = 0, double y = 0, double z = 0)
        {
            this.startX = x;
            this.startY = y;
            this.startZ = z;

            return this;
        }

        /// <summary>
        /// EndPoints of the lines's x, y, z coordinates, assuming WCS (World Coordinate System)
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public LineBuilder To(double x = 0, double y = 0, double z = 0)
        {
            this.endX = x;
            this.endY = y;
            this.endZ = z;

            return this;
        }

        /// <summary>
        /// Build's a new Line
        /// </summary>
        /// <code>
        ///         Line line = new LineBuilder()
        ///                         .From(1, 2, 3)
        ///                         .To(y: 4, z: 10)
        ///                         .Build();
        /// </code>
        /// <returns> Line </returns>
        public Line Build() => new Line().From(startX, startY, startZ).To(endX, endY, endZ);
    }
}

