using Packages.Rider.Editor.UnitTesting;
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
            velocity.x -= speed;
        }
        if(Input.GetKey("d"))
        {
            velocity.x += speed;
        }
        if (Input.GetKey("space") && is_grounded())
        {
            velocity.y += jump_height;
        }

        
    }

    private bool is_grounded()
    {
        return Physics2D.BoxCast(player_collider.bounds.center, player_collider.bounds.size, 0f, Vector2.down,0.1f, ground_layer);
    }

    private void FixedUpdate()
    {
        rb.velocity = velocity;
    }

}