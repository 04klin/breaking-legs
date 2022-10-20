using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //goes to game
    public void goToGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }
    //closes game
    public void quitGame()
    {
        Application.Quit();
    }
}
