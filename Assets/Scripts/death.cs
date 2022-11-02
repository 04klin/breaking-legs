using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;

public class death : MonoBehaviour
{
    [Header("Components needed")]
    public GameObject death_controller;
    public GameObject main_character;
    public TMP_Text score;
    public TMP_Text high_score;
    public highscore get_high_score;
    public AudioSource death_sound;
    public GameObject game_ui;
    public GameObject instructions;
    public GameObject new_ui;
    public AudioSource ingame_music;
    public static int play_music = 0;

    //plays music and doesnt restart when you die
    private void Start()
    {
        ingame_music = GameObject.FindGameObjectsWithTag("ingame music")[0].GetComponent<AudioSource>();
        if (play_music == 0)
        {
            ingame_music.Play();
            DontDestroyOnLoad(ingame_music);
        }
        play_music++;
        
    }

    //trigger from collision.
   public void run()
    {
        death_sound.Play(); 
        death_controller.SetActive(true);
        game_ui.SetActive(false);
        instructions.SetActive(false);
        new_ui.SetActive(false);
        score.text = "" + Math.Round(get_high_score.get_score(),1);
        high_score.text = "" + Math.Round(get_high_score.get_high_score(), 1);
        pause_menu.pause();
     
    
    }

    public void restart_game()
    {
        death_controller.SetActive(false);
        pause_menu.resume();
  
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
    //goes to main menu
    public void main_menu()
    { 
        
        Destroy(ingame_music.gameObject);
        SceneManager.LoadScene(0);

    }
    public void quit_game()
    {
        Application.Quit();
    }

    


}
