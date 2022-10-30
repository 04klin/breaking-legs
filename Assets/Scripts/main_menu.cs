using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class main_menu : MonoBehaviour
{
    
    //goes to game
    public void play_game()
    {
        SceneManager.LoadScene(1);
        pause_menu.resume();
    }

    //closes game
    public void quit_game()
    {
        Application.Quit();
    }
}
