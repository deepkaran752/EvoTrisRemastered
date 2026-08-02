using UnityEngine;
using babbarversestudios;

public class GrabObjectTrigger : InteractableObjects
{
    public override void PlayerEntered(PlayerCarry playerCarry)
    {
        if (playerCarry.IsCarrying)
        {
            playerCarry.Drop(transform);
            GameManager.Instance.CurrentObjectiveDone?.Invoke();
        }
    }
}
