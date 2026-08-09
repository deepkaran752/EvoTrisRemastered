using UnityEngine;

public class Player : MonoBehaviour
{
    private StateMachine stateMachine;

    private PlayerState CurrentState;
    private void Awake()
    {
        stateMachine = new StateMachine();
    }

    void Start()
    {
        ChangeState(States.Free);
    }

    private void Update() =>
        stateMachine.Update();

    public void ChangeState(States states, Transform sittingObjPosition = null)
    {
        switch (states)
        {
            case States.Sitting:
                var sitState = new PlayerSittingState(this, sittingObjPosition);
                stateMachine.ChangeState(sitState);
                CurrentState = sitState;
                break;
            case States.Free:
                var freeState = new PlayerFreeState(this);
                stateMachine.ChangeState(freeState);
                CurrentState = freeState;
                break;
            default:
                Debug.LogError("[DK LOG] no state like this exists");
                break;
        }
    }

    public bool IsInState(States state)
    {
        return CurrentState.State == state;
    }
}
