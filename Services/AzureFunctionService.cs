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
