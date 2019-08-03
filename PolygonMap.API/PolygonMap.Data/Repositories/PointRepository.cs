using Microsoft.EntityFrameworkCore;
using PolygonMap.Domain.Entities;
using PolygonMap.Domain.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PolygonMap.Data.Repositories
{
    public class PointRepository : IPointRepository
    {
        private readonly PolygonMapContext _context;
        public PointRepository(PolygonMapContext context)
        {
            _context = context;
        }

        public void Dispose() => _context.Dispose();

        public async Task<List<Point>> GetAllAsync()=> await _context.Point.ToListAsync();
        public async Task<Point> GetByIdAsync(int id)=> await _context.Point.FindAsync(id);
        public async Task<List<Point>> GetByShapeIdAsync(int id) =>  await _context.Point.Where(a => a.ShapeID == id).ToListAsync();
      
        public async Task<Point> AddAsync(Point newPoint)
        {
            _context.Point.Add(newPoint);
            await _context.SaveChangesAsync();
            return newPoint;
        }
        public async Task<bool> UpdateAsync(Point point)
        {
            if (!(_context.Point.FindAsync(point) != null))
                return false;

            _context.Point.Update(point);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            if (!(_context.Point.FindAsync(id) != null))
                return false;

            var toDelete = _context.Point.Find(id);
            _context.Point.Remove(toDelete);
            await _context.SaveChangesAsync();
            return true;
        }


    }
}
