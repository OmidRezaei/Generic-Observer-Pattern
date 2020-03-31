using System;
using System.Collections.Generic;

namespace ObserverPattern.Strcut
{
    [Serializable]
    public struct SubjectStruct<T> : ISubject<T>, IEquatable<T>, IEquatable<SubjectStruct<T>>
    {
        public delegate void SubjectModifier(SubjectStruct<T> subject, ref T subjectValue);

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

        public SubjectStruct(T value, bool NotifyOnGet = false)
        {
            lastValue = value;
            _value = value;
            observers = new List<IObserver<T>>();
            this.NotifyOnGet = NotifyOnGet;
        }


        #region Observer Methods
        public void NotifyObservers()
        {
            SubjectStruct<T> newSubject = this;
            observers.ForEach(_ => _.Notify(newSubject));
        }

        public void RegisterObserver(IObserver<T> observer) => observers.Add(observer);
        public void RegisterObserver(IObserver<T> observer, int index) => observers.Insert(index, observer);

        public void UnregisterObserver(IObserver<T> observer) => observers.Remove(observer);
        public void UnregisterObserver(int observerIndex) => observers.RemoveAt(observerIndex);
        #endregion

        #region Subject Methods
        public void Modify(SubjectModifier ModifierMethod)
        {
            ModifierMethod(this, ref _value);
            NotifyObservers();
        }

        public static SubjectStruct<T> operator +(SubjectStruct<T> subject, IObserver<T> observer)
        {
            subject.RegisterObserver(observer);

            return subject;
        }

        public static SubjectStruct<T> operator -(SubjectStruct<T> subject, IObserver<T> observer)
        {
            subject.UnregisterObserver(observer);

            return subject;
        }

        #endregion

        public static bool operator ==(SubjectStruct<T> subject, SubjectStruct<T> subject2) => subject.Equals(subject2);
        public static bool operator !=(SubjectStruct<T> subject, SubjectStruct<T> subject2) => !subject.Equals(subject2);

        public static bool operator ==(SubjectStruct<T> subject, T val) => subject.Equals(val);
        public static bool operator !=(SubjectStruct<T> subject, T val) => !subject.Equals(val);
        public static bool operator ==(T val, SubjectStruct<T> subject) => subject.Equals(val);
        public static bool operator !=(T val, SubjectStruct<T> subject) => !subject.Equals(val);



        public bool Equals(SubjectStruct<T> other) => Equals(other.Value);
        public bool Equals(T other) => Value.Equals(other);
        public override bool Equals(object obj)
        {
            if (obj is SubjectStruct<T>)
                return Equals((SubjectStruct<T>)obj);
            else if (obj is T)
                return Equals((T)obj);

            return base.Equals(obj);
        }
        public override int GetHashCode() => Value.GetHashCode();
        public override string ToString() => Value.ToString();
    }
}
