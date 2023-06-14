using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace WorkProcess.Controllers;

[ApiController]
[Route("[controller]")]
public class CounterController : ControllerBase
{
    private readonly DaprClient _daprClient;
    private const string storeName = "statestore";
    private const string key = "counter";

    public CounterController(DaprClient daprClient)
    {
        _daprClient = daprClient;
    }

    [HttpPost("/increase")]
    public async Task<string> Increase([FromHeader] int increaseBy)
    {
        var counter = await _daprClient.GetStateAsync<int>(storeName, key);

        counter += increaseBy;

        await _daprClient.SaveStateAsync(storeName, key, counter);
        await Task.Delay(2000);
        return JsonSerializer.Serialize(new CounterResult { result = counter});
    }
}