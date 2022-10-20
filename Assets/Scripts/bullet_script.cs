using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_script : MonoBehaviour
{
    [Header("Components needed")]
    public Rigidbody2D rb;
    public float flipper;
    public SpriteRenderer sprite_renderer;

    [Header("Bullet Atributes")]
    [SerializeField] private float bullet_speed;
    [SerializeField] private Vector2 mouse_location;
    [SerializeField] private Vector2 spawn_location;
    [SerializeField] private Vector2 direction_vector;
    [SerializeField] private float angle;

    [Header("Duration of flight")]
    [SerializeField] private bool destroy;
    [SerializeField] private float flight_time_max;
    [SerializeField] private float flight_time;
    


    void Start()
    {
        spawn_location = transform.position;

        //read the angle the bullet in degrees and make it usable
        angle = convert_to_readable_angle(transform.eulerAngles.z);

        direction_vector = new Vector2(Mathf.Cos(angle*Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        
    }

    void Update()   //add collision 
    {
        rb.velocity = direction_vector * bullet_speed;

        flight_time += Time.deltaTime;

        //create own rotated boxcast that is composed of 4 raycasts that returns true if it hits something.
        //use "boxcast" to detect bullet collision
        //https://answers.unity.com/questions/148877/how-to-destroy-a-gameobject-when-hit-by-raycast.html 


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
}
