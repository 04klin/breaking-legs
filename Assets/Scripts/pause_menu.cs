using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pause_menu : MonoBehaviour
{

    public static bool paused = false;
    [SerializeField] private GameObject pause_menu_obj;
    public death music_delete;
    //quits game
    public void quit_game()
    {
        Application.Quit();
    }
    //pauses game
    public void pause_game()
    {

        pause_menu_obj.SetActive(true);
        pause();
    }
    //resumes game
    public void resume_game()
    {
        pause_menu_obj.SetActive(false);
        resume();
    }
    //uses death button main menu
    public void main_menu()
    {
        music_delete.main_menu();

    }

    void Update()
    {//pauses game if escape is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause_game();
        }
    }
    public static void pause()
    {
        Time.timeScale = 0;
        paused = true;
    }
    public static void resume()
    {
        Time.timeScale = 1;
        paused = false;
    }
}
