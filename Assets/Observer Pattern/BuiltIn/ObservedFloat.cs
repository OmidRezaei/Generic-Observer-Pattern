using System;

namespace ObserverPattern.BuiltIn
{
    [Serializable]
    public class ObservedFloat : ComparableSubject<float>
    {
        public ObservedFloat(float value = 0, bool NotifyOnGet = false) : base(value, NotifyOnGet)
        {
        }


        public static implicit operator float(ObservedFloat subject)
        {
            return subject.Value;
        }
    }
}
