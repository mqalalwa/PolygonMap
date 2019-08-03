using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PolygonMap.Domain.Entities
{
     
    public sealed class Shape
    {
        [Key]
        public int ShapeID {get; set;}
        [Required]
        public string Name { get; set; }
        //fixed center to draw points from it
        [Required]
        public float FixedLongitude { get; set; }
        [Required]
        public float FixedLatitude { get; set; }

        public ICollection<Point> Points { get; set; }
        public ICollection<Polygon> Polygons { get; set; }
    }
}
