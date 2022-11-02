using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.Serialization;

public class stat_text : MonoBehaviour
{
    
    public GameObject the_camera;
    public TextMeshProUGUI stat;
    public GameObject stats_text;
    //gets variables for mag size, reload, and spawnrate and replaces it within the text prefab
    public void set_text(float magazine_size, float reload_decrease, float enemy_spawnrate)
    {
        stat.text = "+" + magazine_size + " bullets \n -" + reload_decrease + "% reload time \n +" + enemy_spawnrate + " spawnrate";
       
    }
    //instantiates text message slightly above player
    public void instantiate()
    {

        Instantiate(stats_text, new Vector3(the_camera.transform.position.x + 1, the_camera.transform.position.y + 1, transform.position.z), transform.rotation, transform);

    }
}
