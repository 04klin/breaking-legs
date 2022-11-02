using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class spawn_enemy : MonoBehaviour
{
    [Header("Components needed")]
    public GameObject enemy;
    public Camera main_camera;

    [Header("Spawner Attributes")]
    [SerializeField] private bool spawn;
    public float spawn_speed;
    [SerializeField] private float spawn_height;
    private float time = 0;



   

    private void Update()
    {
        //if game is paused, nothing happens
        if (pause_menu.paused)
            return;

        time += Time.deltaTime;
        //creates an # of enemy per second
        if (time >(1/spawn_speed))
        {
            //finds the pos of camera and its size
            float height = 2f * main_camera.orthographicSize;
            float width = height * main_camera.aspect;
            float left_side = main_camera.transform.position.x - width/2;
            float right_side = main_camera.transform.position.x + width ;
          //replaces min and max with the border if it surpasses it.

            //makes enemy from above within a range
            if(spawn)
            {
                Instantiate(enemy, new Vector3(Random.Range(left_side, right_side), spawn_height, Random.Range(0,1)), enemy.transform.rotation);
                
            }
            time = 0;
        }
    }


}
