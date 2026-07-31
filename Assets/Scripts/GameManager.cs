using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region References
    private ObjectiveManager objectiveManager;
    public ComputerSetup computerSetup; //required for some stuff
    #endregion
    #region Singleton
    public static GameManager Instance;
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion
    public Action CurrentObjectiveDone;

    #region UnitylifeCycle
    private void Start()
    {
        objectiveManager = ObjectiveManager.Instance;
    }
    private void OnEnable()
    {
        CurrentObjectiveDone -= CurrentObjectiveCompleted;
        CurrentObjectiveDone += CurrentObjectiveCompleted;
    }
    private void OnDisable()
    {
        CurrentObjectiveDone -= CurrentObjectiveCompleted;
    }
    #endregion

    private void CurrentObjectiveCompleted()
    {
        objectiveManager.RequestToCompleteObjective?.Invoke(objectiveManager.CurrentObjective);
    }
}
