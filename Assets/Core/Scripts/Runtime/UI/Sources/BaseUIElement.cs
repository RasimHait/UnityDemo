using UnityEngine;

namespace Core.UI
{
    public class BaseUIElement : MonoBehaviour, IUIElement
    {
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}