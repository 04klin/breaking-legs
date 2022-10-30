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

    public GameObject death_controller;
    public GameObject main_character;
    public TMP_Text score;
    public TMP_Text high_score;
    public highscore get_high_score;
    public AudioSource death_sound;





    // Update is called once per frame
    void Update()
    {
        
    }

    //trigger from collision.
   public void run()
    {
        death_sound.Play(); 
        death_controller.SetActive(true);
        score.text = "" + Math.Round(get_high_score.get_score(),1);
        high_score.text = "" + Math.Round(get_high_score.get_high_score(), 1);
        pause_menu.pause_test();
     
    
    }

    public void restart_game()
    {
        death_controller.SetActive(false);
        pause_menu.resume_test();
  
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    //goes to main menu
    public void main_menu()
    {
        SceneManager.LoadScene(0);
    }
    public void quit_game()
    {
        Application.Quit();
    }

    


}
