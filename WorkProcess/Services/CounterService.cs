using Dapr.Client;

namespace WorkProcess;

public class CounterService : ICounterService
{
    private readonly DaprClient _daprClient;
    private const string storeName = "statestore";
    private const string key = "counter";

    public CounterService(DaprClient daprClient)
    {
        _daprClient = daprClient;
    }

    public async Task<int> GetValueAsync()
    {
        var currentValue = await _daprClient.GetStateAsync<int>(storeName, key);
        return currentValue;
    }

    public async Task IncreaseAsync(int increaseBy)
    {
        var counter = await GetValueAsync();
        counter += increaseBy;

        await _daprClient.SaveStateAsync(storeName, key, counter);
    }
}