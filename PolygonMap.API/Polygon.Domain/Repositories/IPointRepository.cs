using PolygonMap.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PolygonMap.Domain.Repositories
{
    public interface IPointRepository:IDisposable
    {
        Task<List<Point>> GetAllAsync();
        Task<Point> GetByIdAsync(int id);
        Task<List<Point>> GetByShapeIdAsync(int id);
        Task<Point> AddAsync(Point newPoint);
        Task<bool> UpdateAsync(Point point);
        Task<bool> DeleteAsync(int id);

    }

}

