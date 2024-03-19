using WirsolutViajes.Application.Interfaces.Repositories;
using WirsolutViajes.Application.Interfaces.Services;
using WirsolutViajes.Shared.DTOs;
using WirsolutViajes.Shared.Wrapper;

namespace WirsolutViajes.Application.Services
{
    public class TripService : ITripService
    {
        private readonly ITripRepository _tripRepository;

        public TripService(ITripRepository tripRepository)
        {
            _tripRepository = tripRepository;
        }

        public async Task<Result<int>> DeleteAsync(int id)
        {
            return await _tripRepository.DeleteAsync(id);
        }

        public async Task<Result<IEnumerable<TripDTO>>> GetFilteredTripsAsync(DateTime tripDate, int destinationId, int vehicleTypeId)
        {
            return await _tripRepository.GetFilteredTripsAsync(tripDate, destinationId, vehicleTypeId);
        }

        public async Task<Result<int>> SaveAsync(AddEditTripDTO addEditTripDTO)
        {
            return await _tripRepository.SaveAsync(addEditTripDTO);

        }
    }
}

