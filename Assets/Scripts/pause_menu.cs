using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pause_menu : MonoBehaviour
{

    private bool paused = false;
    [SerializeField] private GameObject pause_menu_obj;
    //quits game
    public void quit_game()
    {
        Application.Quit();
    }
    //pauses game
    public void pause_game()
    {

        pause_menu_obj.SetActive(true);
        Time.timeScale = 0;
        paused = !paused;
    }
    //resumes game
    public void resume_game()
    {
        pause_menu_obj.SetActive(false);
        Time.timeScale = 1;
        paused = !paused;
    }
    //goes to main menu
    public void main_menu()
    {
        SceneManager.LoadScene(0);
    }

    void Update()
    {//pauses game if escape is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause_game();
        }
    }
}
