using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PolygonMap.Domain.Entities
{
    public sealed class Polygon
    {
        [Key]
        public int PolygonID { get; set; }
        public int ShapeID { get; set; }
        [Required]
        public string Name { get; set; }
        // point where user click on the map; will be as center point. 
        [Required]
        public float RealLongitude { get; set; }
        [Required]
        public float RealLatitude { get; set; }
        //json object
        public string Points { get; set; }

      //  public Shape Shape { get; set; }
    }
}
