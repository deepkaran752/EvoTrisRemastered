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

    public enum DrawState
    {
        Pull,
        Push,
        Open, 
        Closed
    }
}