using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public enum highlight_type 
{
    RITUAL_CIRCLE,
    FILL_CIRCLE
}
public class Highlight_handler : MonoBehaviour
{
    public highlight_type highlight_type;
    public GameObject item_highlight;
    public Sprite item_sprite, circle_sprite, filled_item_sprite;
    public float radius;
    void Start()
    {
        spawn_item_highlight();
    }

    // Update is called once per frame
    public void OnEnable()
    {
        switch(highlight_type)
        {
            case highlight_type.RITUAL_CIRCLE:
                
                break;
        }
    }
    private float true_radius = 0;
    public void spawn_item_highlight()
    {
        if (transform.childCount == 3)
            return;

        GameObject temp = Instantiate(item_highlight, transform.position, transform.rotation);
        temp.transform.parent = transform;
        float cur_angle = Mathf.PI/2, angle = (Mathf.PI * 2) / transform.childCount;

        temp.GetComponent<SpriteRenderer>().sprite = item_sprite;

        foreach(Transform t in transform)
        {
            t.localPosition = angular_position(cur_angle);
            cur_angle += angle;
        }
        true_radius = radius;
    }
    private Vector2 angular_position(float cur_angle)
    {
        float x_pos = true_radius * Mathf.Cos(cur_angle);
        float y_pos = true_radius * Mathf.Sin(cur_angle);



        return new Vector2(x_pos, y_pos);
    }
    private int placed_count = 0;
    public bool place_item()
    {
        int i = 0;
        foreach (Transform t in transform)
        {
            if(i == placed_count)
            {
                t.GetComponent<SpriteRenderer>().sprite = filled_item_sprite;
            }
            i++;
        }
        placed_count++;

        if(transform.childCount == placed_count)
            reset_sprites();

        return true;
    }
    private void reset_sprites()
    {
        placed_count = 0;
        foreach (Transform t in transform)
        {
            t.GetComponent<SpriteRenderer>().sprite = item_sprite;
        }
    }
}
