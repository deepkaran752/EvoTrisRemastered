using babbarversestudios;
using UnityEngine;

public class GrabAndDrop : MonoBehaviour, IInteractable
{
    [SerializeField] private ComputerParts currentPart;
    private ComputerInteractables currentInteractableState;
    [SerializeField] private ComputerSetup Setup;

    private void Start() =>
        currentInteractableState = ComputerInteractables.Box;

    private event System.Action PartSet;
    int count = 0;

    private void OnEnable()
    {
        PartSet += OnPartSet;
    }

    private void OnDestroy()
    {
        PartSet -= OnPartSet;
    }

    public void Interact()
    {
        switch (currentInteractableState)
        {
            case ComputerInteractables.Box:
                Debug.Log("[DK LOG] -> setting up the monitor on computer table");
                Transform value = Setup.SetupPartOnTable(currentPart, out count);
                transform.position = value.position;
                transform.rotation = value.rotation;
                CoroutineUtility.InvokeAfter(
                    () => {
                        currentInteractableState = ComputerInteractables.Table;
                        PartSet?.Invoke();
                    }, .5f);
                break;
        }
    }

    private void OnPartSet()
    {
        if(count >= 5)
            Setup.TurnOnMonitor?.Invoke();
    }
}
