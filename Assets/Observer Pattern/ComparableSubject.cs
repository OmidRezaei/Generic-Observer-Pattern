using System;

namespace ObserverPattern
{
    public class ComparableSubject<T> : Subject<T>, IComparable, IComparable<T>, IComparable<ComparableSubject<T>> where T : IComparable
    {
        public ComparableSubject(T value, bool NotifyOnGet = false) : base(value, NotifyOnGet) { }

        #region Compare Operation Overrides
        public static bool operator <(ComparableSubject<T> subject1, ComparableSubject<T> subject2) => subject1.CompareTo(subject2) < 0;
        public static bool operator >(ComparableSubject<T> subject1, ComparableSubject<T> subject2) => subject1.CompareTo(subject2) > 0;
        public static bool operator <=(ComparableSubject<T> subject1, ComparableSubject<T> subject2) => subject1.CompareTo(subject2) <= 0;
        public static bool operator >=(ComparableSubject<T> subject1, ComparableSubject<T> subject2) => subject1.CompareTo(subject2) >= 0;

        public static bool operator <(ComparableSubject<T> subject1, T val) => subject1.CompareTo(val) < 0;
        public static bool operator >(ComparableSubject<T> subject1, T val) => subject1.CompareTo(val) > 0;
        public static bool operator <=(ComparableSubject<T> subject1, T val) => subject1.CompareTo(val) <= 0;
        public static bool operator >=(ComparableSubject<T> subject1, T val) => subject1.CompareTo(val) >= 0;

        public static bool operator <(T val, ComparableSubject<T> subject1) => subject1.CompareTo(val) > 0;
        public static bool operator >(T val, ComparableSubject<T> subject1) => subject1.CompareTo(val) < 0;
        public static bool operator <=(T val, ComparableSubject<T> subject1) => subject1.CompareTo(val) >= 0;
        public static bool operator >=(T val, ComparableSubject<T> subject1) => subject1.CompareTo(val) <= 0;
        #endregion

        #region Compare To Functions
        public int CompareTo(object obj)
        {
            if (obj is ComparableSubject<T>)
                return CompareTo((Subject<T>)obj);
            else if (obj is T)
                return CompareTo((T)obj);
            throw new Exception("Not Comparable");
        }
        public int CompareTo(ComparableSubject<T> other) => CompareTo(other.Value);
        public int CompareTo(T other) => Value.CompareTo(other);
        #endregion

        public static implicit operator T(ComparableSubject<T> subject)
        {
            return subject.Value;
        }
    }
}
