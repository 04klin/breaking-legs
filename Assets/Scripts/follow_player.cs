using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class follow_player : MonoBehaviour
{
    public Transform billy_pos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = new Vector3(billy_pos.position.x, billy_pos.position.y, transform.position.z);
    }
}
