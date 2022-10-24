using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class death : MonoBehaviour
{


    public BoxCollider2D player_collider;
    public counter counting;
    public timer_bar_control time_bar;
    
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //trigger from collision.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //change to death within the if statement
        if(collision.gameObject.tag == "Enemy")
        {
           SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            counting.run();

        }
        if (collision.gameObject.tag == "Meth")
        {
            Destroy(collision.gameObject);
            time_bar.set_slider_value(time_bar.get_value() + 5000);
        }
    }

    

}
