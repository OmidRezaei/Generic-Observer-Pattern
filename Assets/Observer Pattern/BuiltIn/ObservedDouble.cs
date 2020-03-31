using System;

namespace ObserverPattern.BuiltIn
{
    [Serializable]
    public class ObservedDouble : ComparableSubject<double>
    {
        public ObservedDouble(double value = 0, bool NotifyOnGet = false) : base(value, NotifyOnGet)
        {
        }
    }
}
