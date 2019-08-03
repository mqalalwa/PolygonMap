using PolygonMap.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PolygonMap.Domain.Repositories
{
    public interface IShapeRepository : IDisposable
    {
       
        Task<List<Shape>> GetAllAsync();
        Task<Shape> GetByIdAsync(int id);
        Task<Shape> AddAsync(Shape newShape);
        Task<bool>  UpdateAsync(Shape shape);
        Task<bool>  DeleteAsync(int id);

    }

}

