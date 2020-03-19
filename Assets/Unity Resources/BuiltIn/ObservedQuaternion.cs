using UnityEngine;

namespace ObserverPattern.BuiltIn.Unity
{
    public class ObservedQuaternion : Subject<Quaternion>
    {
        public ObservedQuaternion(Quaternion value, bool NotifyOnGet = false) : base(value, NotifyOnGet)
        { }

        public ObservedQuaternion(float eulerX = 0, float eulerY = 0, float eulerZ = 0, bool NotifyOnGet = false) : this(new Vector3(eulerX, eulerY, eulerZ), NotifyOnGet)
        { }

        public ObservedQuaternion(Vector3 euler, bool NotifyOnGet = false) : this(Quaternion.Euler(euler), NotifyOnGet)
        { }
    }
}
