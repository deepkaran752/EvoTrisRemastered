using UnityEngine;
using babbarversestudios;

namespace SojaExiles
{
    public class PlayerMovement : MonoBehaviour
    {

        public CharacterController controller;

        public float speed = 5f;
        public float gravity = -15f;
        public float interactionDistance = 3f;

        Vector3 velocity;

        bool isGrounded;

        // Update is called once per frame
        void Update()
        {

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            controller.Move(move * speed * Time.deltaTime);

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);


            if (Input.GetKeyDown(KeyCode.E))
                InteractionPrompt();

        }

        private void InteractionPrompt()
        {
            if (Physics.Raycast(transform.position + new Vector3(0, 1.25f, 0), transform.forward, out RaycastHit hit, interactionDistance/*, LayerMask.GetMask("Interactables")*/))
            {
                hit.collider.GetComponentInChildren<IInteractable>()?.Interact();
            }

            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit2, interactionDistance/*, LayerMask.GetMask("Interactables")*/))
            {
                hit2.collider.GetComponentInChildren<IInteractable>()?.Interact();
            }
        }
    }
}