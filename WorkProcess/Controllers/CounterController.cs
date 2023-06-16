using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace WorkProcess.Controllers;

[ApiController]
[Route("[controller]")]
public class CounterController : ControllerBase
{
    private readonly ICounterActorService _counterActorService;
    private const string storeName = "statestore";
    private const string key = "counter";

    public CounterController(ICounterActorService counterActorService)
    {
        _counterActorService = counterActorService;
    }

    [HttpPost("/increase")]
    public async Task<string> Increase([FromHeader] int increaseBy)
    {
        await _counterActorService.IncreaseAsync(storeName, key, increaseBy);

        await Task.Delay(1000);

        var currentCounterValue = await _counterActorService.GetValueAsync(storeName, key);

        return JsonSerializer.Serialize(new CounterResult { result = currentCounterValue });
    }
}