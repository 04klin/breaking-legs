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
    [SerializeField] private float spawn_speed;
    [SerializeField] private float spawn_height;
    private float time = 0;



    public void changeSpawnSpeed(float x)
    {
        spawn_speed = x;
    }    

    private void Update()
    {
        if (pause_menu.paused)
            return;

        time += Time.deltaTime;
        //creates an enemy
        if (time >spawn_speed)
        {
            //finds the pos of camera and its size
            float height = 2f * main_camera.orthographicSize;
            float width = height * main_camera.aspect;
            float left_side = main_camera.transform.position.x - width/2;
            float right_side = main_camera.transform.position.x + width/2 ;
          //replaces min and max with the border if it surpasses it.

            
            if(spawn)
            {
                Instantiate(enemy, new Vector3(Random.Range(left_side, right_side), spawn_height, Random.Range(0,1)), enemy.transform.rotation);
                
            }
            time = 0;
        }
    }


}
