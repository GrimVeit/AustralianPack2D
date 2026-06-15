using System;

public interface IChickenEvent
{
    public ChickenEventType EventType { get; }

    public void Start();
    public void Stop();

    public event Action<IChickenEvent> OnCompleted;
}
