using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class spawn_enemy : MonoBehaviour
{

    public GameObject enemy;
    private float time = 0;
    public CompositeCollider2D floor_collider;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        time += Time.deltaTime;
        //creates an enemy
        if (time >3)
        {
            //world pos to the bounds of the tilemap
            float left_side = floor_collider.bounds.center.x - floor_collider.bounds.size.x/2;
            float right_side = floor_collider.bounds.center.x + floor_collider.bounds.size.x / 2;

            Instantiate(enemy, new Vector3(Random.Range(left_side, right_side),10,0), transform.rotation);
            time = 0;
        }
    }


}
