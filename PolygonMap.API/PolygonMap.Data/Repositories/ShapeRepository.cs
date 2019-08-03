using Microsoft.EntityFrameworkCore;
using PolygonMap.Domain.Entities;
using PolygonMap.Domain.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PolygonMap.Data.Repositories
{
    public class ShapeRepository : IShapeRepository
    {
        private readonly PolygonMapContext _context;
        public ShapeRepository(PolygonMapContext context)
        {
            _context = context;
        }

        public void Dispose()=> _context.Dispose();

        public async Task<List<Shape>> GetAllAsync() => await _context.Shape.ToListAsync();        
        public async Task<Shape> GetByIdAsync(int id)=> await _context.Shape.FindAsync(id);        
        public async Task<Shape> AddAsync(Shape newShape)
        {
            _context.Shape.Add(newShape);
            await _context.SaveChangesAsync();
            return newShape;
        }
        public async Task<bool> UpdateAsync(Shape shape)
        {
            if (!( _context.Shape.FindAsync(shape)!=null))
                return false;

            _context.Shape.Update(shape);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
        
            if (!(_context.Shape.FindAsync(id) != null))
                return false;

            var toDelete = _context.Shape.Find(id);
            _context.Shape.Remove(toDelete);
            await _context.SaveChangesAsync();
            return true;
        }
       
    }
}
