using System;
using System.Collections.Generic;

namespace ObserverPattern.Strcut
{
    [Serializable]
    public struct ComparableSubjectStruct<T> : ISubject<T>, IComparable, IComparable<T>, IComparable<ComparableSubjectStruct<T>> where T : IComparable
    {
        public delegate void SubjectModification(ComparableSubjectStruct<T> subject);

        #region Subject Variables
        public bool NotifyOnGet;

        private T _value;
        public T Value
        {
            get
            {
                if (NotifyOnGet)
                    NotifyObservers();
                return _value;
            }

            set
            {
                lastValue = _value;
                _value = value;
                NotifyObservers();
            }
        }

        public T lastValue;

        public List<IObserver<T>> observers;
        #endregion

        public ComparableSubjectStruct(T value, bool NotifyOnGet = false)
        {
            lastValue = value;
            _value = value;
            observers = new List<IObserver<T>>();
            this.NotifyOnGet = NotifyOnGet;
        }

        #region Observer Methods
        public void NotifyObservers()
        {
            ComparableSubjectStruct<T> newSubject = this;
            observers.ForEach(_ => _.Notify(newSubject));
        }

        public void RegisterObserver(IObserver<T> observer) => observers.Add(observer);
        public void RegisterObserver(IObserver<T> observer, int index) => observers.Insert(index, observer);

        public void UnregisterObserver(IObserver<T> observer) => observers.Remove(observer);
        public void UnregisterObserver(int observerIndex) => observers.RemoveAt(observerIndex);
        #endregion


        #region Subject Methods

        public void Modify(SubjectModification ModificationMethod)
        {
            ModificationMethod(this);
            NotifyObservers();
        }

        public static ComparableSubjectStruct<T> operator +(ComparableSubjectStruct<T> subject, IObserver<T> observer)
        {
            subject.RegisterObserver(observer);

            return subject;
        }

        public static ComparableSubjectStruct<T> operator -(ComparableSubjectStruct<T> subject, IObserver<T> observer)
        {
            subject.UnregisterObserver(observer);

            return subject;
        }

        #endregion

        #region Compare Operation Overrides
        public static bool operator <(ComparableSubjectStruct<T> subject1, ComparableSubjectStruct<T> subject2) => subject1.CompareTo(subject2) < 0;
        public static bool operator >(ComparableSubjectStruct<T> subject1, ComparableSubjectStruct<T> subject2) => subject1.CompareTo(subject2) > 0;
        public static bool operator <=(ComparableSubjectStruct<T> subject1, ComparableSubjectStruct<T> subject2) => subject1.CompareTo(subject2) <= 0;
        public static bool operator >=(ComparableSubjectStruct<T> subject1, ComparableSubjectStruct<T> subject2) => subject1.CompareTo(subject2) >= 0;

        public static bool operator <(ComparableSubjectStruct<T> subject1, T val) => subject1.CompareTo(val) < 0;
        public static bool operator >(ComparableSubjectStruct<T> subject1, T val) => subject1.CompareTo(val) > 0;
        public static bool operator <=(ComparableSubjectStruct<T> subject1, T val) => subject1.CompareTo(val) <= 0;
        public static bool operator >=(ComparableSubjectStruct<T> subject1, T val) => subject1.CompareTo(val) >= 0;

        public static bool operator <(T val, ComparableSubjectStruct<T> subject1) => subject1.CompareTo(val) > 0;
        public static bool operator >(T val, ComparableSubjectStruct<T> subject1) => subject1.CompareTo(val) < 0;
        public static bool operator <=(T val, ComparableSubjectStruct<T> subject1) => subject1.CompareTo(val) >= 0;
        public static bool operator >=(T val, ComparableSubjectStruct<T> subject1) => subject1.CompareTo(val) <= 0;
        #endregion

        #region Compare Functions
        public int CompareTo(object obj)
        {
            if (obj is ComparableSubjectStruct<T>)
                return CompareTo((Subject<T>)obj);
            else if (obj is T)
                return CompareTo((T)obj);
            throw new Exception("Not Comparable");
        }
        public int CompareTo(T other) => Value.CompareTo(other);
        public int CompareTo(ComparableSubjectStruct<T> other) => CompareTo(other.Value);
        #endregion
    }
}
