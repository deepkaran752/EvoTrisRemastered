using UnityEngine;
namespace babbarversestudios
{
    public partial class WindowsStart : MonoBehaviour
    {
        [Space]
        [Header("Panel Related")]
        [SerializeField] GameObject panelObject;

        public void OpenClosePanel() =>
            panelObject.SetActive(!panelObject.activeInHierarchy);
    }
}
