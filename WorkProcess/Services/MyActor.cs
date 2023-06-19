using Dapr.Actors.Runtime;
using Dapr.Client;

namespace WorkProcess;

public class MyActor : Actor, IMyActor
{
    private readonly ICounterService _counterService;

    public MyActor(ActorHost host, ICounterService counterService) : base(host)
    {
        _counterService = counterService;
    }

    public async Task IncreaseCounterAsync(int increaseBy)
    {
        await _counterService.IncreaseAsync(increaseBy);
    }

    public async Task<int> QueryCounterAsync()
    {
        return await _counterService.GetValueAsync();
    }
}