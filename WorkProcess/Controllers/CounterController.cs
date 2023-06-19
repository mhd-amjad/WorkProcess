using Dapr.Actors.Client;
using Dapr.Actors;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace WorkProcess.Controllers;

[ApiController]
[Route("[controller]")]
public class CounterController : ControllerBase
{
    private readonly IMyActor _actor;

    public CounterController()
    {
        var actorType = "MyActor";
        var actorId = new ActorId("1");
        _actor = ActorProxy.Create<IMyActor>(actorId, actorType, new ActorProxyOptions { HttpEndpoint = "http://127.0.0.1:5001" });
    }

    [HttpPost("/increase")]
    public async Task<string> Increase([FromHeader] int increaseBy)
    {
        await _actor.IncreaseCounterAsync(increaseBy);

        await Task.Delay(1000);

        var currentCounterValue = await _actor.QueryCounterAsync();

        return JsonSerializer.Serialize(new CounterResult { result = currentCounterValue });
    }
}