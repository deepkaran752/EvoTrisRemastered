using UnityEngine;
using UnityEngine.InputSystem;

namespace babbarversestudios
{
    public class PlayerInput : MonoBehaviour
    {
        #region Serialize Fields
        [SerializeField] InputActionAsset InputAction;
        [SerializeField] Camera mCamera;
        PlayerCarry playerCarry;
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
        private readonly float walkSpeed = 5f;
        private readonly float rotateSpeed = 5f;
        private readonly float interactionDistance = 3f;
        private readonly float gravity = -1f;
        private float xRotation = 0f;
        #endregion

        #region Unity life cycle
        private void OnEnable()
        {
            InputAction.FindActionMap("Player").Enable();
            interactAction.performed += Interact;
        }

        private void OnDisable()
        {
            InputAction.FindActionMap("Player").Disable();
            interactAction.performed -= Interact;
        }

        private void Awake()
        {
            moveAction = InputSystem.actions.FindAction("Move");
            lookAction = InputSystem.actions.FindAction("Look");
            interactAction = InputSystem.actions.FindAction("Interact");
            mController = this.GetComponent<CharacterController>();
            playerCarry = this.GetComponent<PlayerCarry>();
        }

        private void Update()
        {
            moveAmt = moveAction.ReadValue<Vector2>();
            lookAmt = lookAction.ReadValue<Vector2>();

            Rotating();
            Walking();
        }
        #endregion

        #region Walking
        private void Walking()
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
        private void Rotating()
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
        #endregion
        #region Interact
        private void Interact(InputAction.CallbackContext context)
        {
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
    }
}
