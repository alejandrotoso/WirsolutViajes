
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using WirsolutViajes.Application.Interfaces.Repositories;
using WirsolutViajes.Domain.Entities;
using WirsolutViajes.Server.Infrastructure.Persistence.Contexts;
using WirsolutViajes.Server.Infrastructure.Persistence.Repositories;
using WirsolutViajes.Shared.DTOs;
using WirsolutViajes.Shared.Wrapper;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WirsolutViajes.Infrastructure.Repositories
{
    public class TripRepository : Repository<Trip>, ITripRepository
    {
        private readonly IMapper _mapper;
        public TripRepository(ApplicationDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<TripDTO>>> GetFilteredTripsAsync(DateTime tripDate, int destinationId, int vehicleTypeId)
        {
            var resultResponse = await (
                from vi in _context.Set<Trip>().AsNoTracking()
                join c in _context.Set<City>().AsNoTracking() on vi.DestinationId equals c.Id
                join ve in _context.Set<Vehicle>().AsNoTracking() on vi.VehicleId equals ve.Id

                join mtv in _context.Set<BrandVehicleType>().AsNoTracking() on ve.BrandVehicleTypeId equals mtv.Id
                join tv in _context.Set<VehicleType>().AsNoTracking() on mtv.VehicleTypeId equals tv.Id
                join stv in _context.Set<VehicleSubtype>().AsNoTracking() on ve.VehicleSubtypeId equals stv.Id
                join ma in _context.Set<Brand>().AsNoTracking() on mtv.BrandId equals ma.Id
                join mo in _context.Set<Model>().AsNoTracking() on ve.ModelId equals mo.Id

                where vi.TripDate.Date == tripDate.Date &&
                    (destinationId == 0 || vi.DestinationId == destinationId) &&
                    (vehicleTypeId == 0 || tv.Id == vehicleTypeId)

                orderby vi.TripDate, c.Name, tv.Name, ma.Name, mo.Name

                select new
                {
                    Trip = vi,
                    City = c,
                    Vehicle = ve,
                    BrandVehicleType = mtv,
                    VehicleType = tv,
                    VehicleSubtype = stv,
                    Brand = ma,
                    Model = mo
                }
                 ).ToListAsync();


            var mappedResult = resultResponse.Select(x => _mapper.Map<TripDTO>(x.Trip, opt =>
            {
                opt.AfterMap((src, dest) =>
                {
                    dest.Destination = _mapper.Map<CityDTO>(x.City);
                    dest.Vehicle = _mapper.Map<VehicleDTO>(x.Vehicle);
                    dest.Vehicle.BrandVehicleType = _mapper.Map<BrandVehicleTypeDTO>(x.BrandVehicleType);
                    dest.Vehicle.BrandVehicleType.Brand = _mapper.Map<BrandDTO>(x.Brand);
                    dest.Vehicle.BrandVehicleType.VehicleType = _mapper.Map<VehicleTypeDTO>(x.VehicleType);
                    dest.Vehicle.VehicleSubtype = _mapper.Map<VehicleSubtypeDTO>(x.VehicleSubtype);
                    dest.Vehicle.Model = _mapper.Map<ModelDTO>(x.Model);
                });
            })).ToList();

            return await Result<IEnumerable<TripDTO>>.SuccessAsync(mappedResult);
        }

        public async Task<Result<int>> DeleteAsync(int id)
        {
            var trip = await GetByIdAsync(id);
            if (trip != null)
            {
                await DeleteAsync(trip);
                return await Result<int>.SuccessAsync(trip.Id, "Viaje eliminado!");
            }
            else
            {
                return await Result<int>.FailAsync("Viaje no encontrado");
            }
        }

        public async Task<Result<int>> SaveAsync(AddEditTripDTO addEditTripDTO)
        {
            Trip trip;
            if (addEditTripDTO.Id != 0)
            {
                trip = await GetByIdAsync(addEditTripDTO.Id);
                if (trip == null)
                {
                    return await Result<int>.FailAsync("Viaje no encontrado!");
                }
            }
            else
            {
                trip = new Trip();
            }

            trip.TripDate = addEditTripDTO.TripDate.Value;
            trip.DestinationId = addEditTripDTO.DestinationId;
            trip.VehicleId = addEditTripDTO.VehicleId;
            if (addEditTripDTO.Id == 0)
            {
                await AddAsync(trip);
            }
            else
            {
                await UpdateAsync(trip);
            }

            return await Result<int>.SuccessAsync(trip.Id, "Viaje guardado");
        }
    }
}
