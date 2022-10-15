using Packages.Rider.Editor.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float speed = 5f;
    public float jump_height = 5f;
    public Rigidbody2D rb;
    public BoxCollider2D player_collider;
    private Vector2 velocity;
    public LayerMask ground_layer;
    public bool grounded = true;
    public bool jumping = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        velocity = new Vector2(0, rb.velocity.y);

        if (Input.GetKey("a"))
        {   
            velocity += Vector2.left;
        }
        if(Input.GetKey("d"))
        {
            velocity += Vector2.right;
        }

        //flip sprite based on what direction player is going
        if (velocity.x != 0 && transform.localScale.x != velocity.x)
            transform.localScale = new Vector3(velocity.x, 1, 1);

        velocity.x *= speed;

        
        //the jumping state can only be changed when the ground state changes
        //used to prevent multiple jumps from happening before the players clears the is_grounded() check range
        bool was_grounded = grounded;
        grounded = is_grounded();
        if (grounded != was_grounded)
        {
            jumping = !grounded;
        }


        if (Input.GetKey("space") && !jumping)
        {
            Debug.Log("jump");
            velocity = Vector2.up * jump_height;
            jumping = true;
        }

        rb.velocity = velocity;

    }

    private bool is_grounded()
    {
        bool what = Physics2D.BoxCast(player_collider.bounds.center, player_collider.bounds.size, 0f, Vector2.down,0.1f, ground_layer);
         
        return what;
    }

}