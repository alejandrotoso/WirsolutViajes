using AutoMapper;
using WirsolutViajes.Application.Interfaces.Repositories;
using WirsolutViajes.Application.Interfaces.Services;
using WirsolutViajes.Shared.DTOs;
using WirsolutViajes.Shared.Wrapper;

namespace WirsolutViajes.Application.Services
{
    public class ValueService : IValueService
    {
        private readonly IVehicleTypeRepository _vehicleTypeRepository;
        private readonly IVehicleSubtypeRepository _vehicleSubtypeRepository;
        private readonly IBrandVehicleTypeRepository _brandVehicleTypeRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IModelRepository _modelRepository;


        public ValueService(IVehicleTypeRepository vehicleTypeRepository,
            IVehicleSubtypeRepository vehicleSubtypeRepository,
            IBrandVehicleTypeRepository brandVehicleTypeRepository,
            IBrandRepository brandRepository, 
            IModelRepository modeloRepository)
        {
            _vehicleTypeRepository = vehicleTypeRepository;
            _vehicleSubtypeRepository = vehicleSubtypeRepository;
            _brandVehicleTypeRepository = brandVehicleTypeRepository;
            _brandRepository = brandRepository;
            _modelRepository = modeloRepository;
        }


        public async Task<Result<IEnumerable<VehicleTypeDTO>>> GetAllVehicleTypesAsync()
        {
            return await _vehicleTypeRepository.GetAllAsync();
        }

        public async Task<Result<IEnumerable<VehicleSubtypeDTO>>> GetVehicleSubtypesByVehicleTypeIdAsync(int vehicleTypeId)
        {
            return await _vehicleSubtypeRepository.GetAllByVehicleTypeIdAsync(vehicleTypeId);
        }

        public async Task<Result<IEnumerable<BrandDTO>>> GetBrandsByVehicleTypeIdAsync(int vehicleTypeId)
        {
            return await _brandRepository.GetAllByVehicleTypeIdAsync(vehicleTypeId);
        }

        public async Task<Result<IEnumerable<ModelDTO>>> GetModelsByBrandVehicleTypeIdAsync(int brandId, int vehicleTypeId)
        {
            return await _modelRepository.GetAllByBrandVehicleTypeIdAsync(brandId, vehicleTypeId);
        }

        public async Task<Result<BrandVehicleTypeDTO>> GetBrandVehicleTypeByIdAsync(int id)
        {
            return await _brandVehicleTypeRepository.GetByIdAsync(id);
        }
    }
}

