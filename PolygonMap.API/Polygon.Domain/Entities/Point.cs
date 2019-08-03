using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PolygonMap.Domain.Entities
{
     
    public sealed class Point
    {
        [Key]
        public int PointID { get; set; }        
        public int ShapeID { get; set; }
        [Required]
        public float Longitude { get; set; }
        [Required]
        public float Latitude { get; set; }

      //  public Shape Shape { get; set;}

    }
}
