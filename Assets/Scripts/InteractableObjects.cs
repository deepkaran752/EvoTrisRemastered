using UnityEngine;
namespace babbarversestudios
{
    public abstract class InteractableObjects : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<PlayerCarry>(out var playerCarry))
                PlayerEntered(playerCarry);
        }

        public abstract void PlayerEntered(PlayerCarry playerCarry);
    }
}

