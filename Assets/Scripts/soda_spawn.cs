using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soda_spawn : MonoBehaviour
{
    public GameObject soda;
    [SerializeField] private int distance;
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

    public void spawn()
    {
        Instantiate(soda, new Vector3(position,0,transform.position.z), transform.rotation);
        position += distance;
    }
}
