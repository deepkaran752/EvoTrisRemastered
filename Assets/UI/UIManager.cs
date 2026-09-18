using UnityEngine;
using System.Collections.Generic;
using System;
namespace babbarversestudios
{
    public class UIManager : MonoBehaviour
    {
        public static bool CanAccessCursor = false;
        #region Singleton Class
        public static UIManager Instance;
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        #endregion

        #region Event sub/unsub
        private event Action WindowRefresh;
        private void OnEnable()
        {
            WindowRefresh += RefreshSiblings;
            InputManager.Instance.AccessCursorValue += GetCursorAccess;
        }

        private void OnDisable()
        {
            WindowRefresh -= RefreshSiblings;
            InputManager.Instance.AccessCursorValue -= GetCursorAccess;
        }

        public void OnDestroy() => mOpenedUI.Clear();
        #endregion

        private void GetCursorAccess(bool value) => CanAccessCursor = value;

        #region IUIhandler handling -> which deals with active windows
        //stores the active UITab
        private readonly List<IUIHandler> mOpenedUI = new(); //using list cuz I've free will.

        public void RegisterWindow(IUIHandler obj)
        {
            if(mOpenedUI.Contains(obj))
                mOpenedUI.Remove(obj);

            mOpenedUI.Add(obj);
            WindowRefresh?.Invoke();
        }

        public void DeRegisterWindow(IUIHandler obj)
        {
            mOpenedUI.Remove(obj);
            WindowRefresh?.Invoke();
        }

        private void RefreshSiblings()
        {
            if (mOpenedUI.Count == 0)
                return;

            int i = 0;

            foreach (var windowOpen in mOpenedUI)
            {
                if (windowOpen is GeneralWindow window)
                {
                    window.transform.SetSiblingIndex(i);
                    i++;
                }
            }
        }
        #endregion
    }

    //will be attached to the UI Objects.
    public interface IUIHandler
    {
        void Open();
        void Close();
    }
}