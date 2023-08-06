using Catalog.Dtos;
using Catalog.Models;
using CSRequest;
using Newtonsoft.Json;

namespace Catalog.Services;

public class TransactionAPI
{
    private readonly IConfiguration _configuration;
    public TransactionAPI(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public async Task<TransactionModel> order(FoodRecOrder model)
    {
        var ep = _configuration.GetValue<string>("TransactionService");
        using var client = new HttpClient();
        var req = await new Request(ep + "/transaction", client)
            .WithJsonBody(model)
            .PostAsync();
        var b = await req.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<TransactionModel>(await req.Content.ReadAsStringAsync())!;
    }
}
