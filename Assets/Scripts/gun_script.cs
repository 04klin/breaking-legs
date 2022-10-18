using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun_script : MonoBehaviour
{
    public GameObject player;
    public float test;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //this is off by a bit

        transform.position = player.transform.position;

        Vector2 mouse_location = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        Vector2 gun_location = Camera.main.WorldToViewportPoint(transform.position);

        float angle = Mathf.Atan2(mouse_location.y - gun_location.y, mouse_location.x - gun_location.x) * 180/Mathf.PI;

        test = angle;

        transform.rotation = Quaternion.Euler(0,0,angle);
    }

    private void Awake()
    {
        
    }
}
