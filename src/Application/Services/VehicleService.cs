using WirsolutViajes.Application.Interfaces.Repositories;
using WirsolutViajes.Application.Interfaces.Services;
using WirsolutViajes.Shared.DTOs;
using WirsolutViajes.Shared.Wrapper;

namespace WirsolutViajes.Application.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<Result<IEnumerable<VehicleDTO>>> GetAllVehiclesAsync()
        {
            return await _vehicleRepository.GetAllAsync();
        }

        public async Task<Result<VehicleDTO>> GetByIdAsync(int id)
        {
            return await _vehicleRepository.GetByIdAsync (id);
        }

        public async Task<Result<IEnumerable<VehicleDTO>>> GetByTypeAndVehicleSubtypeIdAsync(int vehicleTypeId, int vehicleSubtypeId)
        {
            return await _vehicleRepository.GetByTypeAndVehicleSubtypeIdAsync(vehicleTypeId, vehicleSubtypeId);
        }

        public async Task<Result<int>> SaveAsync(AddEditVehicleDTO addEditVehicleDTO)
        {
            return await _vehicleRepository.SaveAsync(addEditVehicleDTO);

        }
    }
}

