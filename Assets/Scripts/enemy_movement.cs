using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class enemy_movement : MonoBehaviour
{
    public float speed = 3f;
    public Transform billy_pos;
    public Rigidbody2D rb;
    private Vector2 velocity;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        velocity = new Vector2(0, rb.velocity.y);

        if (transform.position.x > billy_pos.position.x)
        {
            //move and flip sprite to face right
            velocity += Vector2.left;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (transform.position.x < billy_pos.position.x)
        {
            //move and flip sprite to face left
            velocity += Vector2.right;
            transform.localScale = new Vector3(1, 1, 1);
        }

        velocity.x *= speed;

        rb.velocity = velocity;

    }
}
