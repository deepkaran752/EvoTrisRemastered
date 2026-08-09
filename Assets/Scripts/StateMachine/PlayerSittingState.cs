using UnityEngine;

public class PlayerSittingState : PlayerState
{
    public override States State => States.Sitting;

    public PlayerSittingState(Player player) 
        : base(player) 
    { 

    }

    public override void Enter()
    {
        //Cannot move, only rotate
        Debug.Log("[DK LOG] -> entered the sitting state");
    }

    public override void Exit()
    {
        //can move again
        Debug.Log("[DK LOG] -> exited the free state");
    }

    public override void Execute()
    {
        //executes the part
    }
}
