using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class main_menu : MonoBehaviour
{
    public AudioSource music;
    public void Start()
    {
        music.Play();
    }
    //goes to game
    public void play_game()
    {
        death.play_music = 0;
        SceneManager.LoadScene(1);

        pause_menu.resume();
    }

    //closes game
    public void quit_game()
    {
        Application.Quit();
    }
}
