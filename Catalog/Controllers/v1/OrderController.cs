using Catalog.Dtos;
using Catalog.Models;
using Catalog.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Catalog.Controllers.v1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
[Authorize]
public class OrderController : ControllerBase
{
    private readonly TransactionAPI _transactionApi;
    private readonly UserAuthorizationService _auth;
    public OrderController(TransactionAPI transactionApi, UserAuthorizationService auth)
    {
        _auth = auth;
        _transactionApi = transactionApi;
    }
    [HttpPost]
    public async Task<ActionResult<TransactionModel>> orderFood([FromBody] List<FoodRecOrderItems> model)
    {
        var user = _auth.getAuthorizedUser();

        if (model.Count is 0)
            return BadRequest("Food is null");
        var a = JsonConvert.SerializeObject(new FoodRecOrder(model, user.Id));

        return Ok(await _transactionApi.order(new FoodRecOrder(model, user.Id)));
    }
}
