using System;
using System.Collections.Generic;

namespace ObserverPattern.BuiltIn
{
    /// <summary>
    /// This is Still Being Developed. Don't use unless you know what you are doing.
    /// </summary>
    public class ObservedList<ItemType> : Subject<ItemType>
    {
        private List<ItemType> _valueList;
        public List<ItemType> List
        {
            get
            {
                if (NotifyOnGet)
                    NotifyObservers();
                return _valueList;
            }

            set
            {
                lastList = _valueList;
                _valueList = value;
                NotifyObservers();
            }
        }

        public List<ItemType> lastList;

        public ObservedList(ItemType firstValue, bool NotifyOnGet = false) : base(firstValue, NotifyOnGet) => _valueList = new List<ItemType> { firstValue };

        public override ItemType Value => _valueList.Count > 0 ? List[0] : throw new IndexOutOfRangeException("Observed List Does Not Have Any Items.");

        public ItemType this[int listIndex]
        {
            get => _valueList[listIndex];
            set
            {
                lastValue = _valueList[listIndex];
                _valueList[listIndex] = value;
                NotifyObservers();
            }
        }
    }
}
