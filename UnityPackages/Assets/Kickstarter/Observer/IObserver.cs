namespace Kickstarter.Observer
{
    public interface IObserver<in T>
    {
        public void OnNotify(T argument);
    }
}
