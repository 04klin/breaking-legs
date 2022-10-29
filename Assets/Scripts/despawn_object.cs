using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class despawn_object: MonoBehaviour
{
    [SerializeField] private bool despawn;
    [SerializeField] private float despawn_time;
    private float time = 0;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > despawn_time && despawn)
        {
            Destroy(this.gameObject);
        }
    }
}
