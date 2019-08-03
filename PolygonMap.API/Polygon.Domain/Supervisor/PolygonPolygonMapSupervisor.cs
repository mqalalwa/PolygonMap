using PolygonMap.Domain.ApiModels;
using PolygonMap.Domain.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PolygonMap.Domain.Entities;

namespace PolygonMap.Domain.Supervisor
{
    public partial class PolygonMapSupervisor 
    {
       public async Task<IEnumerable<PolygonApiModel>> GetAllPolygonAsync() =>
              _mapper.Map<List<PolygonApiModel>>(await _polygonRepository.GetAllAsync());

       public async Task<PolygonApiModel> GetPolygonByIdAsync(int id) =>
              _mapper.Map<PolygonApiModel>(await _polygonRepository.GetByIdAsync(id));

       public async Task<IEnumerable<PolygonApiModel>> GetPolygonsByListOfIdsAsync(List<int> ids)=>
              _mapper.Map<List<PolygonApiModel>>(await _polygonRepository.GetByListOfIdsAsync(ids));

       public async Task<IEnumerable<PolygonApiModel>> GetPolygonByShapeIdAsync(int id) =>
              _mapper.Map<List<PolygonApiModel>>(await _polygonRepository.GetByShapeIdAsync(id));
       
       public async Task<PolygonApiModel> AddPolygonAsync(PolygonApiModel newPolygonApiModel)
       {
               var polygon = _mapper.Map<Polygon>(newPolygonApiModel);
               polygon = await _polygonRepository.AddAsync(polygon);
               newPolygonApiModel.PolygonID = polygon.PolygonID;
               return newPolygonApiModel;
       }
       public async Task<bool> UpdatePolygonAsync(PolygonApiModel polygonApiModel)
       {
           var polygon = _mapper.Map<Polygon>(polygonApiModel);
           return await _polygonRepository.UpdateAsync(polygon);
       }
       public async Task<bool> DeletePolygonAsync(int id) => await _polygonRepository.DeleteAsync(id);       

    }
}
