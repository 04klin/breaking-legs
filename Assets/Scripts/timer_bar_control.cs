using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class timer_bar_control : MonoBehaviour //kevin side takes this 
{

    public Slider slider;
    [SerializeField] private float speed = 5f;

    public void set_slider_value(float amount)
    {
        slider.value = amount;
    }

    public void add_percent(float amount)
    {
        //input negitive value to subtract
        slider.value += slider.maxValue * amount; 
    }

    public float get_percent_full()
    {
        return slider.value / slider.maxValue;
    }

    public float get_percent_full(float zero_percent)
    {
        float condition = (slider.value - (zero_percent * slider.maxValue)) / (slider.maxValue - (zero_percent * slider.maxValue));

        return condition < 0 ? 0 : condition;
    }

    public float get_max_value()
    {
        return slider.maxValue;
    }

    public float get_value()
    {
        return slider.value;
    }

    public void add_speed(float amount)
    {
        //input negitive value to subtract
        speed += amount;
    }


    void Update()
    {
        if (pause_menu.paused)
            return;

        slider.value -= speed;

    }

}
