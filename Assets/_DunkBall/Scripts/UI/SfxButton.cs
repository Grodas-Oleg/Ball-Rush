using _DunkBall.Scripts.Data;
using UnityEngine;
using UnityEngine.UI;

namespace _DunkBall.Scripts.UI
{
    public class SfxButton : MonoBehaviour
    {
        [SerializeField] private Button _sfxButton;
        [SerializeField] private Image _offHolder;

        private void Awake()
        {
            var data = DataSaver.GlobalData;
            AudioListener.volume = data.SfxValue ? 1 : 0;
            _offHolder.gameObject.SetActive(!data.SfxValue);

            _sfxButton.onClick.AddListener(() =>
            {
                bool flag;
                if (data.SfxValue)
                {
                    AudioListener.volume = 0;
                    flag = false;
                }
                else
                {
                    AudioListener.volume = 1;
                    flag = true;
                }

                DataSaver.GlobalData.SfxValue = flag;
                DataSaver.GlobalData = data;

                _offHolder.gameObject.SetActive(!data.SfxValue);
            });
        }
    }
}