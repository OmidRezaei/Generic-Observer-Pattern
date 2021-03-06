﻿using System;

namespace ObserverPattern.BuiltIn
{
    public class ObservedEnum<T> : ComparableSubject<T> where T : Enum
    {
        public ObservedEnum(T value, bool NotifyOnGet = false) : base(value, NotifyOnGet)
        {
        }

        public static implicit operator T(ObservedEnum<T> subject)
        {
            return subject.Value;
        }
    }
}
