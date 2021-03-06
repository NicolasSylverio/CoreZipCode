using System;
using System.Net.Http;
using CoreZipCode.Interfaces;
using System.Text.RegularExpressions;

namespace CoreZipCode.Services.ZipCode.SmartyStreetsApi
{
    public class SmartyStreets : ZipCodeBaseService
    {
        private const string ZipCodeSizeErrorMessage = "Invalid ZipCode Size";
        private const string ZipCodeFormatErrorMessage = "Invalid ZipCode Format";

        private const string BaseZipcodeUrl = "https://us-zipcode.api.smartystreets.com/lookup";
        private const string BaseStreetUrl = "https://us-street.api.smartystreets.com/street-address";

        private readonly string _authId;
        private readonly string _authToken;

        public SmartyStreets(string authId, string authToken)
        {
            _authId = string.IsNullOrWhiteSpace(authId) ? throw new ArgumentNullException(nameof(authId)) : authId;
            _authToken = string.IsNullOrWhiteSpace(authToken) ? throw new ArgumentNullException(nameof(authToken)) : authToken;
        }

        public SmartyStreets(HttpClient request, string authId, string authToken) : base(request)
        {
            _authId = string.IsNullOrWhiteSpace(authId) ? throw new ArgumentNullException(nameof(authId)) : authId;
            _authToken = string.IsNullOrWhiteSpace(authToken) ? throw new ArgumentNullException(nameof(authToken)) : authToken;
        }

        public override string SetZipCodeUrl(string zipcode) => $"{BaseZipcodeUrl}?auth-id={_authId}&auth-token={_authToken}&zipcode={ValidateZipCode(zipcode)}";

        public override string SetZipCodeUrlBy(string state, string city, string street) => $"{BaseStreetUrl}?auth-id={_authId}&auth-token={_authToken}&street={ValidateParam("Street", street)}&city={ValidateParam("City", city)}&state={ValidateParam("State", state, 32)}&candidates=10";

        private static string ValidateParam(string name, string value, int size = 64)
        {
            if (value.Length > size)
            {
                throw new SmartyStreetsException($"Invalid {name}, parameter over size of {size.ToString()} characters.");
            }

            return value.Trim();
        }

        private static string ValidateZipCode(string zipCode)
        {
            zipCode = zipCode.Trim().Replace("-", "");

            if (zipCode.Length < 5 || zipCode.Length > 16)
            {
                throw new SmartyStreetsException(ZipCodeSizeErrorMessage);
            }

            if (!Regex.IsMatch(zipCode, ("[0-9]{5,16}")))
            {
                throw new SmartyStreetsException(ZipCodeFormatErrorMessage);
            }

            return zipCode;
        }
    }
}