using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class counter : MonoBehaviour
{

    private static int count = 0;
    public TMP_Text x;


    // Update is called once per frame
    void Update()
    {
        x.text = count.ToString();
    }

    
    //increments the counter variable for the deaths
    public void run()
    {
       count++;
        x.text = count.ToString();
    }

  
}
