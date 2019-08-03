using Microsoft.EntityFrameworkCore;
using PolygonMap.Data.Configurations;
using PolygonMap.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PolygonMap.Data
{
    public class PolygonMapContext : DbContext
    {

        public virtual DbSet<Polygon> Polygon { get; set; }
        public virtual DbSet<Shape> Shape { get; set; }
        public virtual DbSet<Point> Point { get; set; }

        public PolygonMapContext(DbContextOptions options)
         : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new ShapeConfiguration(modelBuilder.Entity<Shape>());
            new PointConfiguration(modelBuilder.Entity<Point>());
        }

    }
}
