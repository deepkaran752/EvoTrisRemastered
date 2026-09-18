using UnityEngine;
using UnityEngine.UI;
namespace babbarversestudios
{
    public partial class WindowsStart : MonoBehaviour
    {
        [Space]
        [Header("Panel Related")]
        [SerializeField] GameObject panelObject;
        [SerializeField] Slider soundSlider;
        [SerializeField] Slider brightnessSlider;

        [Header("Speaker Related")]
        [SerializeField] Sprite[] soundSprites;
        [SerializeField] GameObject speaker;

        public void OpenClosePanel() =>
            panelObject.SetActive(!panelObject.activeInHierarchy);

        private void ListenToSoundSlider(float value)
        {
            var speakerImg = speaker.GetComponent<Image>();
            if (value <= 0f)
                speakerImg.sprite = soundSprites[0];
            else if (value >= 1f)
                speakerImg.sprite = soundSprites[2];
            else if (value >= 0f && value <= 1f)
                speakerImg.sprite = soundSprites[1];
        }

        private void ListenToBrightnessSlider(float value)
        {

        }
    }
}
