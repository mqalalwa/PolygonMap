using System;
using System.Collections.Generic;
using System.Text;

namespace PolygonMap.Domain.ApiModels
{
    public class PolygonApiModel
    {

       
        public int PolygonID { get; set; }
        public int ShapeID { get; set; }
        public string Name { get; set; }
        // point where user click on the map; will be as center point. 
        public float RealLongitude { get; set; }
        public float RealLatitude { get; set; }
        //json object
        public string Points { get; set; }

     //   public ShapeApiModel Shape { get; set; }
    }
}
