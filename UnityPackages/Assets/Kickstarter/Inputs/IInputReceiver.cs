using Kickstarter.Identification;

public interface IInputReceiver<in T> : IInputReceiver
{
    public void ReceiveInput(T input);
}

public interface IInputReceiver
{
    public void SubscribeToInputs(Player player);

    public void UnsubscribeToInputs(Player player);
}
