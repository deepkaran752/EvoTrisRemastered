using UnityEngine;

namespace babbarversestudios
{
    public class Interactable : MonoBehaviour
    {
    }

    //responsible for interactions
    public interface IInteractable
    {
        void Interact();

        System.Collections.IEnumerator WaitForCertainDuration(InteractableState setState = InteractableState.None);
    }

    public enum InteractableState
    {
        Closed,
        Opening,
        Closing,
        Open,
        None
    }

    public enum ComputerInteractables
    {
        Box,
        Table
    }
}