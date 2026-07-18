using System.Collections;
using babbarversestudios;
using UnityEngine;

public class OpenCloseWindowRedesigned : MonoBehaviour, IInteractable
{
    public Animator openandclosewindow;
    private InteractableState currentState;

    void Start() =>
        currentState = InteractableState.Closed;

    public IEnumerator WaitForCertainDuration(InteractableState setState)
    {
        yield return new WaitForSeconds(.5f);
        currentState = setState;
    }

    public void Interact()
    {
        Debug.Log("[DK LOG] -> trying to interact with this object");
        switch (currentState)
        {
            case InteractableState.Closed:
                openandclosewindow.Play("Openingwindow");
                currentState = InteractableState.Opening;
                StartCoroutine(WaitForCertainDuration(InteractableState.Open));
                break;

            case InteractableState.Open:
                openandclosewindow.Play("Closingwindow");
                currentState = InteractableState.Closing;
                StartCoroutine(WaitForCertainDuration(InteractableState.Closed));
                break;

            case InteractableState.Opening:
            case InteractableState.Closing:
                Debug.Log("[DK LOG] -> cant do anything in this state");
                break;
        }
    }
}