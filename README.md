# How to invoke an Azure Function from a Blazor WebAssembly application

## 1. Create in Visual Studio 2022 Community Edition a Blazor WebAssembly application



## 2. Create a new folder for the service and Create a Service file for invoking the Azure Function

We create a new file inside the Services folder:

![image](https://github.com/user-attachments/assets/706eddfe-84b9-40b9-9ecb-4115b6920488)

This is the Service whole source code:

**AzureFunctionService.cs**

```csharp
namespace BlazorWebAssembly.Services
{
    public class AzureFunctionService
    {
        private readonly HttpClient _httpClient;

        public AzureFunctionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Method to call the Azure Function
        public async Task<string> InvokeAzureFunction()
        {
            // Update the URL with the actual Azure Function URL after deployment
            string functionUrl = "https://myfunctionforblazor.azurewebsites.net/api/Function1?code=YUKb4eMSrWqeFw2lxL0XJUzUgBfIw3Gh-pVTeELRtym8AzFuThTRwQ%3D%3D";

            // Making the HTTP request
            HttpResponseMessage response = await _httpClient.GetAsync(functionUrl);

            // Check if the response was successful
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            return "Failed to invoke Azure Function";
        }
    }
}
```

Navigate to the Azure Function in your Azure Portal and copy the default URL

Select All Resources in Azure Portal

![image](https://github.com/user-attachments/assets/f97f05af-e1af-4a60-811e-772195236692)

Click on the Azure Function name

![image](https://github.com/user-attachments/assets/8c772241-6af9-4655-8891-d250327aeda2)

Click on **Function1** it is the Azure Function class name

![image](https://github.com/user-attachments/assets/0ffc88cb-891c-4b45-9712-1c42b04ef768)

![image](https://github.com/user-attachments/assets/9bb97778-bc93-46e4-a826-17988d205633)

Copy the Azure Function URL in the service **AzureFunctionService.cs** code:

```csharp
 string functionUrl = "https://myfunctionforblazor.azurewebsites.net/api/Function1?code=YUKb4eMSrWqeFw2lxL0XJUzUgBfIw3Gh-pVTeELRtym8AzFuThTRwQ%3D%3D";
```

## 3. Create a new component for invoking the Service

```razor
@page "/AzureFunctionComponent"
@inject BlazorWebAssembly.Services.AzureFunctionService AzureFunctionService

<h3>Azure Function Invocation</h3>

<p>@message</p>

<button @onclick="InvokeFunction">Call Azure Function</button>

@code {
    private string message = "Click the button to invoke the Azure Function.";

    private async Task InvokeFunction()
    {
        // Call the Azure Function and store the response
        message = await AzureFunctionService.InvokeAzureFunction();
    }
}
```


## 4. Modify the middleware(Program.cs)

We add this code in the Program.cs file for registering the Service to invoke the Azure Function:

```
// Register the AzureFunctionService to handle Azure Function invocations
builder.Services.AddScoped<AzureFunctionService>();
```

See the middleware whole code:

**Program.cs**

```csharp
using BlazorWebAssembly;
using BlazorWebAssembly.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Register HttpClient for Azure Function calls
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Register the AzureFunctionService to handle Azure Function invocations
builder.Services.AddScoped<AzureFunctionService>();

await builder.Build().RunAsync();
```

## 5. Add the allowed CORS origins reference in the Azure Portal

Set the allowed CORS origins in API option 

![image](https://github.com/user-attachments/assets/04935ee7-fa99-42a6-aa98-1a0b4f40328e)

## 6. Run a test the Blazor WebAssembly application

![image](https://github.com/user-attachments/assets/6ec4e010-c101-421e-8a7f-c4573adf592d)

![image](https://github.com/user-attachments/assets/2ea5ea2a-6d8e-4c82-b5f0-db5f1b080d9e)

![image](https://github.com/user-attachments/assets/1b58a190-1e68-48aa-ae74-37383c51719c)
