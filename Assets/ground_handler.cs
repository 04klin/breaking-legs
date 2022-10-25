using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class ground_handler : MonoBehaviour
{
    public Transform player;
    public GameObject ground;
    private GameObject[] ground_list = new GameObject[2];
    private CompositeCollider2D[] collider_list = new CompositeCollider2D[2];
    private float collider_x_offset;
    

    // Start is called before the first frame update
    void Start()
    {
        ground_list[0] = ground;
        collider_list[0] = ground_list[0].GetComponentsInChildren<CompositeCollider2D>()[0]; //for quick refrence 
        ground_list[1] = Instantiate(ground,ground.transform.position,ground.transform.rotation);
        collider_list[1] = ground_list[1].GetComponentsInChildren<CompositeCollider2D>()[0];

        collider_x_offset = collider_list[0].bounds.center.x - ground_list[0].transform.position.x;

    }

    // Update is called once per frame
    void Update()
    {
        //if the player is not within the bounds then the other ground piece should be used as the main
        int standing_ground = 0;
        if (!in_bounds(player, collider_list[0]))
        {
            standing_ground = 1;
        }

        float new_center;
        if (player.position.x > collider_list[standing_ground].bounds.center.x)
        {
            //player is to the right of the center and the other ground should be moved to the right side
            new_center = collider_list[standing_ground].bounds.center.x + collider_list[standing_ground].bounds.size.x;
        }
        else
        {
            //player is to the left of the center and the other ground should be moved to the left side
            new_center = collider_list[standing_ground].bounds.center.x - collider_list[standing_ground].bounds.size.x;
        }

        Vector3 changing_transform = ground_list[1 - standing_ground].transform.position;

        ground_list[1 - standing_ground].transform.position = new Vector3(new_center - collider_x_offset, changing_transform.y, changing_transform.z);


    }


    public bool in_bounds(Transform player, CompositeCollider2D ground)
    {
        if (ground.bounds.center.x - ground.bounds.extents.x < player.position.x && player.position.x < ground.bounds.center.x + ground.bounds.extents.x)
        {
            return true;
        }
        return false;
    }

}
