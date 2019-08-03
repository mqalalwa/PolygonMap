using PolygonMap.Domain.ApiModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PolygonMap.Domain.Supervisor
{
    public interface IPolygonMapSupervisor
    {
        //Shape Supervisor
        Task<IEnumerable<ShapeApiModel>> GetAllShapeAsync();
        Task<ShapeApiModel> GetShapeByIdAsync(int id);
        Task<ShapeApiModel> AddShapeAsync(ShapeApiModel newShapeApiModel);
        Task<bool> UpdateShapeAsync(ShapeApiModel shapeApiModel);
        Task<bool> DeleteShapeAsync(int id);

        //Point Supervisor
        Task<IEnumerable<PointApiModel>> GetPointByShapeIdAsync(int id);


        //Polygon Supervisor      
        Task<IEnumerable<PolygonApiModel>> GetAllPolygonAsync();
        Task<PolygonApiModel> GetPolygonByIdAsync(int id);
        Task<IEnumerable<PolygonApiModel>> GetPolygonByShapeIdAsync(int id);
        Task<IEnumerable<PolygonApiModel>> GetPolygonsByListOfIdsAsync(List<int> ids);
        Task<PolygonApiModel> AddPolygonAsync(PolygonApiModel newPolygonApiModel);
        Task<bool> UpdatePolygonAsync(PolygonApiModel polygonApiModel);
        Task<bool> DeletePolygonAsync(int id);


    }
}
