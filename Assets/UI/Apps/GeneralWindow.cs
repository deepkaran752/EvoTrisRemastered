using TMPro;
using UnityEngine;

namespace babbarversestudios
{
    public abstract class GeneralWindow : MonoBehaviour, IUIHandler
    {
        public abstract bool IsCurrentActiveWindow  { get; set; }
        public abstract bool IsMinimized { get; set; }

        public virtual void Close()
        {
            Debug.Log("[Dk Handler LOG] Closed app");
            UIManager.Instance.DeRegisterWindow(this);
            IsMinimized = false;
            IsCurrentActiveWindow = false;
        }

        public virtual void Open()
        {
            if (!UIManager.CanAccessCursor) return;
            Debug.Log("[Dk Handler LOG] Opened app");
            UIManager.Instance.RegisterWindow(this);
            this.gameObject.SetActive(true);
        }
    }

}
