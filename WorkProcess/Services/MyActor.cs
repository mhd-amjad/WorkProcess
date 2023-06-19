using Dapr.Actors.Runtime;
using Dapr.Client;

namespace WorkProcess;

public class MyActor : Actor, IMyActor
{
    private readonly DaprClient _client;
    public MyActor(ActorHost host, DaprClient client) : base(host)
    {
        _client = client;
    }

    //public Task<string> IncreaseCounterAsync()
    //{
    //    throw new NotImplementedException();
    //}

    //public Task<string> QueryCounterAsync()
    //{
    //    throw new NotImplementedException();
    //}

    public async Task<string> SaySomething()
    {
        var currentValue = await _client.GetStateAsync<int>("statestore", "counter");
        return currentValue.ToString();
    }
}