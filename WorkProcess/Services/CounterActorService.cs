using Dapr.Client;

namespace WorkProcess;

public class CounterActorService : ICounterActorService
{
    private readonly DaprClient _daprClient;

    public CounterActorService(DaprClient daprClient)
    {
        _daprClient = daprClient;
    }

    public async Task<int> GetValueAsync(string storeName, string key)
    {
        var currentValue = await _daprClient.GetStateAsync<int>(storeName, key);
        return currentValue;
    }

    public async Task IncreaseAsync(string storeName, string key, int increaseBy)
    {
        var counter = await GetValueAsync(storeName, key);
        counter += increaseBy;

        await _daprClient.SaveStateAsync(storeName, key, counter);
    }
}