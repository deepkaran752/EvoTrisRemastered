using System.Collections;
using babbarversestudios;
using UnityEngine;

public class OpenCloseWindowRedesigned : MonoBehaviour, IInteractable
{
    public Animator openandclosewindow;
    private InteractableState currentState;

    void Start() =>
        currentState = InteractableState.Closed;

    public void Interact()
    {
        Debug.Log("[DK LOG] -> trying to interact with this object");
        switch (currentState)
        {
            case InteractableState.Closed:
                openandclosewindow.Play("Openingwindow");
                currentState = InteractableState.Opening;
                CoroutineUtility.InvokeAfter(
                    () => {
                        currentState = InteractableState.Open;
                    }, .5f);
                break;

            case InteractableState.Open:
                openandclosewindow.Play("Closingwindow");
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