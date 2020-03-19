namespace ObserverPattern
{
    public interface IRegisterer<T>
    {
        void RegisterObserver(IObserver<T> observer);
        void RegisterObserver(IObserver<T> observer, int index);
        void UnregisterObserver(int observerIndex);
        void UnregisterObserver(IObserver<T> observer);
    }
}