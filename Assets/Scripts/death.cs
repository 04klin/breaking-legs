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



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //trigger from collision.
   public void run()
    {
   
        death_controller.SetActive(true);
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
