﻿using CoreZipCode.Interfaces;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace CoreZipCode.Services.ViaCepApi
{
    public class ViaCep : ZipCodeBaseService
    {
        public ViaCep(HttpClient request) : base(request)
        {
            //
        }

        public override string SetZipCodeUrl(string zipcode) => $"https://viacep.com.br/ws/{ValidateZipCode(zipcode)}/json/";

        public override string SetZipCodeUrlBy(string state, string city, string street) => $"https://viacep.com.br/ws/{ValidateParam("State", state, 2)}/{ValidateParam("City", city)}/{ValidateParam("Street", street)}/json/";

        public static string ValidateParam(string name, string value, int size = 3)
        {
            value = value.Trim();

            if (string.IsNullOrEmpty(value) || value.Length < size)
            {
                throw new ViaCepException($"Invalid {name} Param");
            }

            return value;
        }

        public static string ValidateZipCode(string zipcode)
        {
            zipcode = zipcode.Trim().Replace("-", "");

            if (zipcode.Length != 8)
            {
                throw new ViaCepException("Invalid ZipCode Size");
            }

            if (!Regex.IsMatch(zipcode, ("[0-9]{8}")))
            {
                throw new ViaCepException("Invalid ZipCode Format");
            }

            return zipcode;
        }
    }
}