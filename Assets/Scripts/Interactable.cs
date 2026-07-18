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

        System.Collections.IEnumerator WaitForCertainDuration(InteractableState setState);
    }

    public enum InteractableState
    {
        Closed,
        Opening,
        Closing,
        Open
    }
}