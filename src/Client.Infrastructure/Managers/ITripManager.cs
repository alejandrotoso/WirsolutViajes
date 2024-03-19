using WirsolutViajes.Shared.DTOs;
using WirsolutViajes.Shared.Wrapper;

namespace WirsolutViajes.Client.Infrastructure.Managers
{
    public interface ITripManager : IManager
    {
        Task<IResult<int>> DeleteAsync(int id);
        Task<IResult<List<TripDTO>>> GetAllAsync(DateTime date, int destinationId, int vehicleTypeId);
        Task<IResult<int>> SaveAsync(AddEditTripDTO request);
    }
}