using System.Text;
using System.Text.Json;
using Basket.Service.Models.Dtos;

namespace Basket.Service.Services;

public class CatalogService : ICatalogService
{
    private readonly HttpClient _httpClient;

    public CatalogService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<GetProductByIdResponse> GetProductById(int productId)
    {
        var response = await _httpClient.GetAsync($"api/v1/Product/GetById/{productId}");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<GetProductByIdResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        return null;
    }
    
    public async Task<GetProductsByIdsResponse> GetProductsByIds(IEnumerable<int> productIds)
    {
        var request = new GetProductsByIdsRequest { Payload = productIds };
        var response = await _httpClient.PostAsJsonAsync("api/v1/Product/GetByIds", request);
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<GetProductsByIdsResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        return null;
    }
    
    public class GetProductByIdResponse
    {
        public ProductDto Payload { get; set; }
    }
    
    public class GetProductsByIdsRequest        
    {
        public IEnumerable<int> Payload { get; set; }
    }
    
    public class GetProductsByIdsResponse
    {
        public IEnumerable<ProductDto> Payload { get; set; }
    }
}