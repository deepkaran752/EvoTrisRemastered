using UnityEngine;

public class PlayerFreeState : PlayerState
{
    public override States State => States.Free;

    public PlayerFreeState(Player player) 
        : base(player)
    {

    }

    public override void Enter()
    {
        //CAN MOVE
        Debug.Log("[DK LOG] -> entered the free state");
    }

    public override void Exit()
    {
        //CANNOT MOVE ANYMORE
        //ONLY CAN ROTATE NOW
        Debug.Log("[DK LOG] -> exited the free state");
    }

    public override void Execute()
    {
        //executes the part
    }
}
