using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class gun_script : MonoBehaviour
{
    [Header("Components needed")]
    public GameObject player;
    public GameObject bullet;
    



    [Header("Position Gun Tip point")] // tip_offset is how far the tip is from the center of rotation
    [SerializeField] private bool active;
    [SerializeField] private Vector2 tip_offset;
    private Vector2 rotated_tip_offset = new Vector2(0,0);
    public float angle;
    public float angle_deg;
    private Vector2 tip_point;

    [SerializeField] private float recoil_amplitude;
    private float recoil_angle = 0;
    [SerializeField] private float shoot_speed;




    void Update()
    {
        Vector3 player_position = player.transform.position;

        transform.position = new Vector3(player_position.x, player_position.y, transform.position.z);


        //start of mouse alignment section

        Vector2 mouse_location = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 gun_location = transform.position;


        angle = Mathf.Atan2(mouse_location.y - gun_location.y, mouse_location.x - gun_location.x);
        angle_deg = angle * Mathf.Rad2Deg;

        //setting up so the gun flips so it never goes upside down
        float flipper = 1;
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


        //convert tip_offset to a worldspace point
        rotated_tip_offset = rotate_point(true_tip_offset, ( angle_deg - (counter_angle * flipper)) * Mathf.Deg2Rad);
        tip_point = new Vector2(transform.position.x + rotated_tip_offset.x, transform.position.y + rotated_tip_offset.y);


        //end of mouse alignment section

        //start of bullet and recoil

        if (Input.GetMouseButtonDown(0))
        {
            GameObject new_bullet = Instantiate(bullet, tip_point, transform.rotation);
            new_bullet.GetComponent<bullet_script>().flipper = flipper;
            
        }




        //apply proper rotation on the sprite
        transform.rotation = Quaternion.Euler(0, 0, angle_deg - (counter_angle * flipper));




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
