using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer_bar_control : MonoBehaviour
{

    public Slider slider;
    [SerializeField] private float speed = 5f;

    public void set_slider_value(float amount)
    {
        slider.value = amount;
    }

    private void Update()
    {
        slider.value -= speed;
    }

}
