using Dapr.Actors;

namespace WorkProcess;

public interface ICounterActorService : IActor
{
    Task IncreaseAsync(string storeName, string key, int increaseBy);
    Task<int> GetValueAsync(string storeName, string key);
}