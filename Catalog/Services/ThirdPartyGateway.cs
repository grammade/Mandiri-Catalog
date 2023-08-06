using Catalog.Dtos;
using Catalog.Entities;
using CSRequest;
using Newtonsoft.Json;

namespace Catalog.Services;
public class ThirdPartyGateway
{
    private readonly IConfiguration _configuration;
    public ThirdPartyGateway(IConfiguration configuration)
    {
        _configuration = configuration; 
    }
    public async Task<NutValues> GetNutValues(string foodName)
    {
        var ep = _configuration.GetValue<string>("ThirdPartyGateway");
        using var client = new HttpClient();
        var req = await new Request(ep + "/nutritionValue", client)
            .WithSegments(new string[] {foodName})
            .GetAsync();
        var b = await req.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<NutValues>(await req.Content.ReadAsStringAsync())!;
    }
}
