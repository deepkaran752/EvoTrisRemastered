using UnityEngine;

[CreateAssetMenu(fileName = "SubObjectives", menuName = "Scriptable Objects/SubObjectives")]
public class SubObjective : ScriptableObject
{
    [SerializeField] private string displayText;
    [SerializeField] private ObjectiveUnlockStatus lockStatus;
}
