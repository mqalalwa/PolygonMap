using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PolygonMap.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;


namespace PolygonMap.Data.Configurations
{
    public  class ShapeConfiguration
    {

        public ShapeConfiguration(EntityTypeBuilder<Shape> entity)
        {
            entity.HasData(
              new Shape() { ShapeID = 1, Name = "Triangle",  FixedLatitude = 31.952291310722376f, FixedLongitude = 35.25867734940118f },
              new Shape() { ShapeID = 2, Name = "Quadrilateral", FixedLatitude = 31.952291310722376f, FixedLongitude = 35.25867734940118f },
              new Shape() { ShapeID = 3, Name = "Pentagon",  FixedLatitude = 31.952291310722376f, FixedLongitude = 35.25867734940118f },
              new Shape() { ShapeID = 4, Name = "Hexagon",   FixedLatitude = 31.952291310722376f, FixedLongitude = 35.25867734940118f }
              );                                             
        }
    }
}
