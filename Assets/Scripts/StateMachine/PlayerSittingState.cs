using UnityEngine;
using babbarversestudios;

public class PlayerSittingState : PlayerState
{
    public override States State => States.Sitting;
    private Transform whereToSit;

    public PlayerSittingState(Player player, Transform transform = null)
        : base(player)
    {
        whereToSit = transform;
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
        player.GetComponent<PlayerInput>().Rotating();

        //executes the part
        player.gameObject.transform.position = whereToSit.position + new Vector3(0f, 0f, 0f);
    }
}
