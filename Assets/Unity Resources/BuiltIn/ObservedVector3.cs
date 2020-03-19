using UnityEngine;

namespace ObserverPattern.BuiltIn.Unity
{
    public class ObservedVector3 : Subject<Vector3>
    {
        public ObservedVector3(Vector3 value, bool NotifyOnGet = false) : base(value, NotifyOnGet)
        { }

        public ObservedVector3(float x = 0, float y = 0, float z = 0, bool NotifyOnGet = false) : this(new Vector3(x, y, z), NotifyOnGet)
        { }
    }
}
