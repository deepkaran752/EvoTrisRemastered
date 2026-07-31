using System;
using UnityEngine;

namespace babbarversestudios
{
    //responsible for interactions
    public interface IInteractable
    {
        void Interact(GameObject whoFired = null);
    }
    
    public interface ICarry
    {
        void Carry(PlayerCarry playerCarry);
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

    public enum CarryState
    {
        OnFloor,
        Picking,
        Carried,
        Placing
    }
}