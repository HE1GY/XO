using UnityEngine;
using Utilities.Events;
using Button = UnityEngine.UI.Button;

namespace UI
{
    [RequireComponent(typeof(Button))]
    public class RestartButton : MonoBehaviour
    {
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(delegate { EventsControllerXo.Broadcast(EventsTypeXo.ReStart); });
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(delegate { EventsControllerXo.Broadcast(EventsTypeXo.ReStart); });
        }
    }
}