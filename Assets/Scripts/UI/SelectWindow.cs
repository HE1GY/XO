using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public abstract class SelectWindow : MonoBehaviour
    {
        private static readonly int ShownWindowHashCode = Animator.StringToHash("IsShown");

        [SerializeField] private Button _firstButton;
        [SerializeField] private Button _secondButton;

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        protected void OnEnable()
        {
            _firstButton.onClick.AddListener(OnFirstButtonClick);
            _secondButton.onClick.AddListener(OnSecondButtonClick);
        }

        protected abstract void OnFirstButtonClick();
        protected abstract void OnSecondButtonClick();


        protected void TurnOff()
        {
            _animator.SetBool(ShownWindowHashCode, false);
        }

        protected void TurnOn()
        {
            _animator.SetBool(ShownWindowHashCode, true);
        }
    }
}