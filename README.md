# Signing portal demo

[![Build Status](https://dev.azure.com/technologynexus/GO/_apis/build/status/demo/nexusgo-signingportaldemo%20build)](https://dev.azure.com/technologynexus/GO/_build/latest?definitionId=6)

This project uses the Nexus PDF signing API

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on a live system.

### Prerequisites

Setup an account for [PDF Signing](https://doc.nexusgroup.com/display/PUB/Nexus+GO+PDF+Signing)

The project uses [Azure Keyvault](https://docs.microsoft.com/en-us/azure/key-vault/key-vault-get-started) to sign its assertions

[Use key from webapp](https://docs.microsoft.com/en-us/azure/key-vault/key-vault-use-from-web-application)

### Setup 

To run the application, the following Environment variables needs to be set:
* KEYVAULT_BASE_URL - Base URL to keyvault that holds the signing key
* KEY - Name of signing key
* APPLICATION_ID - Application ID to access the signing key from application
* PASSWORD - Password for Application ID to access the signing key from application
* CLIENT_ID - Client ID to use the PDF Signing API
* ISS - Issuer (this property is not really used in the API, only for information)

## Deployment

[Create webapp in Azure](https://docs.microsoft.com/en-us/azure/app-service/app-service-web-get-started-dotnet)

## Built With

* [.NET Core 2.0](https://docs.microsoft.com/en-us/dotnet/) - The web framework used

## Reference deployment

* [Levepo](https://levepo.azurewebsites.net/)

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details


