using System;

namespace ObserverPattern.BuiltIn
{
    [Serializable]
    public class ObservedChar : ComparableSubject<char>
    {
        public ObservedChar(char value, bool NotifyOnGet = false) : base(value, NotifyOnGet)
        {
        }

        public static implicit operator char(ObservedChar subject)
        {
            return subject.Value;
        }
    }
}
