using UnityEngine;

public class Interactable : MonoBehaviour
{
}

//responsible for interactions
public interface IInteractable
{
    void Interact();
}

public enum InteractableState
{
    Closed,
    Opening,
    Closing,
    Open
}