using Project.View;
using UnityEngine;

namespace System.Runtime.CompilerServices
{
    public class IsExternalInit { }
}

namespace Project.Data
{
    public class EventData
    {
        #region UI Events

        public class UI
        {
            public record TapOnButtonBefore(string Tag);
            public record TapOnButton(string Tag);
        }

        #endregion

        #region Cube Events
        public class Cube
        {
            public record Contact(CubeView Initiator, CubeView Target, Vector3 ContantPoint);
        }
        #endregion
    }
}
