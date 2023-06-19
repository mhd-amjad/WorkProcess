using Dapr.Actors;

namespace WorkProcess;

public interface IMyActor: IActor
{
    Task<string> SaySomething();
}