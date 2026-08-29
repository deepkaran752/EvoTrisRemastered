using UnityEngine;
using UnityEngine.InputSystem;

namespace babbarversestudios
{
    public class PlayerInput : MonoBehaviour
    {
        #region Serialize Fields
        [SerializeField] Camera mCamera;
        #endregion
        #region Player Components
        PlayerCarry playerCarry;
        Player player;
        #endregion

        #region InputActions
        private InputAction moveAction;
        private InputAction lookAction;

        //interact
        private InputAction interactAction;
        #endregion

        #region Vectors
        private Vector2 moveAmt;
        private Vector2 lookAmt;
        private Vector3 velocity;
        #endregion

        #region CharacterController
        private CharacterController mController;
        #endregion

        #region readonly floats fields
        private readonly float walkSpeed = 3.8f;
        private readonly float rotateSpeed = 8.8f;
        private readonly float interactionDistance = 3f;
        private readonly float gravity = -1f;
        private float xRotation = 0f;
        #endregion

        #region Unity life cycle
        private void OnEnable()
        {
            interactAction.performed += Interact;
        }

        private void OnDisable()
        {
            interactAction.performed -= Interact;
        }

        private void Awake()
        {
            moveAction = InputManager.Instance.RegisterAction("Move");
            lookAction = InputManager.Instance.RegisterAction("Look");
            interactAction = InputManager.Instance.RegisterAction("Interact");
            mController = this.GetComponent<CharacterController>();
            playerCarry = this.GetComponent<PlayerCarry>();
            player = this.GetComponent<Player>();

            //handling the OS Cursor Here
            Cursor.visible = false;
        }

        private void OnDestroy()
        {
            InputManager.Instance.DeregisterAction("Move");
            InputManager.Instance.DeregisterAction("Look");
            InputManager.Instance.DeregisterAction("Interact");
        }

        private void Update()
        {
            moveAmt = moveAction.ReadValue<Vector2>();
            lookAmt = lookAction.ReadValue<Vector2>();
        }
        #endregion

        #region Walking
        public void Walking()
        {
            Vector3 moveDirection =
                       transform.forward * moveAmt.y +
                       transform.right * moveAmt.x;
            moveDirection.Normalize();

            mController.Move(
                moveDirection * walkSpeed * Time.fixedDeltaTime
            );

            //keeps the character grounded.
            velocity.y += gravity * Time.deltaTime;
            mController.Move(velocity * Time.deltaTime);
        }
        #endregion
        #region Rotation
        public void Rotating()
        {    
            //responsible for the camera movement up/down
            float mouseY = lookAmt.y * rotateSpeed * Time.deltaTime;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -45f, 45f);
            mCamera.transform.localRotation= Quaternion.Euler(xRotation,0f, 0f);

            //repsonsible for the horizontal player movement
            float rotationAmount = lookAmt.x * rotateSpeed * Time.deltaTime;
            transform.Rotate(0, rotationAmount, 0);
        }

        public void TurnTowardsComputer()
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            mCamera.transform.localRotation = Quaternion.identity;
        }
        #endregion
        #region CameraTweaks
        public void ChangeFieldOfView(float value) =>
            mCamera.fieldOfView = value;
        #endregion
        #region Interact
        public void Interact(InputAction.CallbackContext context)
        {
            //this means, the player was sitting, before interacting, change the state to free and return;
            if (player.IsInState(States.Sitting))
            {
                player.ChangeState(States.Free);
                return;
            }

            //if the player is carrying something, drop it first before interacting.
            if (playerCarry.IsCarrying)
            {
                playerCarry.Drop();
                return;
            }

            if(Physics.Raycast(mCamera.transform.position, mCamera.transform.forward, out RaycastHit hit, interactionDistance))
            {
                hit.collider.GetComponentInChildren<IInteractable>()?.Interact(gameObject);
            }
        }
        #endregion
        #region CursorInteractionBool
        public void AllowCursor(bool value) => InputManager.CanAccessCursor = value;
        #endregion
    }
}
