using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationManager : MonoBehaviour
{
    public static ApplicationManager instance;

    public GameEvent @SceneChange; // called by scene loader script when it's changing scenes. 

    private void OnEnable()
    {
        SceneChange.OnRaise.AddListener((x) => ResumeGame());
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1.0f; // don't forget to resume game on scene change as well. 
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    private void OnDestroy()
    {
        SceneChange.OnRaise.RemoveAllListeners();
    }
}
