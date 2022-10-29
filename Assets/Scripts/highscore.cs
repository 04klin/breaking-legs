using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class highscore : MonoBehaviour
{


    public static float high_score;
    private float score;
    private float current_point;
    private float starting_point;




    //finds the beginning position and is the 0 score.
    void Start()
    {
        starting_point = transform.position.x;
    }

    // increases score 
    void Update()
    {
        current_point = transform.position.x - starting_point;
        if(current_point > score)
        {
            score = current_point;
        }
        
        if (score > high_score)
        {
            high_score = score;
        }
    }
    public float get_score()
    { 
        return score;
    }
    public float get_high_score()
    {
        return high_score;
    }
}
