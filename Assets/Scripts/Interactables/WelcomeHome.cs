using UnityEngine;

public class WelcomeHome : MonoBehaviour
{
    private bool IsPlayerEntered = false;
    private void OnTriggerEnter(Collider other)
    {
        if (IsPlayerEntered)
            return;

        if (other.CompareTag("Player"))
        {
            IsPlayerEntered = true;
            GameManager.Instance.CurrentObjectiveDone?.Invoke();
        }
    }
}
