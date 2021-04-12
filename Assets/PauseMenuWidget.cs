using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuWidget : GameHUDWidget
{
    [SerializeField] private string ExitMenuSceneName;

    public void ResumeGame()
    {
        PauseManager.Instance.UnpauseGame();
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene(ExitMenuSceneName);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
