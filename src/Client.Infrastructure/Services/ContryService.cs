using Nager.Country;
using Nager.Country.Translation;

namespace WirsolutViajes.Client.Infrastructure.Services
{
    public class ContryService : IContryService
    {
        private readonly CountryProvider _countryProvider;
        private readonly TranslationProvider _translationProvider;

        public ContryService()
        {
            _countryProvider = new CountryProvider();
            _translationProvider = new TranslationProvider();
        }

        public string ObtenerNombrePais(string codigoPais)
        {
            //var country = _countryProvider.GetCountry(codigoPais);

            var contryName = _translationProvider.GetCountryTranslatedName(codigoPais, LanguageCode.ES);

            return contryName;
        }
    }
}
