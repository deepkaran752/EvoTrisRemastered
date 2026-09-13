using UnityEngine;
namespace babbarversestudios
{
    public partial class WindowsStart : MonoBehaviour
    {
        [SerializeField] GameObject UnlockScreen;
        [SerializeField] GameObject LockScreen;

        public void UnlockWindows()
        {
            //TODO: We can use animator to beautify stuff
            UnlockScreen.SetActive(true);
            LockScreen.SetActive(false);
        }

        #region Unity Life Cycle
        private void Start()
        {
            //setting the data
            SetDate();
            panelObject.SetActive(false); //in the start.
        }

        private void Update()
        {
            //needs to loop in this
            SetTime();
        }
        #endregion
    }
}
