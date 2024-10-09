# How to invoke an Azure Function from a Blazor WebAssembly application

## 1. Create in Visual Studio 2022 Community Edition a Blazor WebAssembly application



## 2. Create a new folder for the service



## 3. Create a Service for invoking the Azure Function

Navigate to the Azure Function in your Azure Portal and copy the default URL

Select All Resources in Azure Portal

![image](https://github.com/user-attachments/assets/f97f05af-e1af-4a60-811e-772195236692)

Click on the Azure Function name

![image](https://github.com/user-attachments/assets/8c772241-6af9-4655-8891-d250327aeda2)

Click on **Function1** it is the Azure Function class name

![image](https://github.com/user-attachments/assets/0ffc88cb-891c-4b45-9712-1c42b04ef768)

![image](https://github.com/user-attachments/assets/9bb97778-bc93-46e4-a826-17988d205633)

Copy the Azure Function URL in this line:

```csharp
 string functionUrl = "https://myfunctionforblazor.azurewebsites.net/api/Function1?code=YUKb4eMSrWqeFw2lxL0XJUzUgBfIw3Gh-pVTeELRtym8AzFuThTRwQ%3D%3D";
```

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

## 4. Create a new component for invoking the Service



## 5. Modify the middleware(Program.cs)



## 6. Add the CORS reference in the Azure Portal



## 7. Run a test the Blazor WebAssembly application
