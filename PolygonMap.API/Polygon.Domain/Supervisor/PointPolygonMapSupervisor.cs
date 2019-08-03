using PolygonMap.Domain.ApiModels;
using PolygonMap.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PolygonMap.Domain.Supervisor
{
    public partial class PolygonMapSupervisor
    {
        public async Task<IEnumerable<PointApiModel>> GetPointByShapeIdAsync(int id) =>
            _mapper.Map<List<PointApiModel>>(await _pointRepository.GetByShapeIdAsync(id));
    }
}
