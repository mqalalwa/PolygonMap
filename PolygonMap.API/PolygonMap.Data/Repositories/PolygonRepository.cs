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
    public class PolygonRepository : IPolygonRepository
    {
        private readonly PolygonMapContext _context;
        public PolygonRepository(PolygonMapContext context)
        {
            _context = context;
        }

        public void Dispose() => _context.Dispose();
        
        public async Task<List<Polygon>> GetAllAsync() => await _context.Polygon.ToListAsync();       
        public async Task<Polygon> GetByIdAsync(int id)=> await _context.Polygon.FindAsync(id);
        public async Task<List<Polygon>> GetByShapeIdAsync(int id) => await _context.Polygon.Where(a => a.ShapeID == id).ToListAsync();
        public async Task<List<Polygon>> GetByListOfIdsAsync(List<int> ids) => await _context.Polygon.Where(a => ids.Contains(a.PolygonID)).ToListAsync();
        
        public async Task<Polygon> AddAsync(Polygon newPolygon)
        {
            _context.Polygon.Add(newPolygon);
            await _context.SaveChangesAsync();
            return newPolygon;
        }
        public async Task<bool> UpdateAsync(Polygon polygon)
        {
            //if (!await _context.Polygon.AnyAsync(a => a.PolygonID == polygon.PolygonID))
            //    return false;
            //_context.Polygon.Update(polygon);
            //await _context.SaveChangesAsync();

            var attachedPolygon = await _context.Polygon.FindAsync(polygon.PolygonID);
            if (attachedPolygon == null)
               return false;
            
            attachedPolygon.PolygonID = polygon.PolygonID;
            attachedPolygon.Name = polygon.Name;
            attachedPolygon.ShapeID = polygon.ShapeID;
            attachedPolygon.RealLatitude = polygon.RealLatitude;
            attachedPolygon.RealLongitude = polygon.RealLongitude;
            attachedPolygon.Points = polygon.Points;     

            _context.Entry(attachedPolygon).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;


        }
        public async Task<bool> DeleteAsync(int id)
        {
            if (!(_context.Polygon.FindAsync(id) != null))
                return false;

            var toDelete = _context.Polygon.Find(id);
            _context.Polygon.Remove(toDelete);
            await _context.SaveChangesAsync();
            return true;
        }

      
    }
}
