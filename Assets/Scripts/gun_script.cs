using JetBrains.Annotations;
using Newtonsoft.Json.Serialization;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using TreeEditor;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class gun_script : MonoBehaviour
{
    [Header("Components needed")]
    public GameObject player;
    public GameObject bullet;
    public LayerMask ground_layer;
    



    [Header("Position Gun Tip point")] // tip_offset should be the height of the tip and the x should be where you want the bullet to spawn
    [SerializeField] private bool active;
    [SerializeField] private Vector2 arm_offset;
    [SerializeField] private Vector2 tip_offset;
    private Vector2 rotated_tip_offset = new Vector2(0,0);

    private float angle = 0;
    private float angle_deg = 0;
    private Vector2 tip_point;

    [Header("Recoil and Inaccuracy Controls")]
    [SerializeField] private int recoil_amplitude;
    [SerializeField] private float recoil_return_speed;
    private float recoil_angle = 0;
    [SerializeField] private int max_inaccuracy;
    private float current_inaccuracy;
    [SerializeField] private float stability;
    [SerializeField] private float reset_frames;
    [SerializeField] private float fire_rate;

    private float time;
    private int flipper = 1;




    void Update()
    {
        if (pause_menu.paused)
            return;

        player.transform.localScale = new Vector3(Mathf.Abs(player.transform.localScale.x) * flipper, player.transform.localScale.y, player.transform.localScale.z);

        Vector3 player_position = player.transform.position + new Vector3(arm_offset.x * Mathf.Sign(player.transform.localScale.x), arm_offset.y, 0);

        transform.position = new Vector3(player_position.x, player_position.y, transform.position.z);


        //start of mouse alignment section

        Vector2 mouse_location = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 gun_location = transform.position;


        angle = Mathf.Atan2(mouse_location.y - gun_location.y, mouse_location.x - gun_location.x);
        angle_deg = angle * Mathf.Rad2Deg;

        //setting up so the gun flips so it never goes upside down
        
        Vector2 true_tip_offset;

        if (angle_deg >= -90 && angle_deg <= 90)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            flipper = 1;
            true_tip_offset = tip_offset;
        }
        else
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            angle = angle - Mathf.PI; 
            angle_deg = angle_deg - 180; 
            flipper = -1;
            true_tip_offset = new Vector2(-tip_offset.x, tip_offset.y);
        }


        //fancy voodoo magic that calculates the couter rotation angle so the tip of the gun points to the mouse instead of the center of the rotation
        float length_handle_mouse = Vector2.Distance(gun_location, mouse_location);
        float height_gun = tip_offset.y;
        Vector2 point1 = gun_location;
        Vector2 point2 = new Vector2(gun_location.x, gun_location.y + height_gun);
        Vector2 point3 = new Vector2(gun_location.x + length_handle_mouse, gun_location.y);
        float counter_angle = 90 - Vector2.Angle(point1 - point2, point3 - point2); //angle between 3 points


        //adjust for recoil before rotating tip point
        recoil_angle = Mathf.Max(0, recoil_angle - recoil_return_speed);
        angle += recoil_angle * Mathf.Deg2Rad * flipper;
        angle_deg = angle * Mathf.Rad2Deg;

        //convert tip_offset to a worldspace point
        rotated_tip_offset = rotate_point(true_tip_offset, ( angle_deg - (counter_angle * flipper)) * Mathf.Deg2Rad);
        tip_point = new Vector2(transform.position.x + rotated_tip_offset.x, transform.position.y + rotated_tip_offset.y);


        //end of mouse alignment section

        //start of bullet and recoil
        time += Time.deltaTime;
        bool inside_something = Physics2D.Linecast(tip_point, tip_point,ground_layer);

        

        if (!inside_something && Input.GetMouseButton(0))
        {
            if (time >= fire_rate)
            {
                float shot_inaccuracy = Random.Range(-current_inaccuracy, current_inaccuracy);


                GameObject new_bullet = Instantiate(bullet, tip_point, transform.rotation * Quaternion.Euler(0, 0, shot_inaccuracy));
                new_bullet.GetComponent<bullet_script>().flipper = flipper;


                recoil_angle = recoil_amplitude;
                current_inaccuracy = Mathf.Min(max_inaccuracy, current_inaccuracy + stability);

                time = 0;
            }
        }
        else
        {
            current_inaccuracy = Mathf.Max(0, current_inaccuracy - stability / reset_frames);

            time = fire_rate;
        }

        //end of bullet and recoil


        //apply proper rotation on the sprite
        transform.rotation = Quaternion.Euler(0, 0, angle_deg - ((counter_angle) * flipper));




    }

    public Vector2 rotate_point(Vector2 point, float angle_rad)
    {
        //simple rotation matrix

        Vector2 new_point = new Vector2(0,0);

        new_point.x = point.x * Mathf.Cos(angle_rad) - point.y * Mathf.Sin(angle_rad);
        new_point.y = point.x * Mathf.Sin(angle_rad) + point.y * Mathf.Cos(angle_rad);

        return new_point;
    }


    //debug purposes
    private void OnDrawGizmos()
    {
        if (active)
        {
            Gizmos.color = new Color(1, 0, 0, 1f);
            Gizmos.DrawCube(tip_point, new Vector3(.1f, .1f, -.1f));
        }
        
    }

}
