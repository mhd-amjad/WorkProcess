using Dapr.Actors;

namespace WorkProcess;

public interface ICounterService : IActor
{
    Task IncreaseAsync(int increaseBy);
    Task<int> GetValueAsync();
}