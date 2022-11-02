using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using TMPro;
using UnityEngine;
using UnityEngine.VFX;

public class soda_spawn : MonoBehaviour
{
    public GameObject soda;
    public spawn_enemy enemy; 
    public gun_script gun;
    [SerializeField] private float first_soda_x;
    [SerializeField] private int interval;
    [SerializeField] private int max_bullet_increase;
    [SerializeField] private float reload_decrease_percent;
    [SerializeField] private float enemy_spawnrate_increase;
    [SerializeField] private float spawn_height;
    
    
    public stat_text stat;
     
    //spawn creates the first soda prefab.
    //set text changes the value within the prefab text
    void Start()
    {
        spawn();
        stat.set_text(max_bullet_increase, reload_decrease_percent, enemy_spawnrate_increase);
    }

    //creates soda
    public void spawn()
    {
        Instantiate(soda, new Vector3(first_soda_x, spawn_height, transform.position.z), transform.rotation);
        first_soda_x += interval;
    }
    //increases stat values of player and difficulty
    public void upgrade()
    {
        gun.max_ammo += max_bullet_increase;
        gun.max_reload_time = gun.max_reload_time * (1 - reload_decrease_percent);
        enemy.spawn_speed += enemy_spawnrate_increase;
        stat.instantiate();
        
    }
   
}
