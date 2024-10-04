using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using DataAccess.DTO;

public class ApiService<T> where T : class
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    public async Task<List<T>> GetAllAsync(string apiUrl)
    {
        HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
        if (response.IsSuccessStatusCode)
        {
            string data = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<T>>(data, _jsonOptions);
        }
        return null;
    }

    public async Task<T> GetByIdAsync(string apiUrl, int id)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"{apiUrl}/{id}");
        if (response.IsSuccessStatusCode)
        {
            string data = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(data, _jsonOptions);
        }
        return null;
    }

    public async Task<bool> CreateAsync(string apiUrl, T entity)
    {
        var json = JsonSerializer.Serialize(entity);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _httpClient.PostAsync(apiUrl, content);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateAsync(string apiUrl, T entity, int id)
    {
        var json = JsonSerializer.Serialize(entity);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"{apiUrl}/{id}", entity);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteAsync(string apiUrl, int id)
    {
        HttpResponseMessage response = await _httpClient.DeleteAsync($"{apiUrl}/{id}");
        return response.IsSuccessStatusCode;
    }

    public async Task<List<T>> SearchAsync(string apiUrl, string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return await GetAllAsync(apiUrl);
        }

        var encodedSearchTerm = Uri.EscapeDataString(searchTerm);

        var searchUrl = $"{apiUrl}?searchTerm={encodedSearchTerm}";

        HttpResponseMessage response = await _httpClient.GetAsync(searchUrl);
        if (response.IsSuccessStatusCode)
        {
            string data = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<T>>(data, _jsonOptions);
        }
        return null;
    }

}
