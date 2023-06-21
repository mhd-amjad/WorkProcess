using Dapr.Actors.Client;
using Dapr.Actors;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace WorkProcess.Controllers;

[ApiController]
[Route("[controller]")]
public class CounterController : ControllerBase
{
    private readonly IActorProxyFactory _actorProxyFactory;

    public CounterController(IActorProxyFactory actorProxyFactory)
    {
        _actorProxyFactory = actorProxyFactory;
    }

    [HttpPost("/increase")]
    public async Task<string> Increase([FromHeader] int increaseBy)
    {
        var actor = _actorProxyFactory.CreateActorProxy<IMyActor>(new ActorId("1"), "MyActor", new ActorProxyOptions { HttpEndpoint = "http://127.0.0.1:5001" });

        await actor.IncreaseCounterAsync(increaseBy);

        await Task.Delay(1000);

        var currentCounterValue = await actor.QueryCounterAsync();

        return JsonSerializer.Serialize(new CounterResult { result = currentCounterValue });
    }
}