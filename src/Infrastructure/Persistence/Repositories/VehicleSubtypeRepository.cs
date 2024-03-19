
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
    public class VehicleSubtypeRepository : Repository<VehicleSubtype>, IVehicleSubtypeRepository
    {
        private readonly IMapper _mapper;

        public VehicleSubtypeRepository(ApplicationDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<VehicleSubtypeDTO>>> GetAllByVehicleTypeIdAsync(int vehicleTypeId)
        {
            var resultEntity = await _context.Set<VehicleSubtype>()
                .AsNoTracking() 
                .Where(m => m.VehicleType.Id == vehicleTypeId)
                .OrderBy(m => m.Name)
                .ToListAsync();

            var mappedResult = _mapper.Map<IEnumerable<VehicleSubtypeDTO>>(resultEntity);

            return await Result<IEnumerable<VehicleSubtypeDTO>>.SuccessAsync(mappedResult);

        }
    }
}
