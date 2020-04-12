using System;

namespace ObserverPattern.BuiltIn
{
    [Serializable]
    public class ObservedInteger : ComparableSubject<int>
    {
        public ObservedInteger(int value = 0, bool NotifyOnGet = false) : base(value, NotifyOnGet)
        {

        }

        public static implicit operator int(ObservedInteger subject)
        {
            return subject.Value;
        }
    }
}