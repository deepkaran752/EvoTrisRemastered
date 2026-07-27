using UnityEngine;
using System.Collections.Generic;
using System;

public class ObjectiveManager: MonoBehaviour
{
    #region Singleton
    public static ObjectiveManager Instance;

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

    private List<SubObjective> chapterSubObjective;
    [SerializeField] private List<ObjectivesForAChapter> chapterObjectives;
    [SerializeField] SubObjective currentObjective;

    public SubObjective CurrentObjective
    {
        get { return currentObjective; }
    }

    ChapterManager chapterManagerInstance;


    //Queue
    private Queue<SubObjective> objectiveQueue = new();

    //Action
    /// <summary>
    /// Can be invoked to mark the give objective as completed;
    /// </summary>
    public Action<SubObjective> RequestToCompleteObjective;

    private void Start()
    {
        //for instances
        InitializeInstancesForUse();

        //assign new objectves based on the currentchapter
        AssignNewObjective();
    }

    private void OnEnable()
    {
        RequestToCompleteObjective -= ObjectiveCompleted;
        RequestToCompleteObjective += ObjectiveCompleted;
    }
    private void OnDisable()
    {
        RequestToCompleteObjective -= ObjectiveCompleted;
    }

    #region Queue Dequeue logic for SubObjectives
    /// <summary>
    /// Responsible for Queueing the objective based on the current chapter;
    /// </summary>
    private void InitializeQueue(List<SubObjective> subObjectives)
    {
        objectiveQueue.Clear(); //clearing the queue
        //need a check here if the current chapter has been completed, only on that basis, we are gonna proceed to the next thing
        foreach (var subObj in subObjectives)
        {
            objectiveQueue.Enqueue(subObj); //[1,2,3,4,5]
        }

        currentObjective = objectiveQueue.Count > 0 ? objectiveQueue.Peek(): null;
    }

    private void ObjectiveCompleted(SubObjective subObj)
    {
        if (currentObjective != subObj)
            return;

        objectiveQueue.Dequeue(); //[1] will be popped

        if (objectiveQueue.Count > 0)
            currentObjective = objectiveQueue.Peek();
        else
        {
            currentObjective = null;
            chapterManagerInstance.RequestChapterCompletion?.Invoke();
            AssignNewObjective();
        }
    }
    #endregion

    #region Helpers
    /// <summary>
    /// Assigns the new charactersubobjective and initilizes the Queue
    /// </summary>
    private void AssignNewObjective()
    {
        if (chapterManagerInstance.IsLastChapter())
            return;

        chapterSubObjective = chapterObjectives[(int)chapterManagerInstance.CurrentChapter].objectives;
        InitializeQueue(chapterSubObjective);
    }

    private void InitializeInstancesForUse()
    {
        chapterManagerInstance = ChapterManager.Instance;
    }
    #endregion
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