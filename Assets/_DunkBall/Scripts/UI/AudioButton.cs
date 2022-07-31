using _DunkBall.Scripts.Audio;
using UnityEngine;
using UnityEngine.UI;

namespace _DunkBall.Scripts.UI
{
    [RequireComponent(typeof(Button))]
    public class AudioButton : MonoBehaviour
    {
        [SerializeField] private string _clickSoundName = "click_button_3";
        private Button _button;

        private void Awake()
        {
            TryGetComponent(out _button);

            _button.onClick.AddListener(() => AudioHelper.PlaySoundByName(_clickSoundName));
        }
    }
}