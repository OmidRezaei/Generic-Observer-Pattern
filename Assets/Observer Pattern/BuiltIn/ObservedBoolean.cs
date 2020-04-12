using System;

namespace ObserverPattern.BuiltIn
{
    [Serializable]
    public class ObservedBoolean : Subject<bool>
    {
        public ObservedBoolean(bool value = false, bool NotifyOnGet = false) : base(value, NotifyOnGet)
        {

        }

        public static implicit operator bool(ObservedBoolean subject)
        {
            return subject.Value;
        }
    }
}
