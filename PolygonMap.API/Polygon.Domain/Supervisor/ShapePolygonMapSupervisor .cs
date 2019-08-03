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

        public async Task<IEnumerable<ShapeApiModel>> GetAllShapeAsync() =>
             _mapper.Map<List<ShapeApiModel>>(await _shapeRepository.GetAllAsync());

        public async Task<ShapeApiModel> GetShapeByIdAsync(int id)
        {
           
            var shapeApiModel = _mapper.Map<ShapeApiModel>(await _shapeRepository.GetByIdAsync(id));           
            shapeApiModel.Points =   (await GetPointByShapeIdAsync(shapeApiModel.ShapeID)).ToList();
            shapeApiModel.Polygons = (await GetPolygonByShapeIdAsync(shapeApiModel.ShapeID)).ToList();

            return shapeApiModel;
        }
        public async Task<ShapeApiModel> AddShapeAsync(ShapeApiModel newShapeApiModel)
        {
            var shape = _mapper.Map<Shape>(newShapeApiModel);
            shape = await _shapeRepository.AddAsync(shape);
            newShapeApiModel.ShapeID = shape.ShapeID;
            return newShapeApiModel;
        }
        public async Task<bool> UpdateShapeAsync(ShapeApiModel shapeApiModel)
        {
            var shape = _mapper.Map<Shape>(shapeApiModel);
            return await _shapeRepository.UpdateAsync(shape);
        }
        public async Task<bool> DeleteShapeAsync(int id) => await _shapeRepository.DeleteAsync(id);

    }
}
