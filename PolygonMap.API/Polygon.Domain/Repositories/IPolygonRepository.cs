using PolygonMap.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PolygonMap.Domain.Repositories
{
    public interface IPolygonRepository:IDisposable
    {
        Task<List<Polygon>> GetAllAsync();
        Task<List<Polygon>> GetByShapeIdAsync(int id);
        Task<List<Polygon>> GetByListOfIdsAsync(List<int> ids);
        Task<Polygon> GetByIdAsync(int id);
        Task<Polygon> AddAsync(Polygon newPolygon);
        Task<bool> UpdateAsync(Polygon polygon);
        Task<bool> DeleteAsync(int id);

    }

}

