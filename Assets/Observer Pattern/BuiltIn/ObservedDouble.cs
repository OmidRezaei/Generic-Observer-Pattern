using System;

namespace ObserverPattern.BuiltIn
{
    [Serializable]
    public class ObservedDouble : ComparableSubject<double>
    {
        public ObservedDouble(double value = 0, bool NotifyOnGet = false) : base(value, NotifyOnGet)
        {
        }

        public static implicit operator double(ObservedDouble subject)
        {
            return subject.Value;
        }
    }
}
