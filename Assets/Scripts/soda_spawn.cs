using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soda_spawn : MonoBehaviour
{
    public GameObject soda;
    public spawn_enemy enemy; 
    public gun_script gun;
    [SerializeField] private int interval;
    [SerializeField] private int max_bullet_increase;
    [SerializeField] private float reload_decrease_percent;
    [SerializeField] private float enemy_spawnrate_increase;
    [SerializeField] private float spawn_height;
    // Start is called before the first frame update
    private int position;  
  
    void Start()
    {
        spawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //increases stats
    public void spawn()
    {
        Instantiate(soda, new Vector3(position, spawn_height, transform.position.z), transform.rotation);
        position += interval;
       


    }
    public void upgrade()
    {
        gun.max_ammo += max_bullet_increase;
        gun.max_reload_time = gun.max_reload_time * (1 - reload_decrease_percent);
        enemy.spawn_speed += enemy_spawnrate_increase;
    }
   
}
