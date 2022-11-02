using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main_menu_spawner : MonoBehaviour
{
    public GameObject enemy;
    public Camera Camera;
    [SerializeField] float width;
    [SerializeField] float height;
    [SerializeField] private float spawn_speed;

    private float timespawn = 0; //time that spawns

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    // makes enemy and deletes it after set seconds
    void Update()
    {
        timespawn += Time.deltaTime;

        if (timespawn > (1 / spawn_speed))
        {
            drop();
            timespawn = 0;
        }
    }

    public void drop()
    {
        Instantiate(enemy, new Vector3(Random.Range(width*-1,width), height, transform.position.z), transform.rotation);

    }
}
