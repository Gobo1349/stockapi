using System.Text.Json;
using stockapi.HttpModels;
namespace stockapi.HttpClients;

public interface IStockHttpClient // для интеграции клиентов в наш API 
// можно добавить это в нугет и другие челы смогут подключить его (services.AddHttpClient)
{
    Task<List<StockItemResponce>> GetAll(CancellationToken token);
}

public class StockHttpClient : IStockHttpClient
{
    private readonly HttpClient _httpClient;

    public StockHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<StockItemResponce>> GetAll(CancellationToken token)
    {
        // обращение                                                                                    
        using var responce = await _httpClient.GetAsync("v1/api/stocks", token);
        var body = await responce.Content.ReadAsStringAsync(token);
        return JsonSerializer.Deserialize<List<StockItemResponce>>(body);
    }
    
    public async void StockItemDemand(StockItemDemandRequest model, CancellationToken token)
    {
        using var responce = await _httpClient.GetAsync("/Demand", token);
        var body = await responce.Content.ReadAsStringAsync(token);
        return;
    }
    
    public async Task<List<StockItemDeliveryResponce>> GetInfo(CancellationToken token)
    {
        using var responce = await _httpClient.GetAsync("/GetInfo", token);
        var body = await responce.Content.ReadAsStringAsync(token);
        return JsonSerializer.Deserialize<List<StockItemDeliveryResponce>>(body);
    }
}