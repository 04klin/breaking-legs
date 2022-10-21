using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class spawn_enemy : MonoBehaviour
{

    public GameObject enemy;
    private float time = 0;
    public CompositeCollider2D floor_collider;
    public Camera camera;
    [SerializeField] private bool spawnEnemy;
    [SerializeField] private float spawnSpeed;
    

    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (pause_menu.paused)
            return;

        time += Time.deltaTime;
        //creates an enemy
        if (time >spawnSpeed)
        {
            //world pos to the bounds of the tilemap
            float left_side_max = floor_collider.bounds.center.x - floor_collider.bounds.size.x/2;
            float right_side_max = floor_collider.bounds.center.x + floor_collider.bounds.size.x / 2;
            //finds the pos of camera and its size
            float height = 2f * camera.orthographicSize;
            float width = height * camera.aspect;
            float left_side = camera.transform.position.x - width/2;
            float right_side = camera.transform.position.x + width/2 ;
          //replaces min and max with the border if it surpasses it.
            if(left_side < left_side_max)
            {
                left_side = left_side_max;
            }
            if (right_side > right_side_max)
            {
                right_side = right_side_max;
            }
            
            if(spawnEnemy == true)
            {
                Instantiate(enemy, new Vector3(Random.Range(left_side, right_side), 10, 0), transform.rotation);
                
            }
            time = 0;
        }
    }


}
