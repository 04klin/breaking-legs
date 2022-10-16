using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class spawn_enemy : MonoBehaviour
{

    public GameObject enemy;
    private float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        time += Time.deltaTime;
        //creates an enemy
        if (time >3)
        {
            Instantiate(enemy, new Vector3(5,0,0), transform.rotation);
            time = 0;
        }
    }


}
