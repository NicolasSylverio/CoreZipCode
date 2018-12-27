# CoreZipCode

[![Build Status](https://travis-ci.com/danilolutz/CoreZipCode.svg?branch=master)](https://travis-ci.com/danilolutz/CoreZipCode)
[![License: MIT](https://img.shields.io/badge/License-MIT-428f7e.svg)](https://opensource.org/licenses/MIT)
[![CoreZipCode Nuget Package](https://img.shields.io/nuget/v/CoreZipCode.svg)](https://www.nuget.org/packages/CoreZipCode/)

## Overview

> **WARNING**: Early stage development. Not for production use yet.

This package is supposed to be used for easily implements address services to yours [Microsoft .Net Core](https://dotnet.github.io/) based software.

Also the **CoreZipCode** are designed to be easily extensible, and if you want, implement your own address services.

We follow the [Semantic Versioning](https://semver.org), so check the package compatibility before.

## :sunglasses: Get Started

Well, you can download the code and build it by yourself or you can install by [Nuget](https://www.nuget.org) package in: [CoreZipCode Package](https://www.nuget.org/packages/CoreZipCode/).

Just in case if you doesn't want leave GitHub:

```bash
$ dotnet add package CoreZipCode
```

After you decide how you'll get the CoreZipCode, just inject and use it.

```CSharp
using CoreZipCode.Interfaces;
using CoreZipCode.Services;

namespace YouProject
{
    public YourClass
    {
        private readonly ZipCodeBaseService _coreZipCode;

        public YourClass(ZipCodeBaseService coreZipCode)
        {
            _coreZipCode = coreZipCode;
        }

        public void YourMethod() 
        {
            var addressByZipCode = _coreZipCode.Execute("14810100");
            var zipCodeByAddress = _coreZipCode.Execute("sp", "araraquara", "barão do rio");

            // Generic type return was added in version 1.2.0
            var addressByZipCodeObject = _coreZipCode.GetAddress<ViaCepAddress>("14810100");
            var zipCodeByAddressObjectList = _coreZipCode.ListAddresses<ViaCepAddress>("sp", "araraquara", "barão do rio");
        }
    }
}
```

The `ViaCepAddress` POCO class is the type for returned JSON from [ViaCep](https://viacep.com.br) brazilian service. So you will must to do the type for your prefered service.

> **NOTE**: We have only brazilian address service working out-the-box in this moment. We intent add the USA service coming soon.

### Extending CoreZipCode

Also you can extends ```ZipCodeBaseService``` abstract class and create your own implementation of your prefered address service. Like below:

```CSharp
using CoreZipCode.Interfaces;

namespace CoreZipCode.Services
{
    public class ViaCep : ZipCodeBaseService
    {
        public override string SetZipCodeUrl(string zipcode)
        {
            zipcode = zipcode.Replace("-", "");
            return $"https://viacep.com.br/ws/{zipcode}/json/";
        }

        public override string SetZipCodeUrlBy(string state, string city, string street) => $"https://viacep.com.br/ws/{state}/{city}/{street}/json/";
    }
}
```

## :heavy_check_mark: Available Services

Below a list of available services out-of-the-box.

| Service | Country |
| ------------- | -------- |
| [ViaCep](https://viacep.com.br) | Brazil |

## :construction_worker: Contributing

Thank you for considering contributing to the CodeZipCore! Just see our [Contributing Guide](CONTRIBUTING.md).

### :innocent: Code of Conduct

In order to ensure that the CodeZipCore community is welcoming to all, please review and abide by the [Code of Conduct](CODE_OF_CONDUCT.md).

## :rotating_light: Security Vulnerabilities

If you discover any security vulnerability within CoreZipCode, please [create a vulnerability issue](https://github.com/danilolutz/CoreZipCode/issues/new?labels=security%20vulnerabilities). All security vulnerabilities will be promptly addressed.

## :scroll: License

The CoreZipCode is open-sourced software licensed under the [MIT license](https://opensource.org/licenses/MIT).
