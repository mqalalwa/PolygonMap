using AutoMapper;
using PolygonMap.Domain.ApiModels;
using PolygonMap.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PolygonMap.Domain.Supervisor
{
    public partial class PolygonMapSupervisor : IPolygonMapSupervisor
    {
        private readonly IPolygonRepository _IPolygonRepository;
        public PolygonMapSupervisor(IPolygonRepository IPolygonRepository)
        {
            _IPolygonRepository = IPolygonRepository;
        }

        private readonly IPolygonRepository _polygonRepository;
        private readonly IShapeRepository _shapeRepository;
        private readonly IPointRepository _pointRepository;
        private readonly IMapper _mapper;

        public PolygonMapSupervisor()
        {
        }

        public PolygonMapSupervisor(IPolygonRepository polygonRepository,
            IShapeRepository shapeRepository,
            IPointRepository pointRepository,
            IMapper mapper
        )
        {
            _polygonRepository = polygonRepository;
            _shapeRepository = shapeRepository;
            _pointRepository = pointRepository;
            _mapper = mapper;
        }
    }
}
