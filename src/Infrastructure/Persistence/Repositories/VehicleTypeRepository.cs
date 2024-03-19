
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
    public class VehicleTypeRepository : Repository<VehicleType>, IVehicleTypeRepository
    {
        private readonly IMapper _mapper;

        public VehicleTypeRepository(ApplicationDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<VehicleTypeDTO>>> GetAllAsync()
        {
            var resultEntity = await _context.Set<VehicleType>()
                .AsNoTracking()
                .OrderBy(t => t.Name)
                .ToListAsync();

            var mappedResult = _mapper.Map<IEnumerable<VehicleTypeDTO>>(resultEntity);

            return await Result<IEnumerable<VehicleTypeDTO>>.SuccessAsync(mappedResult);
        }
    }
}
