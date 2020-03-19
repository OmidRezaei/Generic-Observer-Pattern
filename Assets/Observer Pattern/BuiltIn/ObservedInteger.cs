using System;

namespace ObserverPattern.BuiltIn
{
    [Serializable]
    public class ObservedInteger : ComparableSubject<int>
    {
        public ObservedInteger(int value = 0, bool NotifyOnGet = false) : base(value, NotifyOnGet)
        {

        }
    }
}