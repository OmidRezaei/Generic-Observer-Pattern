using System;

namespace ObserverPattern.BuiltIn
{
    [Serializable]
    public class ObservedString : ComparableSubject<string>
    {
        public ObservedString(string value = "", bool NotifyOnGet = false) : base(value, NotifyOnGet)
        {

        }
    }
}
