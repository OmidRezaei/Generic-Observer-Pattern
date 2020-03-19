using UnityEngine;

namespace ObserverPattern.BuiltIn.Unity
{
    public class ObservedColor : Subject<Color>
    {
        public ObservedColor(Color value, bool NotifyOnGet = false) : base(value, NotifyOnGet)
        { }

        public ObservedColor(float red = 0, float green = 0, float blue = 0, float alpha = 1, bool NotifyOnGet = false) : base(new Color(red, green, blue, alpha), NotifyOnGet)
        { }
    }
}
