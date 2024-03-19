
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WirsolutViajes.Application.Interfaces.Repositories;
using WirsolutViajes.Domain.Entities;
using WirsolutViajes.Server.Infrastructure.Persistence.Contexts;
using WirsolutViajes.Server.Infrastructure.Persistence.Repositories;
using WirsolutViajes.Shared.DTOs;
using WirsolutViajes.Shared.Wrapper;

namespace WirsolutViajes.Infrastructure.Repositories
{
    public class ModelRepository : Repository<Model>, IModelRepository
    {
        private readonly IMapper _mapper;

        public ModelRepository(ApplicationDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<ModelDTO>>> GetAllByBrandVehicleTypeIdAsync(int brandId, int vehicleTypeId)
        {
            var resultEntity = await _context.Set<Model>()
                       .AsNoTracking()
                       .Where(mt => mt.BrandVehicleType.VehicleTypeId == vehicleTypeId && mt.BrandVehicleType.BrandId == brandId)
                       .OrderBy(m => m.Name)
            .ToListAsync();

            var mappedResult = _mapper.Map<IEnumerable<ModelDTO>>(resultEntity);

            return await Result<IEnumerable<ModelDTO>>.SuccessAsync(mappedResult);
        }

    }
}
