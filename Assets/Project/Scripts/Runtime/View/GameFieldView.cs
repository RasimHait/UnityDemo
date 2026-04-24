using UnityEngine;

namespace Project.View
{
    public class GameFieldView : MonoBehaviour
    {
        [SerializeField] private Transform _launchOrigin;
        [SerializeField] private float _launchOriginSlideLimit;

        public void MoveLaunchOrigin(float delta)
        {
            var current = _launchOrigin.localPosition;
            current.x = Mathf.Clamp(current.x + delta, -_launchOriginSlideLimit, _launchOriginSlideLimit);
            _launchOrigin.localPosition = current;
        }

        public void PlaceCube(CubeView cube)
        {
            cube.transform.SetParent(_launchOrigin);
            cube.transform.localPosition = Vector3.zero;
        }
    }
}
