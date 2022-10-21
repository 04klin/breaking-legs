using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    private bool paused = false;
    [SerializeField] private GameObject pauseMenu;
    //quits game
    public void quitGame()
    { 
        Application.Quit();
    }
    //pauses game
    public void pauseGame()
    {
        
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        paused = !paused;
    }
    //resumes game
    public void resumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        paused = !paused;
    }
    //goes to main menu
    public void mainMenu()
    {
        SceneManager.LoadScene(0);
    }
    // Update is called once per frame
    void Update()
    {//pauses game if escape is pressed
        if(Input.GetKeyDown(KeyCode.Escape))
        { 
            pauseGame();
        }
    }
}
