using System;
using System.Collections.Generic;

namespace ObserverPattern
{
    [Serializable]
    public class Subject<T> : ISubject<T>, IEquatable<T>, IEquatable<Subject<T>>
    {
        public delegate void SubjectModification(Subject<T> subject);

        #region Subject Variables
        public bool NotifyOnGet = false;
#if UNITY_EDITOR
        [UnityEngine.SerializeField]
#endif
        private T _value;
        public virtual T Value
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
#if UNITY_2019_3_OR_NEWER
        [UnityEngine.SerializeReference]
#endif
        public List<IObserver<T>> observers;
        #endregion

        public Subject(T value, bool NotifyOnGet = false)
        {
            _value = value;
            observers = new List<IObserver<T>>();
            this.NotifyOnGet = NotifyOnGet;
        }


        #region Observer Methods
        public virtual void NotifyObservers() => observers.ForEach(_ => _.Notify(this));

        public virtual void RegisterObserver(IObserver<T> observer) => observers.Add(observer);
        public virtual void RegisterObserver(IObserver<T> observer, int index) => observers.Insert(index, observer);

        public virtual void UnregisterObserver(IObserver<T> observer) => observers.Remove(observer);
        public virtual void UnregisterObserver(int observerIndex) => observers.RemoveAt(observerIndex);
        #endregion

        #region Subject Methods

        public void Modify(SubjectModification ModificationMethod)
        {
            ModificationMethod(this);
            NotifyObservers();
        }

        public static Subject<T> operator +(Subject<T> subject, IObserver<T> observer)
        {
            subject.RegisterObserver(observer);

            return subject;
        }

        public static Subject<T> operator -(Subject<T> subject, IObserver<T> observer)
        {
            subject.UnregisterObserver(observer);

            return subject;
        }

        #endregion

        #region Equaility Operation Overrides
        public static bool operator ==(Subject<T> subject, Subject<T> subject2) => subject.Equals(subject2);
        public static bool operator !=(Subject<T> subject, Subject<T> subject2) => !subject.Equals(subject2);

        public static bool operator ==(Subject<T> subject, T val) => subject.Equals(val);
        public static bool operator !=(Subject<T> subject, T val) => !subject.Equals(val);

        public static bool operator ==(T val, Subject<T> subject) => subject.Equals(val);
        public static bool operator !=(T val, Subject<T> subject) => !subject.Equals(val);
        #endregion

        #region Equality & Object Overriden Functions
        public bool Equals(Subject<T> other) => Value.Equals(other.Value);
        public bool Equals(T other) => Value.Equals(other);
        public override bool Equals(object obj)
        {
            if (obj is Subject<T>)
                return Equals((Subject<T>)obj);
            else if (obj is T)
                return Equals((T)obj);

            return base.Equals(obj);
        }
        public override int GetHashCode() => Value.GetHashCode();
        public override string ToString() => Value.ToString();
        #endregion
    }


}