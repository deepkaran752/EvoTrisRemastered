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

    public Transform SetupPartOnTable(ComputerParts part)
    {
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