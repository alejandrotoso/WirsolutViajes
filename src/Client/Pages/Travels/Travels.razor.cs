using MudBlazor;
using WirsolutViajes.Client.Infrastructure.Managers;
using WirsolutViajes.Shared.DTOs;

namespace WirsolutViajes.Client.Pages.Travels
{
    public partial class Travels
    {
        private List<TripDTO> _travelsList = new();
        private TripDTO _trip = new();
        private string _searchString = "";
        private bool _dense = true;
        private bool _striped = true;
        private bool _bordered = false;


        private DateTime _tripDate;
        private string _destinationId = "0";
        private string _previousDestinationId = "0";
        private string _vehicleTypeId = "0";
        private DateTime _dateNow;

        private List<CityDTO> _citiesList;
        private List<VehicleTypeDTO> _vehicleTypesList;

        private bool _loaded;

        protected override async Task OnInitializedAsync()
        {

            await LoadDataAsync();
            _dateNow = DateTime.Now;
            _tripDate = DateTime.Now;
            _loaded = true;
        }



        private async Task LoadDataAsync()
        {
            await LoadCitiesAsync();
            await LoadVehicleTypesAsync();

            await GetTripAsync();
        }

        private async Task LoadCitiesAsync()
        {
            var response = await _cityManager.GetAllActiveAsync();

            if (response.Succeeded)
            {
                _citiesList = response.Data.ToList();

                if (_citiesList.Count == 1)
                {
                    _destinationId = _citiesList.FirstOrDefault().Id.ToString();
                }
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

        private async Task LoadVehicleTypesAsync()
        {
            var response = await _valueManager.GetAllVehicleTypesAsync();

            if (response.Succeeded)
            {
                _vehicleTypesList = response.Data.ToList();
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
        private async Task GetTripAsync()
        {
            var response = await _tripManager.GetAllAsync(_tripDate, int.Parse(_destinationId), int.Parse(_vehicleTypeId));

            if (response.Succeeded)
            {
                _travelsList = response.Data.ToList();
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
                _trip = _travelsList.FirstOrDefault(c => c.Id == id);
                if (_trip != null)
                {
                    parameters.Add(nameof(AddEditTripModal.AddEditTripModel), new AddEditTripDTO
                    {
                        Id = _trip.Id,

                        TripDate = _trip.TripDate,
                        DestinationId = _trip.DestinationId,
                        VehicleId = _trip.VehicleId,
                    });

                }
            }
            else
            {
                parameters.Add(nameof(AddEditTripModal.AddEditTripModel), new AddEditTripDTO
                {
                    Id = 0,
                });

            }

            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<AddEditTripModal>(id == 0 ? "Crear" : "Editar", parameters, options);
            var result = await dialog.Result;
            if (!result.Canceled)
            {
                await Reset();
            }
        }


        private async Task Delete(TripDTO selectedTrip)
        {
            string title = $"¿Estás seguro de que deseas eliminar el viaje a ({selectedTrip.Destination.Description})? Ten en cuenta que este proceso no se puede revertir.";
            string deleteContent = "¿Confirmar la eliminación del viaje?";

            var parameters = new DialogParameters
            {
                {nameof(Shared.Dialogs.DeleteConfirmation.ContentTitle), title},
                {nameof(Shared.Dialogs.DeleteConfirmation.ContentText), deleteContent},
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
                var response = await _tripManager.DeleteAsync(selectedTrip.Id);
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


        private bool CheckIsDateToDisabled(DateTime date)
        {
            DateTime currentDate = DateTime.Now.Date;
            DateTime futureDate = currentDate.AddDays(5);


            return date < _dateNow.Date || date > futureDate;
        }

        private async Task HandleDateToChanged(DateTime? newDate)
        {
            if (newDate.HasValue)
            {
                if (_tripDate.Date != newDate.Value.Date)
                {
                    _tripDate = newDate.Value;

                    if (int.Parse(_destinationId) != 0)
                    {
                        await GetTripAsync();
                    }
                }
            }
        }

        private async Task OnDestinoSelected(IEnumerable<string> selectedValues)
        {
            var selectedId = int.Parse(selectedValues.FirstOrDefault());

            if (_destinationId != _previousDestinationId)
            {
                _previousDestinationId = _destinationId;
                await GetTripAsync();
            }
        }

        private async Task OnVehicleTypeSelected(IEnumerable<string> selectedValues)
        {
            //var selectedVehicleTypeId = int.Parse(selectedValues.FirstOrDefault());
            await GetTripAsync();
        }

        private async Task Reset()
        {
            _trip = new TripDTO();
            await GetTripAsync();
        }

        private async Task<IEnumerable<int>> SearchCities(string value)
        {
            if (string.IsNullOrEmpty(value))
                return _citiesList.Select(x => x.Id);

            return _citiesList.Where(x => x.Description.Contains(value, StringComparison.OrdinalIgnoreCase))
                .Select(x => x.Id);
        }

        private bool Search(TripDTO itemToSearch)
        {
            if (string.IsNullOrWhiteSpace(_searchString))
                return true;

            if (itemToSearch.TripDate.Date.ToString("dd/MM/aaaa")?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
                return true;
            if (itemToSearch.Destination.Description?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
                return true;
            if (itemToSearch.Vehicle.Description?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
                return true;

            return false;
        }
    }
}
