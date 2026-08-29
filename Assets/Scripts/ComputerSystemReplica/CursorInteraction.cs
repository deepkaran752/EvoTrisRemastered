using UnityEngine;
using UnityEngine.InputSystem;
namespace babbarversestudios { 
    /// <summary>
    /// Responsible for cusor interaction
    /// </summary>
    public partial class CursorInteraction : MonoBehaviour
    {
        #region Private Cursor vars
        private GameObject m_cursor;
        private Camera m_Camera;
        private Vector3 gamepadCursorPosition;
        private Vector2 cursorTravelDistance;
        private ComputerSetup m_ComputerSetup;
        #endregion
        #region RaycastHit section
        RaycastHit mCursorHitPoint;
        #endregion
        #region SerializeFields + Cursor movement related code
        [SerializeField] private GameObject lockScreen;
        [SerializeField] private BoxCollider bound; //this is the boundary that the m_cursor shouldn't cross
        [SerializeField] private LayerMask computerScreen;
        [SerializeField] private float HorizontalSensitivity = 1f;
        [SerializeField] private float VerticalSensitivity = 1f;
        [SerializeField] private float inputSpeed = 1f;
        #endregion
        #region InputAction for cursor movement
        InputAction cursorMovement;
        #endregion
        #region Unity Life Cycle
        private void Awake()
        {
            m_ComputerSetup = GameManager.Instance?.computerSetup;
            m_Camera = GameManager.Instance.GetCamera();
            m_ComputerSetup.TurnOnMonitor += InGameCursorVisibility;
        }
        private void Start()
        {
            Cursor.lockState = CursorLockMode.Confined;
            m_cursor = this.gameObject;
            m_cursor.SetActive(false);
            gamepadCursorPosition = m_cursor.GetComponent<RectTransform>().localPosition;
        }
        private void OnEnable()
        {
            cursorMovement = InputManager.Instance.RegisterAction("CursorMovement");
            cursorMovement.performed += CursorMovement;
            cursorMovement.canceled += CursorMovement;

            //for Clicking the cursor
            clickAction = InputManager.Instance.RegisterAction("Click");
            clickAction.performed += OnClick;
        }
        private void OnDisable()
        {
            cursorMovement.performed -= CursorMovement;
            cursorMovement.canceled -= CursorMovement;
            InputManager.Instance.DeregisterAction("CursorMovement");

            //for clicks
            clickAction.performed -= OnClick;
            InputManager.Instance.DeregisterAction("Click");
        }
        #endregion

        //Main Update Loop, responsible for deciding the cursor movement (gamepad or via mouse)
        private void Update()
        {
            if (Mouse.current != null && Mouse.current.delta.ReadValue() != Vector2.zero)
                InputManager.IsUsingGamepad = false;

            if (InputManager.IsUsingGamepad)
            {
                FollowGamepadController();
            }
            else
            {
                if (TryHitComputer())
                    FollowMousePointer();
            }
        }

        /// <summary>
        /// Mapped to the Cursor Movement action for the gamepad.
        /// </summary>
        /// <param name="ctx"></param>
        public void CursorMovement(InputAction.CallbackContext ctx)
        {
            if (!InputManager.CanAccessCursor) return;

            InputDevice device = ctx.control?.device;
            InputManager.IsUsingGamepad = device is Gamepad;

            if (!InputManager.IsUsingGamepad)
                return;

            Vector2 direction = ctx.ReadValue<Vector2>();
            cursorTravelDistance = new Vector2(direction.x, direction.y);
        }

        private bool TryHitComputer()
        {
            if (!InputManager.CanAccessCursor) 
                return false;

            Ray ray = m_Camera.ScreenPointToRay(Input.mousePosition);
            return bound.Raycast(ray, out mCursorHitPoint, Mathf.Infinity); //checking from the box collider
        }

        public void InGameCursorVisibility() => 
            m_cursor.SetActive(true);

        /// <summary>
        /// responsible for mapping the m_cursor to the OS cursor
        /// </summary>
        public void FollowMousePointer()
        {
            RectTransform rect = m_cursor.GetComponent<RectTransform>();
            Vector3 localPoint = bound.transform.InverseTransformPoint(mCursorHitPoint.point); //converting from world space to local space
            localPoint.z = 0f;
            //Vector3 previousPosition = rect.localPosition;
            rect.localPosition = localPoint;

            //if (!IsCursorInsideBounds())
            //{
            //    rect.localPosition = previousPosition;
            //    return;
            //}

            //syncing the gamepad position
            gamepadCursorPosition = localPoint;
        }

        /// <summary>
        /// responsible for moving the cursor in local space via the gamepad and bounds usage
        /// </summary>
        public void FollowGamepadController() 
        {
            float xCursor = cursorTravelDistance.x;
            gamepadCursorPosition += new Vector3(xCursor * HorizontalSensitivity, cursorTravelDistance.y * VerticalSensitivity, 0) * (inputSpeed * Time.unscaledDeltaTime);
            
            float minX = bound.center.x - bound.size.x * 0.5f; //to bring the cursor from worldspace to local space
            float maxX = bound.center.x + bound.size.x * 0.5f;

            float minY = bound.center.y - bound.size.y * 0.5f;
            float maxY = bound.center.y + bound.size.y * 0.5f;

            gamepadCursorPosition.x = Mathf.Clamp(
                gamepadCursorPosition.x,
                minX,
                maxX
            );

            gamepadCursorPosition.y = Mathf.Clamp(
                gamepadCursorPosition.y,
                minY,
                maxY
            );

            gamepadCursorPosition.z = 0f;

            m_cursor.GetComponent<RectTransform>().localPosition = gamepadCursorPosition;
        }

        private bool IsCursorInsideBounds()
        {
            RectTransform rect = m_cursor.GetComponent<RectTransform>();
            Vector3[] corners = new Vector3[4]; //will return me the corners of the cursor 
            rect.GetWorldCorners(corners);

            Vector3 center = bound.center; //not to use bounds (it's world space)
            Vector3 halfSize = bound.size * 0.5f;

            foreach(Vector3 value in corners)
            {
                Vector3 localCorner = bound.transform.InverseTransformPoint(value);

                if (localCorner.x < center.x - halfSize.x ||
                    localCorner.x > center.x + halfSize.x ||
                    localCorner.y < center.y - halfSize.y ||
                    localCorner.y > center.y + halfSize.y)
                {
                    return false;
                }
            }
            return true; 
        }
        /// helper
        #region When the AI is gonna access the cursor 
        //public void InteractWithCursor()
        //{

        //}
        #endregion
    }
}
