using UnityEngine;
using babbarversestudios;

public class GrabObject : MonoBehaviour, ICarry, IInteractable
{
    private CarryState currentState;
    private ComputerSetup computerSetup;
    private bool isBoxEmpty = false;

    [SerializeField] Transform targetPosition;
    public CarryState GetSetCarryState
    {
        get { return currentState; }
        set { currentState = value; }
    }

    #region Unity Life Cycle
    void Awake() =>
        computerSetup = GameManager.Instance?.computerSetup;
    private void Start()
    {
        currentState = CarryState.OnFloor;
    }
    void OnEnable() =>
        computerSetup.TurnOnMonitor += SetBool;

    void OnDisable() =>
        computerSetup.TurnOnMonitor -= SetBool;

    void OnDestroy() =>
        isBoxEmpty = false;
    #endregion

    private void SetBool() => isBoxEmpty = true;

    public void Interact(GameObject whoFired = null)
    {
        if (!isBoxEmpty) return; //if the box is not empty return

        Carry(whoFired?.GetComponent<PlayerCarry>());
    }

    public void Carry(PlayerCarry playerCarry)
    {
        //used to carry the object.
        switch (currentState)
        {
            case CarryState.OnFloor:
                currentState = CarryState.Picking;
                playerCarry.CarryObject(this);
                CoroutineUtility.InvokeAfter(
                    () => currentState = CarryState.OnFloor
                    ,0.5f);
                break;
            case CarryState.Carried:
            case CarryState.Placing:
            case CarryState.Picking:
                Debug.Log("[DK LOG] can't do anything in this period");
                break;
            default:
                break;
        }
    }
}
