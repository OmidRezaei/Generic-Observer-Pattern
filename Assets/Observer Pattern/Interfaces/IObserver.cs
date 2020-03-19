namespace ObserverPattern
{
    public interface IObserver<T>
    {
        void Notify(ISubject<T> subject);
    }
}