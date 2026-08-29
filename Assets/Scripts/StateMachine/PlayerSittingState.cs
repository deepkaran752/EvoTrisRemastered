using UnityEngine;
using babbarversestudios;

public class PlayerSittingState : PlayerState
{
    public override States State => States.Sitting;
    private Transform whereToSit;

    //camera zoom in zoom out values (field of view)
    private readonly float zoomedOutValue = 60f;
    private readonly float zoomedInValue = 40f;

    public PlayerSittingState(Player player, Transform transform = null)
        : base(player)
    {
        whereToSit = transform;
    }

    public override void Enter()
    {
        //Cannot move and rotate
        player.gameObject.transform.position = whereToSit.position + new Vector3(0f, 0f, 0f);
        player.GetComponent<PlayerInput>().ChangeFieldOfView(zoomedInValue); //changing for better apps vis
        player.GetComponent<PlayerInput>().TurnTowardsComputer();
        player.GetComponent<PlayerInput>().AllowCursor(true);
        Debug.Log("[DK LOG] -> entered the sitting state");
    }

    public override void Exit()
    {
        //can move again
        Debug.Log("[DK LOG] -> exited the sitting state");
        player.GetComponent<PlayerInput>().ChangeFieldOfView(zoomedOutValue); //changing to the default one.
        player.GetComponent<PlayerInput>().AllowCursor(false);
    }

    public override void Execute()
    {
    }
}
