# ThreeCXApi

A .NET library for seamless integration with the 3CX API, including OAuth2 authentication and API communication. This library simplifies interactions with 3CX instances, such as retrieving configurations and managing tokens.

## 🚀 Features

- **OAuth2 Token Management:** Automatic authentication and token caching.
- **HttpClient Integration:** Preconfigured services for accessing the 3CX API.
- **Modular Design:** Easily integrates into existing .NET projects.
- **Configuration-Based Setup:** Quickly set up using app configuration.

## 📦 Installation
You can install the package via [NuGet](https://www.nuget.org/):

```bash
dotnet add package ThreeCXApi
```

## 🛠️ Configuration
Add the following configuration section to your ```appsettings.json```:

```json
{
  "3CX": {
    "BaseAddress": "https://your-3cx-instance.com",
    "ClientId": "your-client-id",
    "ClientSecret": "your-client-secret",
    "GrantType": "client_credentials"
  }
}
```

In your ```Program.cs``` or ```Startup.cs```, register the service:

```csharp
using ThreeCXApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddThreeCXApi();

var app = builder.Build();
app.Run();
```

## 🌟 Contributing
Feel free to submit pull requests or open issues to improve the library.

## 🛠 Support
If you encounter any issues or have questions, feel free to open an issue.

## 📚 Resources
- [3CX Configuration API ](https://www.3cx.com/docs/configuration-rest-api/)
- [3CX Configuration API Endpoint Specification](https://www.3cx.com/docs/configuration-rest-api-endpoints/)
- [3CX Call Control API ](https://www.3cx.com/docs/call-control-api/)
- [3CX Call Control API Endpoint Specification](https://www.3cx.com/docs/call-control-api-endpoints/)

## 📜 License
This project is licensed under the MIT License.
