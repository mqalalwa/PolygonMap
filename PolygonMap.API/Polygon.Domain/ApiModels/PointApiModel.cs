using System;
using System.Collections.Generic;
using System.Text;

namespace PolygonMap.Domain.ApiModels
{
    public class PointApiModel
    {

        public int PointID { get; set; }
        public int ShapeID { get; set; }   
        public float Longitude { get; set; }
        public float Latitude { get; set; }
      //  public ShapeApiModel Shape { get; set; }
    }
}
