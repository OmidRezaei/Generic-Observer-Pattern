namespace ObserverPattern.BuiltIn
{
    public class ObservedDouble : ComparableSubject<double>
    {
        public ObservedDouble(double value, bool NotifyOnGet = false) : base(value, NotifyOnGet)
        {
        }
    }
}
