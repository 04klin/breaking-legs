using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class bullet_script : MonoBehaviour
{
    [Header("Components needed")]
    public Rigidbody2D rb;
    public float flipper;
    public SpriteRenderer sprite_renderer;
    public LayerMask bullet_layer;
    public GameObject blood;

    [Header("Bullet Atributes")]
    [SerializeField] private float bullet_speed;
    [SerializeField] private Vector2 mouse_location;
    [SerializeField] private Vector2 spawn_location;
    [SerializeField] private Vector2 direction_vector;
    [SerializeField] private float angle;

    [Header("Duration of flight")]
    [SerializeField] private bool destroy;
    [SerializeField] private bool draw_hitbox;
    [SerializeField] private float flight_time_max;
    [SerializeField] private float flight_time;

    private Vector2 length_to_edge;
    private Vector2 top_right;
    private Vector2 bottom_right;
    private Vector2 top_left;
    private Vector2 bottom_left;




    void Start()
    {
        spawn_location = transform.position;

        //read the angle the bullet in degrees and make it usable
        angle = convert_to_readable_angle(transform.eulerAngles.z);

        direction_vector = new Vector2(Mathf.Cos(angle*Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));

        //sprite_renderer size * localsize = size of the spritebox in editor
        length_to_edge = (sprite_renderer.size * transform.localScale) / 2;

        //set coords of hitbox
        top_right = rotate_point(new Vector2(length_to_edge.x, length_to_edge.y), angle * Mathf.Deg2Rad) + new Vector2(transform.position.x, transform.position.y);
        bottom_right = rotate_point(new Vector2(length_to_edge.x, -length_to_edge.y), angle * Mathf.Deg2Rad) + new Vector2(transform.position.x, transform.position.y);
        top_left = rotate_point(new Vector2(-length_to_edge.x, length_to_edge.y), angle * Mathf.Deg2Rad) + new Vector2(transform.position.x, transform.position.y);
        bottom_left = rotate_point(new Vector2(-length_to_edge.x, -length_to_edge.y), angle * Mathf.Deg2Rad) + new Vector2(transform.position.x, transform.position.y);

    }

    void Update()   //add collision 
    {
        if (pause_menu.paused)
            return;

        rb.velocity = direction_vector * bullet_speed;

        flight_time += Time.deltaTime;

        //create own rotated boxcast that is composed of 4 raycasts that returns true if it hits something.
        //use "boxcast" to detect bullet collision


        //update front of bullet collision 
        top_right = rotate_point(new Vector2(length_to_edge.x, length_to_edge.y), angle * Mathf.Deg2Rad) + new Vector2(transform.position.x, transform.position.y);
        bottom_right = rotate_point(new Vector2(length_to_edge.x, -length_to_edge.y), angle * Mathf.Deg2Rad) + new Vector2(transform.position.x, transform.position.y);

        if (draw_hitbox)
        {
            Debug.DrawLine(top_left, bottom_left);
            Debug.DrawLine(bottom_left, bottom_right);
            Debug.DrawLine(bottom_right, top_right);
            Debug.DrawLine(top_right, top_left);
        }

        //check every edge
        RaycastHit2D hit = Physics2D.Linecast(top_left, top_right, ~bullet_layer);
        if (hit.collider == null)
        {
            hit = Physics2D.Linecast(bottom_right, bottom_left, ~bullet_layer);
        }

        //see if there is a hit
        if (hit.collider != null)
        {
            BoxCollider2D hit_collider = hit.collider.gameObject.GetComponent<BoxCollider2D>();

            Debug.Log(hit.collider.gameObject.layer);
            
            //makes blood and meth and destroys enemy
            if (hit.collider.gameObject.layer == 6 /* enemy layer */)
            {
                Instantiate(blood, hit.point, Quaternion.Euler(0, 0, angle - 180 - 90));
                enemy_movement x = hit.collider.gameObject.GetComponent<enemy_movement>();
                x.make_meth();
            }

            

            Destroy(this.gameObject);

        }

        //used to store back of bullet coords 1 frame behind
        top_left = rotate_point(new Vector2(-length_to_edge.x, length_to_edge.y), angle * Mathf.Deg2Rad) + new Vector2(transform.position.x, transform.position.y);
        bottom_left = rotate_point(new Vector2(-length_to_edge.x, -length_to_edge.y), angle * Mathf.Deg2Rad) + new Vector2(transform.position.x, transform.position.y);





        if (flight_time >= flight_time_max && destroy)
        {
            Destroy(this.gameObject);
        }
    }




    public float convert_to_readable_angle(float gun_angle)
    {
        //the angle is out of wack because flipping the sprite of the gun doesnt flip the rotation value
        //this makes the angle of the gun wacky and unusable for vector math
        if (flipper == 1)
        {
            return gun_angle;
        }
        else
        {
            return gun_angle - 180;
        }


    }

    public Vector2 rotate_point(Vector2 point, float angle_rad)
    {
        //simple rotation matrix

        Vector2 new_point = new Vector2(0, 0);

        new_point.x = point.x * Mathf.Cos(angle_rad) - point.y * Mathf.Sin(angle_rad);
        new_point.y = point.x * Mathf.Sin(angle_rad) + point.y * Mathf.Cos(angle_rad);

        return new_point;
    }
}
