using UnityEngine;
namespace babbarversestudios
{
    public partial class WindowsStart : MonoBehaviour
    {
        [Space]
        [Header("Time/Data")]
        [SerializeField] TMPro.TMP_Text timeRealtime;
        [SerializeField] TMPro.TMP_Text timeRealtimeHome;
        [SerializeField] TMPro.TMP_Text dateRealtimeHome;
        private void SetTime()
        {
            timeRealtime.text = System.DateTime.Now.ToString("HH:mm");
            timeRealtimeHome.text = System.DateTime.Now.ToString("HH:mm");
        }

        private void SetDate() =>
            dateRealtimeHome.text = System.DateTime.Now.ToString("d");
    }
}
