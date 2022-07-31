using UnityEngine;
using UnityEngine.UI;

namespace _DunkBall.Scripts.UI
{
    public class ThemeSwitchButtonView : BaseView
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _buttonIcon;
        [SerializeField] private Sprite _iconLight, _iconDark;
        [SerializeField] private SpriteRenderer _backgroundSprite;
        [SerializeField] private Sprite _light, _dark;

        private void Awake() =>
            _button.onClick.AddListener(() =>
            {
                if (_backgroundSprite.sprite == _light)
                {
                    _buttonIcon.sprite = _iconLight;
                    _backgroundSprite.sprite = _dark;
                }
                else
                {
                    _buttonIcon.sprite = _iconDark;
                    _backgroundSprite.sprite = _light;
                }
            });
    }
}