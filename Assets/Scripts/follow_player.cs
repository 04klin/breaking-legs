using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class follow_player : MonoBehaviour
{
    public Transform player_pos;


    // Update is called once per frame
    void Update()
    {

        transform.position = new Vector3(player_pos.position.x, player_pos.position.y + 3, transform.position.z);
    }
}
