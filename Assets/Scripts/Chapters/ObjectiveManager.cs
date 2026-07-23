using UnityEngine;

public class ObjectiveManager: MonoBehaviour
{
    //static ObjectiveManager objects;
    ////used for knowing the objectives for each chapter
    //private Dictionary<Chapters, List<string>> objectives = new();

    //public static void SetupObjectives(Chapters chapter, List<string> list) =>
    //    objects.objectives[chapter] = list;
}
public enum CurrentObjectiveStatus
{
    InProgress,
    Completed
}

public enum ObjectiveUnlockStatus
{
    Lock,
    Unlock
}