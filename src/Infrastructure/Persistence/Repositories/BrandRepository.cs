using Microsoft.EntityFrameworkCore;
using WirsolutViajes.Application.Interfaces.Repositories;
using WirsolutViajes.Domain.Entities;
using WirsolutViajes.Server.Infrastructure.Persistence.Contexts;
using WirsolutViajes.Server.Infrastructure.Persistence.Repositories;
using WirsolutViajes.Shared.DTOs;
using WirsolutViajes.Shared.Wrapper;

namespace WirsolutViajes.Infrastructure.Repositories
{
    public class BrandRepository : Repository<Brand>, IBrandRepository
    {
        public BrandRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Result<IEnumerable<BrandDTO>>> GetAllByVehicleTypeIdAsync(int vehicleTypeId)
        {

            var resultResponse = await (
                from b in _context.Set<Brand>().AsNoTracking()
                join bvt in _context.Set<BrandVehicleType>().AsNoTracking() on b.Id equals bvt.BrandId
                where bvt.VehicleTypeId == vehicleTypeId
                orderby b.Name 
                select new BrandDTO  
                { 
                    Id = b.Id,
                    Name = b.Name,
                    BrandVehicleTypeId = bvt.Id 
                })
                .ToListAsync();


            return await Result<IEnumerable<BrandDTO>>.SuccessAsync(resultResponse);

        }
    }
}


