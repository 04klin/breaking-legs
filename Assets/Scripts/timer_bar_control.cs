using System.Collections;
using System.Collections.Generic;
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

    public void subtract_percent(float amount)
    {
        slider.value -= slider.maxValue * amount;
    }

    public void add_percent(float amount)
    {
        slider.value += slider.maxValue * amount; 
    }

    public float get_percent_full()
    {
        return slider.value / slider.maxValue;
    }

    public float get_max_value()
    {
        return slider.maxValue;
    }

    public float get_value()
    {
        return slider.value;
    }

    void Update()
    {
        slider.value -= speed;
    }

}
