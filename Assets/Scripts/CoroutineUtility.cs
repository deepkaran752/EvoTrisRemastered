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

    private IEnumerator WaitRoutine(
        Action callback,
        float delay)
    {
        yield return new WaitForSeconds(delay); //ingame seconds 

        callback?.Invoke();
    }

}
