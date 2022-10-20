using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    private bool paused = false;
    [SerializeField] private GameObject pauseMenu;

    public void quitGame()
    { 
        Application.Quit();
    }

    public void pauseGame()
    {
        
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        paused = !paused;
    }

    public void resumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        paused = !paused;
    }

    public void mainMenu()
    {
        SceneManager.LoadScene(0);
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        { 
            pauseGame();
        }
    }
}
