using Dapr.Actors.Client;
using Dapr.Actors;
using Dapr.Client;

namespace WorkProcess;

public class CounterActorService : ICounterActorService
{
    private readonly DaprClient _daprClient;
    private readonly IMyActor _actor;

    public CounterActorService(DaprClient daprClient)
    {
        _daprClient = daprClient;

        var actorType = "MyActor";
        var actorId = new ActorId("1");
        _actor = ActorProxy.Create<IMyActor>(actorId, actorType, new ActorProxyOptions { HttpEndpoint = "http://127.0.0.1:5001"});
    }

    public async Task<int> GetValueAsync(string storeName, string key)
    {
        var currentValue = await _daprClient.GetStateAsync<int>(storeName, key);
        return currentValue;
    }

    public async Task IncreaseAsync(string storeName, string key, int increaseBy)
    {
        var f = await _actor.SaySomething();

        var counter = await GetValueAsync(storeName, key);
        counter += increaseBy;

        await _daprClient.SaveStateAsync(storeName, key, counter);
    }
}