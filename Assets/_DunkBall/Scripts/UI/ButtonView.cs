using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _DunkBall.Scripts.UI
{
    public class ButtonView : BaseView
    {
        [SerializeField] private Button _button;
        private UnityAction _actionTemp;
        private void Awake() => _button.onClick.AddListener(() => { _actionTemp?.Invoke(); });
        public void Init(UnityAction callback) => _actionTemp = callback;
        public void SwitchInteractable(bool flag) => _button.interactable = flag;
        protected override void OnDisable() => _actionTemp = null;

        public override void Init(params object[] parameters)
        {
        }
    }
}