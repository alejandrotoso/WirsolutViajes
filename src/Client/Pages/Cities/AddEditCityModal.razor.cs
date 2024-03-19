using Microsoft.AspNetCore.Components;
using MudBlazor;
using WirsolutViajes.Client.Infrastructure.DTOs;
using WirsolutViajes.Shared.DTOs;


namespace WirsolutViajes.Client.Pages.Cities
{
    public partial class AddEditCityModal
    {
        [Parameter] public AddEditCityDTO AddEditCityModel { get; set; } = new();
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }


        private bool _success;
        private string[] _errors = { };
        private MudForm _form;
        private Dictionary<string, string> _fieldErrors = new Dictionary<string, string>();

        private List<LocationResponse> _locationResponseList = new();
        private LocationResponse _locationResponse = new();

        private string _cityQuery= string.Empty;

        private bool _loaded;


        protected override async Task OnInitializedAsync()
        {
            await LoadDataAsync();
            _loaded = true;
        }


        public void Cancel()
        {
            MudDialog.Cancel();
        }

        private async Task LoadDataAsync()
        {
        }


        private async Task OnLocationSelected(IEnumerable<string> selectedValues)
        {
            var selectedLocationId = selectedValues.FirstOrDefault();

            var selectedLocation = _locationResponseList.Where(x => x.Id == selectedLocationId).FirstOrDefault();

            AddEditCityModel.Name = selectedLocation.Name;
            AddEditCityModel.Latitude = selectedLocation.Lat;
            AddEditCityModel.Longitude = selectedLocation.Lon;
            AddEditCityModel.Country = selectedLocation.CountryName;
            AddEditCityModel.CountryISOCode = selectedLocation.Country;
            AddEditCityModel.State = selectedLocation.State;
        }

        private async Task SaveAsync()
        {
            // validar que no exista la city
            AddEditCityModel.Active = true;
            var response = await _cityManager.SaveAsync(AddEditCityModel);
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

        private async Task ValidateForm()
        {
            _fieldErrors.Clear();
            _form.Validate();

            if (_success)
            {
                await SaveAsync();
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

        private async Task GetCitiesAsync()
        {
            _fieldErrors.Clear();
            if(_cityQuery.Length<1)
            {
                _fieldErrors.Add(nameof(_cityQuery), "Por favor, ingrese el nombre de la ciudad antes de realizar la búsqueda.");
            }
            else
            {
                _locationResponseList = await _weatherAPIService.GetLocations(_cityQuery);
            }
        }
    }
}
