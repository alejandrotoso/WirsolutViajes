using MudBlazor;

namespace WirsolutViajes.Client.Services
{
    public class NotificationService
    {
        private readonly ISnackbar _snackbar;

        public NotificationService(ISnackbar snackbar)
        {
            _snackbar = snackbar;
        }

        public void ShowErrorMessages<T>(T response)
        {
            if (response is string errorMessage)
            {
                _snackbar.Add(errorMessage, Severity.Error);
            }
            else if (response is IEnumerable<string> errorMessages)
            {
                foreach (var message in errorMessages)
                {
                    _snackbar.Add(message, Severity.Error);
                }
            }
        }
    }
}
