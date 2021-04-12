using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance = null;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        UnpauseGame();
    }

    public void PauseGame()
    {
        var pausables = FindObjectsOfType<MonoBehaviour>().OfType<IPausable>();
        foreach(IPausable pauseObject in pausables)
        {
            pauseObject.PauseGame();
        }

        Time.timeScale = 0;
        AppEvents.Invoke_MouseCursorEnable(true);
    }

    public void UnpauseGame()
    {
        var pausables = FindObjectsOfType<MonoBehaviour>().OfType<IPausable>();
        foreach (IPausable pauseObject in pausables)
        {
            pauseObject.UnpauseGame();
        }

        Time.timeScale = 1;
        AppEvents.Invoke_MouseCursorEnable(false);
    }

    private void OnDestroy()
    {
        UnpauseGame();
    }
}

interface IPausable
{
    void PauseGame();
    void UnpauseGame();
}
