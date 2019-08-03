using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PolygonMap.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;


namespace PolygonMap.Data.Configurations
{
    public  class PointConfiguration
    {

        public PointConfiguration(EntityTypeBuilder<Point> entity)
        {
            entity.HasData(
              new Point() { PointID = 1, ShapeID = 1, Latitude = 31.9635641446775f, Longitude = 35.2622822383172f },
              new Point() { PointID = 2, ShapeID = 1, Latitude = 31.9532234830172f, Longitude = 35.2512959101922f },
              new Point() { PointID = 3, ShapeID = 1, Latitude = 31.9536604359408f, Longitude = 35.2696636775261f },

              new Point() { PointID = 4, ShapeID = 2, Latitude = 31.9607387846037f, Longitude = 35.2677754023797f },
              new Point() { PointID = 5, ShapeID = 2, Latitude = 31.9607387846037f, Longitude = 35.2512959101922f },
              new Point() { PointID = 6, ShapeID = 2, Latitude = 31.9483586009739f, Longitude = 35.2512959101922f },
              new Point() { PointID = 7, ShapeID = 2, Latitude = 31.9486499185804f, Longitude = 35.2677754023797f },

              new Point() { PointID = 8, ShapeID = 3, Latitude = 31.9485042598926f, Longitude = 35.2512959101922f },
              new Point() { PointID = 9, ShapeID = 3, Latitude = 31.9486499185804f, Longitude = 35.2677754023797f },
              new Point() {PointID = 10, ShapeID = 3, Latitude = 31.9549130236913f, Longitude = 35.2677754023797f },
              new Point() {PointID = 11, ShapeID = 3, Latitude = 31.9610300629488f, Longitude = 35.2595356562859f },
              new Point() {PointID = 12, ShapeID = 3, Latitude = 31.9547673749338f, Longitude = 35.2512959101922f },

              new Point() {PointID = 13, ShapeID = 4, Latitude = 31.9617582547694f, Longitude = 35.2591923335320f },
              new Point() {PointID = 14, ShapeID = 4, Latitude = 31.9552043205133f, Longitude = 35.2674320796257f },
              new Point() {PointID = 15, ShapeID = 4, Latitude = 31.9470476603141f, Longitude = 35.2679470637566f },
              new Point() {PointID = 16, ShapeID = 4, Latitude = 31.9402013329882f, Longitude = 35.2600506404168f },
              new Point() {PointID = 17, ShapeID = 4, Latitude = 31.9467563376274f, Longitude = 35.2512959101922f },
              new Point() {PointID = 18, ShapeID = 4, Latitude = 31.9553499685779f, Longitude = 35.2506092646843f }

              );
        }
    }
}



