using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class enemy_movement : MonoBehaviour
{
    public float speed = 3f;
    private Transform billy_pos;
    public Rigidbody2D rb;
    private Vector2 velocity;
    public LayerMask ground_layer; 
    public BoxCollider2D enemy_collider;
    public GameObject questionable_substance;
    public GameObject enemy;
    public GameObject enemy_hit;


    public void make_meth()
    { 
        Instantiate(questionable_substance, new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.y), enemy.transform.rotation);
        Destroy(enemy);
        Instantiate(enemy_hit);
        
    }
    // Start is called before the first frame update
    void Start()
    {
        billy_pos = GameObject.FindGameObjectsWithTag("Player")[0].transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (pause_menu.paused)
            return;

        velocity = new Vector2(0, rb.velocity.y);
        //won't move until it is grounded
        if (is_grounded())
        {
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
    //checks for grounded
    private bool is_grounded()
    {
        bool what = Physics2D.BoxCast(enemy_collider.bounds.center, enemy_collider.bounds.size, 0f, Vector2.down, 0.1f, ground_layer);

        return what;
    }
}
