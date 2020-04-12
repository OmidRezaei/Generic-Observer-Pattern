using System;
using System.Collections.Generic;

namespace ObserverPattern
{
    [Serializable]
    public class Subject<T> : ISubject<T>, IEquatable<T>, IEquatable<Subject<T>>
    {
        public delegate void SubjectModifier(Subject<T> subject, ref T subjectValue);

        #region Subject Variables
        public bool NotifyOnGet = false;
#if UNITY_EDITOR
        [UnityEngine.SerializeField]
#endif
        private T _value;
        /// <summary>
        /// Value property supports both getting and setting.
        /// If Value is returned and "NotifyOnGet == True" then NotifyObservers() is called.
        ///
        /// If value is a reference type object then changing its attributes will NOT call NotifyObservers().
        /// If you wish to call NotifyObservers() after changing the content of the value object, use Modify() instead or call NotifyObservers() manually afterwards.
        /// </summary>
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

        /// <summary>
        /// It applies modifier method on the subject and its value, then calls NotifyObservers().
        /// NotifyOnGet will NOT affect the given "subjectValue" in the modifierMethod.
        /// NotifyOnGet will affect "subject" object passed to modifierMethod if the "Get" method of property "Value" is called.
        /// 
        /// </summary>
        public virtual void Modify(SubjectModifier modifierMethod)
        {
            modifierMethod(this, ref _value);
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

        public static implicit operator T(Subject<T> subject)
        {
            return subject.Value;
        }

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