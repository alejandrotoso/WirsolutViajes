using MudBlazor;
using System.Globalization;
using WirsolutViajes.Shared.DTOs;

namespace WirsolutViajes.Client.Pages.Vehicles
{
    public partial class Vehicles
    {
        private List<VehicleDTO> _vehiclesList = new();
        private VehicleDTO _vehicle = new();
        private string _searchString = "";
        private bool _dense = true;
        private bool _striped = true;
        private bool _bordered = false;

        private bool _loaded;

        protected override async Task OnInitializedAsync()
        {

            await GetVehicleAsync();
            _loaded = true;
        }

        private async Task GetVehicleAsync()
        {
            var response = await _vehicleManager.GetAllAsync();

            if (response.Succeeded)
            {
                _vehiclesList = response.Data.ToList();
            }
            else
            {
                _notificationService.ShowErrorMessages(response.Messages);
                //foreach (var message in response.Messages)
                //{
                //    _snackBar.Add(message, Severity.Error);
                //}
            }
        }

        private async Task InvokeModal(int id = 0)
        {
            var parameters = new DialogParameters();
            if (id != 0)
            {
                _vehicle = _vehiclesList.FirstOrDefault(c => c.Id == id);
                if (_vehicle != null)
                {
                    parameters.Add(nameof(AddEditVehicleModal.AddEditVehicleModel), new AddEditVehicleDTO
                    {
                        Id = _vehicle.Id,
                        LicensePlate = _vehicle.LicensePlate,
                        BrandVehicleTypeId = _vehicle.BrandVehicleTypeId,
                        VehicleSubtypeId = _vehicle.VehicleSubtypeId.ToString(),
                        ModelId = _vehicle.ModelId.ToString(),

                        YearManufactured = _vehicle.YearManufactured,
                        Mileage = _vehicle.Mileage,
                        Active = _vehicle.Active,
                    });

                    //parameters.Add(nameof(AddEditVehiculoModal.ProvinciaId), _currentUser.GetProvinciaId());
                }
            }
            else
            {
                parameters.Add(nameof(AddEditVehicleModal.AddEditVehicleModel), new AddEditVehicleDTO
                {
                    Id = 0,
                });
            }

            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<AddEditVehicleModal>(id == 0 ? "Crear" : "Editar", parameters, options);
            var result = await dialog.Result;
            if (!result.Canceled)
            {
                await Reset();
            }
        }

        private async Task Delete(VehicleDTO selectedVehicle)
        {
            string title = $"¿Estás seguro de que deseas desactivar el vehículo ({selectedVehicle.Description})? Ten en cuenta que este proceso se puede revertir.";
            string deleteContent = "¿Confirmar desactivación del vehículo?";


            var parameters = new DialogParameters
            {
                {nameof(Shared.Dialogs.DeleteConfirmation.ContentTitle), title},
                {nameof(Shared.Dialogs.DeleteConfirmation.ContentText), deleteContent},
                {nameof(Shared.Dialogs.DeleteConfirmation.Active), true}
            };

            var options = new DialogOptions
            {
                CloseButton = false,
                MaxWidth = MaxWidth.Small,
                FullWidth = true,
                DisableBackdropClick = true,
                CloseOnEscapeKey = true
            };
            var dialog = _dialogService.Show<Shared.Dialogs.DeleteConfirmation>("Eliminar", parameters, options);
            var result = await dialog.Result;


            if (!result.Canceled)
            {
                var vehicle = _vehiclesList.FirstOrDefault(c => c.Id == selectedVehicle.Id);
                if (vehicle != null)
                {
                    AddEditVehicleDTO addEditVehicleDTO = new AddEditVehicleDTO
                    {
                        Id = vehicle.Id,

                        LicensePlate = vehicle.LicensePlate,
                        BrandVehicleTypeId = vehicle.BrandVehicleTypeId,
                        VehicleSubtypeId = vehicle.VehicleSubtypeId.ToString(),
                        ModelId = vehicle.ModelId.ToString(),

                        YearManufactured = vehicle.YearManufactured,
                        Mileage = vehicle.Mileage,

                        Active = false,
                        DeactivationComment = result.Data?.ToString() ?? null,
                    };

                    var response = await _vehicleManager.SaveAsync(addEditVehicleDTO);
                    if (response.Succeeded)
                    {
                        await Reset();
                        _snackBar.Add(response.Messages[0], MudBlazor.Severity.Success);
                    }
                    else
                    {
                        _notificationService.ShowErrorMessages(response.Messages);
                        //foreach (var message in response.Messages)
                        //{
                        //    _snackBar.Add(message, MudBlazor.Severity.Error);
                        //}
                    }
                }
            }
        }

        private async Task Activate(VehicleDTO selectedVehicle)
        {
            string title = $"¿Estás seguro de que deseas activar el vehículo ({selectedVehicle.Description})? Ten en cuenta que este proceso se puede revertir.";
            string deleteContent = "¿Confirmar activación del vehículo?";

            var parameters = new DialogParameters
            {
                {nameof(Shared.Dialogs.ActivateConfirmation.ContentTitle), title},
                {nameof(Shared.Dialogs.ActivateConfirmation.ContentText), deleteContent},
            };

            var options = new DialogOptions
            {
                CloseButton = false,
                MaxWidth = MaxWidth.Small,
                FullWidth = true,
                DisableBackdropClick = true,
                CloseOnEscapeKey = true
            };
            var dialog = _dialogService.Show<Shared.Dialogs.ActivateConfirmation>("Activar", parameters, options);
            var result = await dialog.Result;


            if (!result.Canceled)
            {
                var vehicle = _vehiclesList.FirstOrDefault(c => c.Id == selectedVehicle.Id);
                if (vehicle != null)
                {
                    AddEditVehicleDTO addEditVehicleDTO = new AddEditVehicleDTO
                    {
                        Id = vehicle.Id,
                        LicensePlate = vehicle.LicensePlate,
                        BrandVehicleTypeId = vehicle.BrandVehicleTypeId,
                        VehicleSubtypeId = vehicle.VehicleSubtypeId.ToString(),
                        ModelId = vehicle.ModelId.ToString(),

                        YearManufactured = vehicle.YearManufactured,
                        Mileage = vehicle.Mileage,

                        Active = true,
                    };

                    var response = await _vehicleManager.SaveAsync(addEditVehicleDTO);
                    if (response.Succeeded)
                    {
                        await Reset();
                        _snackBar.Add(response.Messages[0], MudBlazor.Severity.Success);
                    }
                    else
                    {
                        _notificationService.ShowErrorMessages(response.Messages);
                        //foreach (var message in response.Messages)
                        //{
                        //    _snackBar.Add(message, MudBlazor.Severity.Error);
                        //}
                    }
                }
            }
        }

        private async Task Reset()
        {
            _vehicle = new VehicleDTO();
            await GetVehicleAsync();
        }

        private bool Search(VehicleDTO itemToSearch)
        {
            if (string.IsNullOrWhiteSpace(_searchString))
                return true;
            if (itemToSearch.BrandVehicleType.VehicleType.Name?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
                return true;
            if (itemToSearch.VehicleSubtype.Name?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
                return true;
            if (itemToSearch.BrandVehicleType.Brand.Name?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
                return true;
            if (itemToSearch.Model.Name?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
                return true;
            if (itemToSearch.LicensePlate?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
                return true;
            if (itemToSearch.YearManufactured.ToString().Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
                return true;
            if (itemToSearch.Mileage.ToString().Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
                return true;
            if (itemToSearch.DeactivationComment?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
                return true;
            if (itemToSearch.DeactivatedAt?.ToString("dd/MM/yyyy").Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
                return true;


            return false;
        }
    }
}
