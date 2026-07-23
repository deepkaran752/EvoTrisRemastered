using UnityEngine;

public class ChapterManager : MonoBehaviour
{
    Chapters chapters;

    private void Start()
    {
        
    }

    private void InitializeObjectives()
    {
        foreach(var chapter in System.Enum.GetValues(typeof(Chapters)))
        {
            //
        }
    }
}
public enum Chapters
{
    SetupComputer,
    PlayFirstGame,
    AIFirstLie,
    ParentsDiversion,
    SearchCreatorFiles,
    ShutdownAttempt,
    Ending
}

public enum ChapterStatus
{
    Queued,
    Current,
    Completed
}
