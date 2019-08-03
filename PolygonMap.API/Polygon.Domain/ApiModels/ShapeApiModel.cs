using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PolygonMap.Domain.ApiModels
{
    public class ShapeApiModel
    {

     
        public int ShapeID { get; set; }
        public string Name { get; set; }
        //fixed center to draw points from it
        public float FixedLongitude { get; set; }
        public float FixedLatitude { get; set; }

        public IList<PointApiModel> Points { get; set; }
        public IList<PolygonApiModel> Polygons { get; set; }
       
     }
}
