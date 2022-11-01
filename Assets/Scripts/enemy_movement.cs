using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class enemy_movement : MonoBehaviour
{
    [Header("Components needed")]
    private Transform billy_pos;
    public Rigidbody2D rb;
    public LayerMask ground_layer;
    public BoxCollider2D enemy_collider;
    public GameObject questionable_substance;
    public GameObject enemy_hit_sound;
    public SpriteRenderer sprite_renderer;
    public BoxCollider2D enemy_box;
    public Animator enemy_animate;

    [Header("Enemy Attributes")]
    public float speed = 3f;
    private Vector2 velocity;
    private enum animate_states
    {
        falling,
        landing,
        crawling
    }
    private animate_states current_state = animate_states.falling;
    private float landing_timer = 0;
    public float max_landing_time;
    


    
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

        if (is_grounded() && current_state == animate_states.falling)
        {
            current_state = animate_states.landing;
        }

        if (current_state == animate_states.landing)
        {
            landing_timer += Time.deltaTime;
        }

        if (landing_timer > max_landing_time)
        {
            current_state = animate_states.crawling;
        }

        enemy_animate.SetInteger("state", (int)current_state);



        //making the collider box fit the animations
        //not needed while crawling to reduce calculations and the hitbox does not change
        if (current_state != animate_states.crawling)
        {
            // x = left       z = right
            // y = bottom     w = top

            float percent_left = sprite_renderer.sprite.border.x / sprite_renderer.sprite.rect.width;
            float percent_right = (sprite_renderer.sprite.rect.width - sprite_renderer.sprite.border.z) / sprite_renderer.sprite.rect.width;
            float percent_top = (sprite_renderer.sprite.rect.height - sprite_renderer.sprite.border.w) / sprite_renderer.sprite.rect.height;
            float percent_bottom = sprite_renderer.sprite.border.y / sprite_renderer.sprite.rect.height;

            Vector2 new_size = new Vector2(percent_right - percent_left, percent_top - percent_bottom);
            Vector2 new_offset = new Vector2(((percent_right + percent_left) / 2f) - .5f, ((percent_top + percent_bottom) / 2f) - .5f);

            enemy_box.size = new_size;
            enemy_box.offset = new_offset;
        }



        velocity = new Vector2(0, rb.velocity.y);
        //won't move until it is grounded and awake
        if (current_state == animate_states.crawling)
        {
            if (transform.position.x > billy_pos.position.x)
            {
                //move and flip sprite to face right
                velocity += Vector2.left;
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y, transform.localScale.z);
            }
            if (transform.position.x < billy_pos.position.x)
            {
                //move and flip sprite to face left
                velocity += Vector2.right;
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
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

    public void make_meth()
    {
        Instantiate(questionable_substance, new Vector3(transform.position.x, transform.position.y, transform.position.y), transform.rotation);
        Destroy(this.gameObject);
        Instantiate(enemy_hit_sound);

    }
}
