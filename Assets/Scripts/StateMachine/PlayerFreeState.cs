using babbarversestudios;
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
        Debug.Log("[DK LOG] -> entered the free state");
        player.GetComponent<PlayerInput>().AllowCursor(false);
    }

    public override void Exit()
    {
        Debug.Log("[DK LOG] -> exited the free state");
    }

    public override void Execute()
    {
        //executes the part
        player.GetComponent<PlayerInput>().Rotating();
        player.GetComponent<PlayerInput>().Walking();
    }
}
