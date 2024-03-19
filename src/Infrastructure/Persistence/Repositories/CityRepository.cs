using Microsoft.EntityFrameworkCore;
using WirsolutViajes.Application.Interfaces.Repositories;
using WirsolutViajes.Domain.Entities;
using WirsolutViajes.Server.Infrastructure.Persistence.Contexts;
using WirsolutViajes.Server.Infrastructure.Persistence.Repositories;
using WirsolutViajes.Shared.DTOs;
using WirsolutViajes.Shared.Wrapper;

namespace WirsolutViajes.Infrastructure.Repositories
{
    public class CityRepository : Repository<City>, ICityRepository
    {
        public CityRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IResult<IEnumerable<CityDTO>>> GetAllActiveAsync()
        {
            var resultResponse = await(
                from c in _context.Set<City>().AsNoTracking()
                where c.Active == true
                orderby c.Name
                select new CityDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    Latitude = c.Latitude,
                    Longitude = c.Longitude,
                    Country = c.Country,
                    CountryISOCode = c.CountryISOCode,
                    State = c.State,
                    Active = c.Active,
                    DeactivatedAt = c.DeactivatedAt,
                    DeactivationComment = c.DeactivationComment
                })
                .ToListAsync();

            return await Result<IEnumerable<CityDTO>>.SuccessAsync(resultResponse);

        }

        public async Task<Result<IEnumerable<CityDTO>>> GetAllAsync()
        {
            var resultResponse = await (
                from c in _context.Set<City>().AsNoTracking()
                orderby c.Name
                select new CityDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    Latitude = c.Latitude,
                    Longitude = c.Longitude,
                    Country = c.Country,
                    CountryISOCode = c.CountryISOCode,
                    State = c.State,
                    Active = c.Active,
                    DeactivatedAt = c.DeactivatedAt,
                    DeactivationComment = c.DeactivationComment
                })
                .ToListAsync();


            return await Result<IEnumerable<CityDTO>>.SuccessAsync(resultResponse);

        }

        public async Task<Result<int>> SaveAsync(AddEditCityDTO addEditCityDTO)
        {
            var message = string.Empty;

            City city;
            if (addEditCityDTO.Id != 0)
            {
                city = await GetByIdAsync(addEditCityDTO.Id);
                if (city == null)
                {
                    return await Result<int>.FailAsync("Ciudad no encontrada!");
                }
            }
            else
            {
                city = new City();

                city.Active = true;
                addEditCityDTO.Active= true;  
            }

            if (city.Active && !addEditCityDTO.Active)
            {
                city.Active = false;
                city.DeactivatedAt = DateTime.Now;
                city.DeactivationComment = addEditCityDTO.DeactivationComment ?? addEditCityDTO.DeactivationComment;

                message = "Ciudad desactivada";
            }
            else if (!city.Active && addEditCityDTO.Active)
            {
                city.Active = true;
                city.DeactivatedAt = null;
                city.DeactivationComment = null;

                message = "Ciudad activada";
            }
            else
            {
                city.Name = addEditCityDTO.Name;
                city.Latitude = addEditCityDTO.Latitude;
                city.Longitude = addEditCityDTO.Longitude;
                city.Country = addEditCityDTO.Country;

                city.CountryISOCode = addEditCityDTO.CountryISOCode;
                city.State = addEditCityDTO.State;

                message = "Ciudad guardada";
            }

            if (addEditCityDTO.Id == 0)
            {
                await AddAsync(city);
            }
            else
            {
                await UpdateAsync(city);
            }

            return await Result<int>.SuccessAsync(city.Id, message);
        }
    }
}
