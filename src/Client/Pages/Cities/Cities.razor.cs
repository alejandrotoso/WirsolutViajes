using MudBlazor;
using WirsolutViajes.Shared.DTOs;

namespace WirsolutViajes.Client.Pages.Cities
{
    public partial class Cities
    {
        private List<CityDTO> _citiesList = new();
        private CityDTO _city = new();
        private string _searchString = "";
        private bool _dense = true;
        private bool _striped = true;
        private bool _bordered = false;

        private bool _loaded;

        protected override async Task OnInitializedAsync()
        {

            await GetCitiesAsync();
            _loaded = true;
        }

        private async Task GetCitiesAsync()
        {
            var response = await _cityManager.GetAllAsync();

            if (response.Succeeded)
            {
                _citiesList = response.Data.ToList();
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
                _city = _citiesList.FirstOrDefault(c => c.Id == id);
                if (_city != null)
                {
                    parameters.Add(nameof(AddEditCityModal.AddEditCityModel), new AddEditCityDTO
                    {
                        Id = _city.Id,
                    });

                }
            }
            else
            {
                parameters.Add(nameof(AddEditCityModal.AddEditCityModel), new AddEditCityDTO
                {
                    Id = 0,
                });
            }

            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<AddEditCityModal>(id == 0 ? "Crear" : "Editar", parameters, options);
            var result = await dialog.Result;
            if (!result.Canceled)
            {
                await Reset();
            }
        }


        private async Task Delete(CityDTO selectedCity)
        {
            string title = $"¿Estás seguro de que deseas desactivar la ciudad '{selectedCity.Description}'? Ten en cuenta que este proceso se puede revertir.";
            string deleteContent = "¿Confirmar desactivación de la ciudad?";

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
                var city = _citiesList.FirstOrDefault(c => c.Id == selectedCity.Id);
                if (city != null)
                {
                    AddEditCityDTO addEditCityDTO = new AddEditCityDTO
                    {
                        Id = city.Id,
                        Name = city.Name,
                        Latitude = city.Latitude,
                        Longitude = city.Longitude,
                        Country = city.Country,

                        CountryISOCode = city.CountryISOCode,
                        State = city.State,

                        Active = false,
                        DeactivationComment = result.Data?.ToString() ?? null
                    };

                    var response = await _cityManager.SaveAsync(addEditCityDTO);
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

        private async Task Activate(CityDTO selectedCity)
        {
            string title = $"¿Estás seguro de que deseas activar la ciudad '{selectedCity.Description}'? Ten en cuenta que este proceso se puede revertir.";
            string deleteContent = "¿Confirmar activación de la ciudad?";

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
                var city = _citiesList.FirstOrDefault(c => c.Id == selectedCity.Id);
                if (city != null)
                {
                    AddEditCityDTO addEditCityDTO = new AddEditCityDTO
                    {
                        Id = city.Id,

                        Name = city.Name,
                        Latitude = city.Latitude,
                        Longitude = city.Longitude,
                        Country = city.Country,

                        CountryISOCode = city.CountryISOCode,
                        State = city.State,

                        Active = true,
                        DeactivationComment = result.Data?.ToString() ?? null
                    };

                    var response = await _cityManager.SaveAsync(addEditCityDTO);
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
            _city = new CityDTO();
            await GetCitiesAsync();
        }

        private bool Search(CityDTO itemToSearch)
        {
            if (string.IsNullOrWhiteSpace(_searchString))
                return true;
            if (itemToSearch.Name?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
                return true;
            if (itemToSearch.Country?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
                return true;
            if (itemToSearch.State?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
                return true;
            if (itemToSearch.DeactivationComment?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
                return true;
            if (itemToSearch.DeactivatedAt?.ToString("dd/MM/yyyy").Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
                return true;

            return false;
        }
    }
}
