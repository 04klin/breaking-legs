using JetBrains.Annotations;
using Packages.Rider.Editor.UnitTesting;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor.Profiling;
using UnityEngine;
using UnityEngine.UI;

public class timer_bar_control : MonoBehaviour //kevin side takes this 
{

    public Slider slider;
    [SerializeField] private float speed = 5f;
    public death death_control;
    public float timer_blink_percent;
    public Color blink_color;
    private Color base_color;
    public GameObject methbar;
    private Image methbar_image;
    public float blink_speed;
    private float time;
    public float test;


    private void Start()
    {
        methbar_image = methbar.GetComponent<Image>();
        base_color = methbar_image.color;
    }

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

        time += Time.deltaTime;

        if (slider.value <= 0)
        {
            death_control.run();
        }

        if (get_percent_full() < timer_blink_percent)
        {
            time += Time.deltaTime;

            methbar_image.color = Color.Lerp(base_color, blink_color, Mathf.PingPong(time/blink_speed, 1));
            //meth_icon_image.color = Color.Lerp(Color.white, blink_color, Mathf.PingPong(time / blink_speed, 1));
        }
        else
        {
            methbar_image.color = base_color;
        }

        slider.value -= speed;

    }

    

}
