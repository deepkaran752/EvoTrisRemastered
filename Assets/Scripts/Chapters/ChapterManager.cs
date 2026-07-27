using UnityEngine;

public class ChapterManager : MonoBehaviour
{
    #region Singelton
    public static ChapterManager Instance;
    private void Awake()
    {
        if(Instance!=null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    [SerializeField] private Chapters currentChapter;

    //action for checking if the chapter is completed
    public System.Action RequestChapterCompletion;

    #region UnityLifeCycle
    private void Start()
    {
        currentChapter = Chapters.SetupComputer; //default, if any doesn't exist
    }
    private void OnEnable()
    {
        RequestChapterCompletion -= ChapterCompleted;
        RequestChapterCompletion += ChapterCompleted;
    }
    private void OnDisable()
    {
        RequestChapterCompletion -= ChapterCompleted;
    }
    #endregion

    private void ChapterCompleted()
    {
        //TODO: Game ends here, probably add the credit scene, kuch bhi
        if (currentChapter == Chapters.Ending) 
            return;

        currentChapter++;
        CurrentChapter = currentChapter;
    }

    public Chapters CurrentChapter
    {
        get { return currentChapter; }
        private set {  currentChapter = value; }    
    }

    public bool IsLastChapter()
    {
        return currentChapter == Chapters.Ending;
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
