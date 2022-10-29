using Packages.Rider.Editor.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    [Header("Components needed")]
    public Rigidbody2D rb;
    public BoxCollider2D player_collider;
    public LayerMask ground_layer;
    public LayerMask enemy_layer;
    public Animator player_animate;
    public timer_bar_control timer_bar;
    public AudioSource jump_sound;



    



    [Header("Character Atributes")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jump_height = 5f;
    [SerializeField] private float allowed_jump_percent = .1f;
    [SerializeField] private float percent_jump_subtract = .1f;


    private Vector2 velocity;
    private bool grounded = true;
    private bool jumping = false;
    private enum animate_states
    {
        idle,
        walking,
        falling,
        raising,
        backward_walking
    }
    private int current_state = (int) animate_states.idle;

   

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (pause_menu.paused)
            return;

        velocity = new Vector2(0, rb.velocity.y);

        if (Input.GetKey("a"))
        {   
            velocity += Vector2.left;
        }
        if(Input.GetKey("d"))
        {
            velocity += Vector2.right;
        }

        //flipping sprite is based on gun 
        

        velocity.x *= speed;

        
        //the jumping state can only be changed when the ground state changes
        //used to prevent multiple jumps from happening before the players clears the is_grounded() check range
        bool was_grounded = grounded;
        grounded = is_grounded();
        if (grounded != was_grounded)
        {
            jumping = !grounded;
        }


        if (Input.GetKey("space") && !jumping && timer_bar.get_percent_full() > allowed_jump_percent)
        {
            jump_sound.Play();
            velocity = Vector2.up * jump_height;
            jumping = true;
            timer_bar.add_percent(-percent_jump_subtract);
        }


        //set the state for the animation player
        if (grounded && velocity.x != 0 && MathF.Sign(velocity.x) == MathF.Sign(transform.localScale.x))
        {
            current_state = (int) animate_states.walking;
        }
        else if (grounded && velocity.x != 0 && MathF.Sign(velocity.x) == -MathF.Sign(transform.localScale.x))
        {
            current_state = (int) animate_states.backward_walking;
        }
        else if (grounded && velocity.x == 0)
        {
            current_state = (int) animate_states.idle;
        }
        if (jumping && rb.velocity.y > 0)
        {
            current_state = (int) animate_states.raising;
        }
        else if (jumping && rb.velocity.y < 0)
        {
            current_state = (int) animate_states.falling;
        }

        


        player_animate.SetInteger("state", (int)current_state);
        rb.velocity = velocity;
    }

    private bool is_grounded()
    {
        bool what = Physics2D.BoxCast(player_collider.bounds.center, player_collider.bounds.size, 0f, Vector2.down,0.1f, ground_layer) ||

        /*remove this is to please kevin because he is a small brined monke who wants to jump off of radishes ------------------------------------------------------------------------*/
        Physics2D.BoxCast(player_collider.bounds.center, player_collider.bounds.size, 0f, Vector2.down, 0.1f, enemy_layer);

        return what;
    }

}