using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Drawing;
using WirsolutViajes.Shared.DTOs;


namespace WirsolutViajes.Client.Pages.Vehicles
{
    public partial class AddEditVehicleModal
    {
        [Parameter] public AddEditVehicleDTO AddEditVehicleModel { get; set; } = new();
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }


        private bool _success;
        private string[] _errors = { };
        private MudForm _form;
        private Dictionary<string, string> _fieldErrors = new Dictionary<string, string>();


        private List<VehicleTypeDTO> _vehicleTypesList;
        private List<VehicleSubtypeDTO> _vehicleSubtypesList;
        private List<BrandDTO> _brandsList;
        private List<ModelDTO> _modelsList;

        private string _vehicleTypeId;
        private string _brandId;


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
            await LoadVehicleTypesAsync();

            if (AddEditVehicleModel.Id > 0)
            {
                _isInitialLoadUpdate = true;
                var response = await _valueManager.GetBrandVehicleTypeByIdAsync(AddEditVehicleModel.BrandVehicleTypeId);

                if (response.Succeeded)
                {
                    var brandVehicleType = response.Data;


                    _vehicleTypeId = brandVehicleType.VehicleTypeId.ToString();
                    _brandId = brandVehicleType.BrandId.ToString();

                    List<string> selectedValues = new List<string> { _vehicleTypeId };

                    // Llamar al método con la lista creada
                    await OnVehicleTypeSelected(selectedValues);

                    List<string> selectedValuesBrand = new List<string> { _brandId };

                    await OnBrandSelected(selectedValuesBrand);

                }
                else
                {
                    _notificationService.ShowErrorMessages(response.Messages);
                    //foreach (var message in response.Messages)
                    //{
                    //    _snackBar.Add(message, MudBlazor.Severity.Error);
                    //}
                }
                _isInitialLoadUpdate = false;
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
            await LoadMarcasAsync(selectedVehicleTypeId);
        }

        private async Task LoadVehicleSubtypesAsync(int selectedVehicleTypeId)
        {
            var response = await _valueManager.GetSubtypeVehicleByVehicleTypeIdAsync(selectedVehicleTypeId);

            if (response.Succeeded)
            {
                _vehicleSubtypesList = response.Data.ToList();

                if (_vehicleSubtypesList.Count == 1)
                {
                    AddEditVehicleModel.VehicleSubtypeId = _vehicleSubtypesList.FirstOrDefault().Id.ToString();
                }
                else if (!_isInitialLoadUpdate)
                {
                    AddEditVehicleModel.VehicleSubtypeId = "";
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

        private async Task LoadMarcasAsync(int selectedVehicleTypeId)
        {
            var response = await _valueManager.GetBrandsByVehicleTypeIdAsync(selectedVehicleTypeId);

            if (response.Succeeded)
            {
                _brandsList = response.Data.ToList();
                _modelsList = null;

                if (!_isInitialLoadUpdate)
                {
                    AddEditVehicleModel.ModelId = "";
                    AddEditVehicleModel.BrandVehicleTypeId = 0;
                }

                if (_brandsList.Count == 1)
                {
                    _brandId = _brandsList.FirstOrDefault().Id.ToString();
                }
                else if (!_isInitialLoadUpdate)
                {
                    _brandId = "";
                }
                StateHasChanged();
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


        private async Task OnBrandSelected(IEnumerable<string> selectedValues)
        {
            var selectedBrandId = int.Parse(selectedValues.FirstOrDefault());

            await LoadModelosAsync(selectedBrandId);

            var marca = _brandsList.Where(x => x.Id == selectedBrandId).FirstOrDefault();


            if (marca != null)
            {
                AddEditVehicleModel.BrandVehicleTypeId = marca.BrandVehicleTypeId;
            }
            else
            {
                AddEditVehicleModel.BrandVehicleTypeId = 0;
            }

        }


        private async Task LoadModelosAsync(int selectedBrandId)
        {
            var response = await _valueManager.GetModelsByBrandVehicleTypeIdAsync(selectedBrandId, int.Parse(_vehicleTypeId));

            if (response.Succeeded)
            {
                _modelsList = response.Data.ToList();
                if (_modelsList.Count == 1)
                {
                    AddEditVehicleModel.ModelId = _modelsList.FirstOrDefault().Id.ToString();
                }
                else if (!_isInitialLoadUpdate)
                {
                    AddEditVehicleModel.ModelId = "";
                }
                StateHasChanged();
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


        private async Task SaveAsync()
        {

            var response = await _vehicleManager.SaveAsync(AddEditVehicleModel);
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

            if (AddEditVehicleModel.YearManufactured == 0)
            {
                _fieldErrors.Add(nameof(AddEditVehicleModel.YearManufactured), "Por favor selecciona un Año de Fabricación.");
                _success = false;
            }
            else if (AddEditVehicleModel.YearManufactured > DateTime.Now.Year)
            {
                _fieldErrors.Add(nameof(AddEditVehicleModel.YearManufactured), "El año de fabricación no puede ser mayor al año en curso.");
                _success = false;
            }
            else if (AddEditVehicleModel.YearManufactured < 1900)
            {
                _fieldErrors.Add(nameof(AddEditVehicleModel.YearManufactured), "El año de fabricación no puede ser menor al año 1900.");
                _success = false;
            }

            if (AddEditVehicleModel.Mileage == 0)
            {
                _fieldErrors.Add(nameof(AddEditVehicleModel.Mileage), "Por favor ingrese el Kilometraje.");
                _success = false;
            }

            _success = ValidarPatente(AddEditVehicleModel.LicensePlate, _success);


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


        public bool ValidarPatente(string patente, bool success)
        {
            if (string.IsNullOrEmpty(patente))
            {
                _fieldErrors.Add(nameof(AddEditVehicleModel.LicensePlate), "La patente es requerida.");
                return false;
            }
            // Validar longitud mínima y máxima
            if (patente.Length < 3 || patente.Length > 8)
            {
                _fieldErrors.Add(nameof(AddEditVehicleModel.LicensePlate), "Longitud de patente incorrecta");
                return false;
            }

            // Caracteres permitidos por región
            string caracteresAmerica = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string caracteresEuropa = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-";

            // Validar caracteres permitidos
            bool esPatenteAmerica = patente.All(c => caracteresAmerica.Contains(char.ToUpper(c)));
            bool esPatenteEuropa = patente.All(c => caracteresEuropa.Contains(char.ToUpper(c)));

            if (!esPatenteAmerica && !esPatenteEuropa)
            {
                _fieldErrors.Add(nameof(AddEditVehicleModel.LicensePlate), "No es un formato válido de patente.");
                return false;
            }

            // Validar símbolos prohibidos (si es necesario)
            string simbolosProhibidos = "$#@!%&";
            if (patente.Any(c => simbolosProhibidos.Contains(c)))
            {
                _fieldErrors.Add(nameof(AddEditVehicleModel.LicensePlate), "Caracteres no válidos.");
                return false;
            }

            return success;
        }
    }
}
