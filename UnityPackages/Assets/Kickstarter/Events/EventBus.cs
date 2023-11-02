using System;
using System.Threading.Tasks;

public static class EventBus<T> where T : struct, IEvent
{
    private const int millisecondsPerSecond = 1000;
    private static Action<T> @event;
    
    public static void Register(Action<T> listener)
    {
        @event += listener;
    }

    public static void Deregister(Action<T> listener)
    {
        @event -= listener;
    }

    public static void Raise(T arguments, float delay = 0)
    {
        int millisecondsDelay = (int) (delay * millisecondsPerSecond);
        RaiseWithDelay(arguments, millisecondsDelay);
    }

    private static async void RaiseWithDelay(T arguments, int delay = 0)
    {
        await Task.Delay(delay);
        @event.Invoke(arguments);
    }
}

public interface IEvent
{
    
}