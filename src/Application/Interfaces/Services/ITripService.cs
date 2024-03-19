using WirsolutViajes.Shared.DTOs;
using WirsolutViajes.Shared.Wrapper;

namespace WirsolutViajes.Application.Interfaces.Services
{
    public interface ITripService
    {
        Task<Result<IEnumerable<TripDTO>>> GetFilteredTripsAsync(DateTime tripDate, int destinationId,int vehicleTypeId);

        Task<Result<int>> SaveAsync(AddEditTripDTO addEditTripDTO);

        Task<Result<int>> DeleteAsync(int id);
    }
}
