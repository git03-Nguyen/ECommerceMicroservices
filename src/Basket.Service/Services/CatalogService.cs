using System.Text.Json;
using Basket.Service.Models.Dtos;

namespace Basket.Service.Services;

public class CatalogService
{
    private readonly HttpClient _httpClient;

    public CatalogService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ProductDto> GetProductById(int productId)
    {
        var response = await _httpClient.GetAsync($"api/v1/Product/GetById/{productId}");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ProductDto>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        return null;
    }
}