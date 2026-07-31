using System.Collections;
using babbarversestudios;
using UnityEngine;

public class OpencloseDoorRedesigned : MonoBehaviour, IInteractable
{
    public Animator openandclose;
    private InteractableState currentState;

    void Start() =>
        currentState = InteractableState.Closed;

    public IEnumerator WaitForCertainDuration(InteractableState setState)
    {
        yield return new WaitForSeconds(.5f);
        currentState = setState;
    }

    public void Interact(GameObject whoFired = null)
    {
        Debug.Log("[DK LOG] -> trying to interact with this object");
        switch (currentState)
        {
            case InteractableState.Closed:
                openandclose.Play("Opening");
                currentState = InteractableState.Opening;
                StartCoroutine(WaitForCertainDuration(InteractableState.Open));
                break;

            case InteractableState.Open:
                openandclose.Play("Closing");
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