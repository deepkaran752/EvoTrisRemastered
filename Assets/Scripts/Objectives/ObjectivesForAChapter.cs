using UnityEngine;

[CreateAssetMenu(fileName = "ObjectivesForAChapter", menuName = "Scriptable Objects/ObjectivesForAChapter")]
public class ObjectivesForAChapter : ScriptableObject
{
    [SerializeField] private Chapters chapter;
    [SerializeField] private System.Collections.Generic.List<SubObjective> objectives; //list of all the subobjectives in this project
}
