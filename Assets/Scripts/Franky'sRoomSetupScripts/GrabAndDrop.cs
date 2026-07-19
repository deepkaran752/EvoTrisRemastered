using babbarversestudios;
using System.Collections;
using UnityEngine;

public class GrabAndDrop : MonoBehaviour, IInteractable
{
    [SerializeField] private ComputerParts currentPart;
    private ComputerInteractables currentInteractableState;
    [SerializeField] private ComputerSetup Setup;

    private void Start() =>
        currentInteractableState = ComputerInteractables.Box;

    public void Interact()
    {
        switch (currentInteractableState)
        {
            case ComputerInteractables.Box:
                Debug.Log("[DK LOG] -> setting up the monitor on computer table");
                transform.position = Setup.SetupPartOnTable(currentPart).position;
                transform.rotation = Setup.SetupPartOnTable(currentPart).rotation;
                StartCoroutine(WaitForCertainDuration(InteractableState.None));
                break;
        }
    }

    public IEnumerator WaitForCertainDuration(InteractableState setState)
    {
        yield return new WaitForSeconds(.5f);
        currentInteractableState = ComputerInteractables.Table;
    }
}
