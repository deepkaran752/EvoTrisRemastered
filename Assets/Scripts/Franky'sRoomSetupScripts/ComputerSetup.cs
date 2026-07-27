using UnityEngine;

public class ComputerSetup : MonoBehaviour
{
    //this script will be used for setting up the computer, can be grab and drop.
    [Header("Computer Objects")]
    [SerializeField] GameObject Monitor;
    [SerializeField] GameObject Cpu;
    [SerializeField] GameObject Mouse;
    [SerializeField] GameObject Keyboard;
    [SerializeField] GameObject[] Speakers;


    [Header("Where to be placed")]
    [SerializeField] Transform MonitorPosition;
    [SerializeField] Transform CpuPosition;
    [SerializeField] Transform MousePosition;
    [SerializeField] Transform KeyBoardPosition;
    [SerializeField] Transform SpeakerLeftPosition;
    [SerializeField] Transform SpeakerRightPosition;

    [Header("For Monitor")]
    [SerializeField] GameObject TurnedOffScreen;
    [SerializeField] GameObject TurnedOnScreen;

    private int count = 0;

    #region Unity Life Cycle + Subs
    public System.Action TurnOnMonitor;
    private void OnEnable() =>
        TurnOnMonitor += SetupMonitorComplete;

    private void OnDestroy() =>
        TurnOnMonitor -= SetupMonitorComplete;

    #endregion

    public Transform SetupPartOnTable(ComputerParts part, out int count)
    {
        count = this.count++;
        return part switch
        {
            ComputerParts.Monitor => MonitorPosition,
            ComputerParts.Cpu => CpuPosition,
            ComputerParts.Mouse => MousePosition,
            ComputerParts.Keyboard => KeyBoardPosition,
            ComputerParts.SpeakerL => SpeakerLeftPosition,
            ComputerParts.SpeakerR => SpeakerRightPosition,
            _ => MonitorPosition
        };
    }

    public void SetupMonitorComplete()
    {
        TurnedOffScreen.SetActive(false);
        GameManager.Instance.CurrentObjectiveDone?.Invoke();
    }
    
}

public enum ComputerParts
{
    Monitor,
    Cpu,
    Mouse,
    Keyboard,
    SpeakerL,
    SpeakerR
}