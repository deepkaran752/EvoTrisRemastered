using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace babbarversestudios
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance { get; private set; }

        [SerializeField] private InputActionAsset inputActions;

        private Dictionary<string, InputAction> registeredActions = new();
        private bool isUsingGamepad;

        public static bool IsUsingGamepad {
            get { return Instance.isUsingGamepad; }
            set { Instance.isUsingGamepad = value; }
        }

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

        /// <summary>
        /// Registers the input action u want
        /// </summary>
        /// <param name="actionName"></param>
        /// <returns>the consequent action</returns>
        public InputAction RegisterAction(string actionName)
        {
            InputAction action = inputActions.FindAction(actionName);
            if (action == null)
            {
                Debug.LogError($"Input action '{actionName}' was not found.");
                return null;
            }

            action.Enable();

            registeredActions[actionName] = action;

            return action;
        }

        /// <summary>
        /// Deregisters the input action
        /// </summary>
        /// <param name="actionName"></param>
        public void DeregisterAction(string actionName)
        {
            if (!registeredActions.TryGetValue(actionName, out InputAction action))
                return;

            action.Disable();
            registeredActions.Remove(actionName);
        }
    }
}
