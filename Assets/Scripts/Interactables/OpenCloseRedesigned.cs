using System.Collections;
using babbarversestudios;
using UnityEngine;

public class OpenCloseRedesigned : MonoBehaviour, IInteractable
{
    public Animator openandclose;
    private InteractableState currentState;

    void Start() =>
        currentState = InteractableState.Closed;

    public void Interact(GameObject whoFired = null)
    {
        Debug.Log("[DK LOG] -> trying to interact with this object");
        switch (currentState)
        {
            case InteractableState.Closed:
                openandclose.Play("Opening");
                currentState = InteractableState.Opening;
                CoroutineUtility.InvokeAfter( 
                    () => {
                        currentState = InteractableState.Open;
                    }, .5f);
                break;

            case InteractableState.Open:
                openandclose.Play("Closing");
                currentState = InteractableState.Closing;
                CoroutineUtility.InvokeAfter(
                    () => {
                        currentState = InteractableState.Closed;
                    }, .5f);
                break;

            case InteractableState.Opening:
            case InteractableState.Closing:
                Debug.Log("[DK LOG] -> cant do anything in this state");
                break;
        }
    }
}