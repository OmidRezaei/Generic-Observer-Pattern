namespace ObserverPattern
{
    public interface ISubject<T> : IRegisterer<T>
    {
        T Value { get; set; }
        
        void NotifyObservers();
    }
}