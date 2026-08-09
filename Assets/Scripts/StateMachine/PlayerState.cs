using UnityEngine;

public abstract class PlayerState : IState
{
    public abstract States State { get; }
    protected Player player;

    protected PlayerState (Player player)
    {
        this.player = player;
    }

    public abstract void Enter();

    public abstract void Exit();

    public abstract void Execute();
}

public enum States
{
    Free, 
    Sitting
}

public interface IState
{
    void Enter();
    void Exit();
    void Execute();
}

public class StateMachine
{
    private IState currentState;

    public void ChangeState(IState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }

    public void Update() =>
        currentState?.Execute();
}