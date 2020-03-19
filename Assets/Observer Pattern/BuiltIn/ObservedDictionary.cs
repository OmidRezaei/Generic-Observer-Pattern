using System;
using System.Collections.Generic;
using System.Linq;

namespace ObserverPattern.BuiltIn
{
    public class ObservedDictionary<KeyType, ValueType> : Subject<ValueType>
    {
        public ObservedDictionary(KeyType firstKey, ValueType firstValue, bool NotifyOnGet = false) : base(firstValue, NotifyOnGet) 
            => _valueDictionary = new Dictionary<KeyType, ValueType> { { firstKey, firstValue } };

        private Dictionary<KeyType, ValueType> _valueDictionary;
        public Dictionary<KeyType, ValueType> Dictionary
        {
            get
            {
                if (NotifyOnGet)
                    NotifyObservers();
                return _valueDictionary;
            }

            set
            {
                lastDictionary = _valueDictionary;
                _valueDictionary = value;
                NotifyObservers();
            }
        }

        public Dictionary<KeyType, ValueType> lastDictionary;

        public override ValueType Value => Dictionary.Count > 0 ? Dictionary.Values.ToArray()[0] : throw new Exception("Observed Dictionary Is Empty.");

        public ValueType this[KeyType key]
        {
            get => Dictionary[key];
            set
            {
                lastValue = Dictionary[key];
                Dictionary[key] = value;
                NotifyObservers();
            }
        }
    }
}
