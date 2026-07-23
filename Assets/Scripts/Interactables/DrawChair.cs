using UnityEngine;
using babbarversestudios;

public class DrawChair : MonoBehaviour, IInteractable
{
    public Animator chair;
    private DrawState currentState;

    void Start() =>
        currentState = DrawState.Closed;

    public void Interact()
    {
        Debug.Log("[DK LOG] -> trying to interact with this object");
        switch (currentState)
        {
            case DrawState.Closed:
                chair.Play("DrawChaire");
                currentState = DrawState.Pull;
                CoroutineUtility.InvokeAfter(
                    () =>
                    {
                        currentState = DrawState.Open;
                    },.5f);
                break;

            case DrawState.Open:
                chair.Play("PushChaire");
                currentState = DrawState.Push;
                CoroutineUtility.InvokeAfter(
                    () =>
                    {
                        currentState = DrawState.Closed;
                    }, .5f);
                break;

            case DrawState.Pull:
            case DrawState.Push:
                Debug.Log("[DK LOG] -> cant do anything in this state");
                break;
        }
    }
}
