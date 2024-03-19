using Microsoft.AspNetCore.Components;
using MudBlazor;
using WirsolutViajes.Client.Infrastructure.DTOs;
using WirsolutViajes.Client.Services;
using WirsolutViajes.Shared.DTOs;
using WirsolutViajes.Shared.Wrapper;


namespace WirsolutViajes.Client.Pages.Travels
{
    public partial class AddEditTripModal
    {
        [Parameter] public AddEditTripDTO AddEditTripModel { get; set; } = new();
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }


        private bool _success;
        private string[] _errors = { };
        private MudForm _form;
        private Dictionary<string, string> _fieldErrors = new Dictionary<string, string>();


        private List<CityDTO> _citiesList;
        private List<VehicleTypeDTO> _vehicleTypesList;
        private List<VehicleSubtypeDTO> _vehicleSubtypesList;
        private List<VehicleDTO> _vehiclesList;

        private DateTime _dateNow;

        private string _vehicleTypeId;
        private string _subtypeVehicleId;


        private List<DailyForecast> _dailyForecastList;
        private DailyForecast _dayForecast;

        private int _previousDestinationId;

        private bool _isInitialLoadUpdate;

        private bool _loaded;

        protected override async Task OnInitializedAsync()
        {
            await LoadDataAsync();

            _loaded = true;
            _isInitialLoadUpdate = false;
        }

        public void Cancel()
        {
            MudDialog.Cancel();
        }

        private async Task LoadDataAsync()
        {
            _dateNow = DateTime.Now;
            _previousDestinationId = AddEditTripModel.DestinationId;


            await LoadCitiesAsync();
            await LoadVehicleTypesAsync();


            if (AddEditTripModel.Id == 0)
            {
                AddEditTripModel.TripDate = _dateNow.Date;
            }
            else if (AddEditTripModel.Id > 0)
            {
                _isInitialLoadUpdate = true;

                await GetForecast();

                var response = await _vehicleManager.GetByIdAsync(AddEditTripModel.VehicleId);

                if (response.Succeeded)
                {
                    var vehicle = response.Data;

                    _vehicleTypeId = vehicle.BrandVehicleType.VehicleTypeId.ToString();

                    List<string> selectedValues = new List<string> { _vehicleTypeId };

                    await OnVehicleTypeSelected(selectedValues);
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

        private async Task LoadCitiesAsync()
        {
            var response = await _cityManager.GetAllActiveAsync();

            if (response.Succeeded)
            {
                _citiesList = response.Data.ToList();

                if (_citiesList.Count == 1)
                {
                    AddEditTripModel.DestinationId = _citiesList.FirstOrDefault().Id;
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


        private async Task OnVehicleTypeSelected(IEnumerable<string> selectedValues)
        {
            var selectedVehicleTypeId = int.Parse(selectedValues.FirstOrDefault());

            await LoadVehicleSubtypesAsync(selectedVehicleTypeId);
            await LoadVehiclesAsync(selectedVehicleTypeId);
        }


        private async Task OnSubtypeVehicleSelected(IEnumerable<string> selectedValues)
        {
            await LoadVehiclesAsync(int.Parse(_vehicleTypeId));
        }

        private async Task LoadVehicleSubtypesAsync(int selectedVehicleTypeId)
        {
            var response = await _valueManager.GetSubtypeVehicleByVehicleTypeIdAsync(selectedVehicleTypeId);

            if (response.Succeeded)
            {
                _vehicleSubtypesList = response.Data.ToList();

                if (_vehicleSubtypesList.Count == 1)
                {
                    _subtypeVehicleId = _vehicleSubtypesList.FirstOrDefault().Id.ToString();
                }
                else
                {
                    _subtypeVehicleId = "0";
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


        private async Task LoadVehiclesAsync(int selectedVehicleTypeId)
        {
            var subtypeVehicleId = string.IsNullOrEmpty(_subtypeVehicleId) ? 0 : int.Parse(_subtypeVehicleId);

            var response = await _vehicleManager.GetByTypeAndVehicleSubtypeIdAsync(selectedVehicleTypeId, subtypeVehicleId); //GetBrandsByVehicleTypeIdAsync(selectedVehicleTypeId);


            if (response.Succeeded)
            {
                _vehiclesList = response.Data.ToList();
                //_modelsList = null;
                //AddEditVehicleModel.ModelId = "";
                //AddEditVehicleModel.BrandVehicleTypeId = 0;

                if (_vehiclesList.Count == 1)
                {
                    //AddEditVehicleModel.BrandVehicleType.BrandId = _brandsList.FirstOrDefault().Id;
                    AddEditTripModel.VehicleId = _vehiclesList.FirstOrDefault().Id;
                }
                else if (!_isInitialLoadUpdate)
                {
                    AddEditTripModel.VehicleId = 0;
                }
                //StateHasChanged();
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

        private async Task ValidateForm()
        {
            _fieldErrors.Clear();
            _form.Validate();


            if (AddEditTripModel.DestinationId == 0)
            {
                _fieldErrors.Add(nameof(AddEditTripModel.DestinationId), "Por favor, selecciona un destino.");
                _success = false;
            }
            if (AddEditTripModel.VehicleId == 0)
            {
                _fieldErrors.Add(nameof(AddEditTripModel.VehicleId), "Por favor, selecciona un vehiculo.");
                _success = false;
            }

            if (_success)
            {
                await SaveAsync();
            }
        }

        private async Task SaveAsync()
        {
            // validar que no exista la city

            var response = await _tripManager.SaveAsync(AddEditTripModel);
            if (response.Succeeded)
            {
                _snackBar.Add(response.Messages[0], MudBlazor.Severity.Success);
                MudDialog.Close();
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

        private bool GetFieldError(string fieldName, out string errorMessage)
        {
            if (_fieldErrors.TryGetValue(fieldName, out errorMessage))
            {
                return true;
            }

            errorMessage = null;
            return false;
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
                if (AddEditTripModel.TripDate.Value.Date != newDate.Value.Date)
                {
                    AddEditTripModel.TripDate = newDate.Value;

                    if (AddEditTripModel.DestinationId != 0)
                    {
                        await GetForecast();
                    }
                }
            }
        }

        private async Task HandleChangeDestino()
        {
            if (AddEditTripModel.DestinationId != _previousDestinationId)
            {
                _previousDestinationId = AddEditTripModel.DestinationId;
                await GetForecast();
            }
        }


        private async Task GetForecast()
        {
            var city = _citiesList.Where(x => x.Id == AddEditTripModel.DestinationId).FirstOrDefault();


            _dailyForecastList = await _weatherAPIService.GetFiveDayForecast(city.Latitude, city.Longitude);

            if (_dailyForecastList == null || _dailyForecastList.Count == 0)
            {
                var message = $"No se encontraron datos del clima para {city.Description} Latitud: {city.Latitude} Longitud: {city.Longitude}";
                _snackBar.Add(message, MudBlazor.Severity.Error);
            }
            else
            {
                StateHasChanged();
                _dayForecast = _dailyForecastList.Where(x => x.Date.Date == AddEditTripModel.TripDate.Value.Date).FirstOrDefault();

                if (_dayForecast == null)
                {
                    var message = $"No se encontraron datos del clima para el dia {AddEditTripModel.TripDate?.ToString("dd/MM/yyyy")}";
                    _snackBar.Add(message, MudBlazor.Severity.Error);
                }
            }

        }

        private async Task<IEnumerable<int>> SearchCities(string value)
        {
            if (string.IsNullOrEmpty(value))
                return _citiesList.Select(x => x.Id);

            return _citiesList.Where(x => x.Description.Contains(value, StringComparison.OrdinalIgnoreCase))
                .Select(x => x.Id);
        }

        private async Task<IEnumerable<int>> SearchVehiculos(string value)
        {
            if (string.IsNullOrEmpty(value))
                return _vehiclesList.Select(x => x.Id);

            return _vehiclesList.Where(x => x.Description.Contains(value, StringComparison.OrdinalIgnoreCase))
                .Select(x => x.Id);
        }
    }
}
