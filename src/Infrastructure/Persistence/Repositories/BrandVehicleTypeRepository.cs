
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
    public class BrandVehicleTypeRepository : Repository<BrandVehicleType>, IBrandVehicleTypeRepository
    {
        private readonly IMapper _mapper;

        public BrandVehicleTypeRepository(ApplicationDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }


        public async Task<Result<BrandVehicleTypeDTO>> GetByIdAsync(int id)
        {
            var resultEntity = await _context.Set<BrandVehicleType>()
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(entity => entity.Id == id);

            var mappedResult = _mapper.Map<BrandVehicleTypeDTO>(resultEntity);

            return await Result<BrandVehicleTypeDTO>.SuccessAsync(mappedResult);

        }

        public async Task<Result<IEnumerable<VehicleType>>> GetAllOrderedByNameAsync()
        {
            var resultResponse = await _context.Set<VehicleType>()
                .AsNoTracking()
                .OrderBy(t => t.Name)
                .ToListAsync();

            return await Result<IEnumerable<VehicleType>>.SuccessAsync(resultResponse);
        }
    }
}
