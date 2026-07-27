using UnityEngine;

[CreateAssetMenu(fileName = "ObjectivesForAChapter", menuName = "Scriptable Objects/ObjectivesForAChapter")]
public class ObjectivesForAChapter : ScriptableObject
{
    public Chapters chapter;
    public System.Collections.Generic.List<SubObjective> objectives; //list of all the subobjectives in this project
}
