using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class bullet_display : MonoBehaviour
{
    //this is the size of a single pixel
    private float unit_pixel = 3.128f;
    public int sprite_height;
    public float unit_height;
    public int max_ammo;
    public int current_ammo;
    public RectTransform background_transform;
    public RectTransform foreground_transform;
    public TMP_Text text_box;
    public Image bullet_sprite;
    public gun_script gun;
    private float bottom;


    void Start()
    {
        bottom = background_transform.position.y - (background_transform.sizeDelta.y / 2);
    }


    void Update()
    {
        current_ammo = gun.get_current_ammo();
        max_ammo = gun.get_max_ammo();

        unit_height = unit_pixel * sprite_height;
        

        //sets the sprite in the UI to the correct amount of bullets    the -1f is to adjust for the rasterization issues with really precise floats
        background_transform.sizeDelta = new Vector2(background_transform.sizeDelta.x, (unit_height*max_ammo/ bullet_sprite.pixelsPerUnitMultiplier) - 1f);
        float new_center = bottom + (unit_height * max_ammo / bullet_sprite.pixelsPerUnitMultiplier) / 2;
        background_transform.position = new Vector3(background_transform.position.x, new_center, background_transform.position.z);


        foreground_transform.sizeDelta = new Vector2(foreground_transform.sizeDelta.x, (unit_height * current_ammo / bullet_sprite.pixelsPerUnitMultiplier) - 1f);
        float new_center_2 = bottom + (unit_height * current_ammo / bullet_sprite.pixelsPerUnitMultiplier) / 2;
        foreground_transform.position = new Vector3(foreground_transform.position.x, new_center_2, foreground_transform.position.z);

        text_box.text = current_ammo + "/" + max_ammo;
    }
}
