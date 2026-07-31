using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// The main coroutine class, to unify stuff
/// </summary>
public class CoroutineUtility : MonoBehaviour
{
    public static CoroutineUtility Instance;

    #region Singelton
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }
    #endregion

    public static void InvokeAfter(Action callback = null, float delay = .5f)
    {
        Instance.StartCoroutine(Instance.WaitRoutine(callback, delay));
    }

    public static void InvokeWhen(Action callback = null, Func<bool> condition = null)
    {
        Instance.StartCoroutine(Instance.WaitUntill(callback, condition));
    }

    public static void While(Action callback, Func<bool> condition)
    {
        Instance.StartCoroutine(Instance.WhileRoutine(callback, condition));
    }

    private IEnumerator WhileRoutine(Action callback, Func<bool> condition)
    {
        while (condition())
        {
            callback?.Invoke();
            yield return null;
        }
    }

    private IEnumerator WaitRoutine(
        Action callback,
        float delay)
    {
        yield return new WaitForSeconds(delay); //ingame seconds 

        callback?.Invoke();
    }

    private IEnumerator WaitUntill(
        Action callback, 
        Func <bool> condition)
    {
        yield return new WaitUntil(condition);
        callback?.Invoke();
    }

}
