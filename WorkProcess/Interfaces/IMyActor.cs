using Dapr.Actors;

namespace WorkProcess;

public interface IMyActor: IActor
{
    Task IncreaseCounterAsync(int increaseBy);
    Task<int> QueryCounterAsync();
}