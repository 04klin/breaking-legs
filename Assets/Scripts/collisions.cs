using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class collisions : MonoBehaviour
{


    public BoxCollider2D player_collider;
    public timer_bar_control time_bar;
    public spawn_enemy spawner;
    public death death;
    public AudioSource pickup_sound;
    public AudioSource free_soda_sound;


    public soda_spawn soda_spawner;

    //trigger from collision.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //change to death within the if statement
        if (collision.gameObject.tag == "Enemy")
        {
            
            death.run();

        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Meth")
        {
            Destroy(collision.gameObject);
            pickup_sound.Play();
            time_bar.set_slider_value(time_bar.get_value() + 5000);
        }
        if (collision.gameObject.tag == "Free Soda")
        { 
            Destroy(collision.gameObject);
            soda_spawner.spawn();
            soda_spawner.upgrade();
            free_soda_sound.Play();
            
        }
    }



}
