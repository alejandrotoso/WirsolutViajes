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
    public class VehicleRepository : Repository<Vehicle>, IVehicleRepository
    {

        private readonly IMapper _mapper;
        public VehicleRepository(ApplicationDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<VehicleDTO>>> GetAllAsync()
        {
            var resultResponse = await (
                from ve in _context.Set<Vehicle>().AsNoTracking()
                join mtv in _context.Set<BrandVehicleType>().AsNoTracking() on ve.BrandVehicleTypeId equals mtv.Id
                join tv in _context.Set<VehicleType>().AsNoTracking() on mtv.VehicleTypeId equals tv.Id
                join stv in _context.Set<VehicleSubtype>().AsNoTracking() on ve.VehicleSubtypeId equals stv.Id
                join ma in _context.Set<Brand>().AsNoTracking() on mtv.BrandId equals ma.Id
                join mo in _context.Set<Model>().AsNoTracking() on ve.ModelId equals mo.Id

                orderby tv.Name, ma.Name, mo.Name

                select new
                {
                    Vehicle = ve,
                    BrandVehicleType = mtv,
                    VehicleType = tv,
                    VehicleSubtype = stv,
                    Brand = ma,
                    Model = mo
                }
                ).ToListAsync();


                var mappedResult = resultResponse.Select(x => _mapper.Map<VehicleDTO>(x.Vehicle, opt =>
                {
                    opt.AfterMap((src, dest) =>
                    {
                        dest.BrandVehicleType = _mapper.Map<BrandVehicleTypeDTO>(x.BrandVehicleType);
                        dest.BrandVehicleType.Brand = _mapper.Map<BrandDTO>(x.Brand);
                        dest.BrandVehicleType.VehicleType = _mapper.Map<VehicleTypeDTO>(x.VehicleType);
                        dest.VehicleSubtype = _mapper.Map<VehicleSubtypeDTO>(x.VehicleSubtype);
                        dest.Model = _mapper.Map<ModelDTO>(x.Model);
                    });
                })).ToList();

                return await Result<IEnumerable<VehicleDTO>>.SuccessAsync(mappedResult);
        }


        public async Task<Result<VehicleDTO>> GetByIdAsync(int id)
        {
            var vehicle = await (
               from ve in _context.Set<Vehicle>().AsNoTracking()
               join mtv in _context.Set<BrandVehicleType>().AsNoTracking() on ve.BrandVehicleTypeId equals mtv.Id
               join tv in _context.Set<VehicleType>().AsNoTracking() on mtv.VehicleTypeId equals tv.Id
               join stv in _context.Set<VehicleSubtype>().AsNoTracking() on ve.VehicleSubtypeId equals stv.Id
               join ma in _context.Set<Brand>().AsNoTracking() on mtv.BrandId equals ma.Id
               join mo in _context.Set<Model>().AsNoTracking() on ve.ModelId equals mo.Id

               where ve.Id == id 

               select new
               {
                   Vehicle = ve,
                   BrandVehicleType = mtv,
                   VehicleType = tv,
                   VehicleSubtype = stv,
                   Brand = ma,
                   Model = mo
               }
               ).FirstOrDefaultAsync();


            if (vehicle == null)
            {
                return Result<VehicleDTO>.Fail("Vehículo no encontrado!");
            }

            var mapped = _mapper.Map<VehicleDTO>(vehicle.Vehicle, opt =>
            {
                opt.AfterMap((src, dest) =>
                {
                    dest.BrandVehicleType = _mapper.Map<BrandVehicleTypeDTO>(vehicle.BrandVehicleType);
                    dest.BrandVehicleType.Brand = _mapper.Map<BrandDTO>(vehicle.Brand);
                    dest.BrandVehicleType.VehicleType = _mapper.Map<VehicleTypeDTO>(vehicle.VehicleType);
                    dest.VehicleSubtype = _mapper.Map<VehicleSubtypeDTO>(vehicle.VehicleSubtype);
                    dest.Model = _mapper.Map<ModelDTO>(vehicle.Model);
                });
            });

            return await Result<VehicleDTO>.SuccessAsync(mapped);
        }

        public async Task<Result<IEnumerable<VehicleDTO>>> GetByTypeAndVehicleSubtypeIdAsync(int vehicleTypeId, int vehicleSubtypeId)
        {
            var resultResponse = await(
                from ve in _context.Set<Vehicle>().AsNoTracking()
                join mtv in _context.Set<BrandVehicleType>().AsNoTracking() on ve.BrandVehicleTypeId equals mtv.Id
                join tv in _context.Set<VehicleType>().AsNoTracking() on mtv.VehicleTypeId equals tv.Id
                join stv in _context.Set<VehicleSubtype>().AsNoTracking() on ve.VehicleSubtypeId equals stv.Id
                join ma in _context.Set<Brand>().AsNoTracking() on mtv.BrandId equals ma.Id
                join mo in _context.Set<Model>().AsNoTracking() on ve.ModelId equals mo.Id

                where tv.Id == vehicleTypeId &&
                      ve.Active == true &&  
                      (vehicleSubtypeId == 0 || stv.Id == vehicleSubtypeId)
                orderby tv.Name, ma.Name, mo.Name


                select new
                {
                    Vehicle = ve,
                    BrandVehicleType = mtv,
                    VehicleType = tv,
                    VehicleSubtype = stv,
                    Brand = ma,
                    Model = mo
                }
                ).ToListAsync();

                var mappedResult = resultResponse.Select(x => _mapper.Map<VehicleDTO>(x.Vehicle, opt =>
                {
                    opt.AfterMap((src, dest) =>
                    {
                        dest.BrandVehicleType = _mapper.Map<BrandVehicleTypeDTO>(x.BrandVehicleType);
                        dest.BrandVehicleType.Brand = _mapper.Map<BrandDTO>(x.Brand);
                        dest.BrandVehicleType.VehicleType = _mapper.Map<VehicleTypeDTO>(x.VehicleType);
                        dest.VehicleSubtype = _mapper.Map<VehicleSubtypeDTO>(x.VehicleSubtype);
                        dest.Model = _mapper.Map<ModelDTO>(x.Model);
                    });
                })).ToList();

                return await Result<IEnumerable<VehicleDTO>>.SuccessAsync(mappedResult);
        }

        public async Task<Result<int>> SaveAsync(AddEditVehicleDTO addEditVehicleDTO)
        {
            var message = string.Empty;

            Vehicle vehicle;
            if (addEditVehicleDTO.Id != 0)
            {
                vehicle = await base.GetByIdAsync(addEditVehicleDTO.Id);
                if (vehicle == null)
                {
                    return await Result<int>.FailAsync("Vehículo no encontrado!");
                }
            }
            else
            {
                vehicle = new Vehicle();

                vehicle.Active = true;
                addEditVehicleDTO.Active = true;
            }


            if (vehicle.Active && !addEditVehicleDTO.Active)
            {
                vehicle.Active = false;
                vehicle.DeactivatedAt = DateTime.Now;
                vehicle.DeactivationComment = addEditVehicleDTO.DeactivationComment ?? addEditVehicleDTO.DeactivationComment;

                message = "Vehículo desactivado";

            }
            else if (!vehicle.Active && addEditVehicleDTO.Active)
            {
                vehicle.Active = true;
                vehicle.DeactivatedAt = null;
                vehicle.DeactivationComment = null;

                message = "Vehículo activado";
            }
            else
            {
                vehicle.LicensePlate = addEditVehicleDTO.LicensePlate;
                vehicle.BrandVehicleTypeId = addEditVehicleDTO.BrandVehicleTypeId;
                vehicle.VehicleSubtypeId = int.Parse(addEditVehicleDTO.VehicleSubtypeId);
                vehicle.ModelId = int.Parse(addEditVehicleDTO.ModelId);
                vehicle.YearManufactured = addEditVehicleDTO.YearManufactured;
                vehicle.Mileage = addEditVehicleDTO.Mileage;

                message = "Vehículo guardado";
            }

            if (addEditVehicleDTO.Id == 0)
            {
                await AddAsync(vehicle);
            }
            else
            {
                await UpdateAsync(vehicle);
            }

            return await Result<int>.SuccessAsync(vehicle.Id, message);
        }
    }
}
