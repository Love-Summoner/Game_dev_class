using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keep_within_camera_image : MonoBehaviour
{
    public float y_limit, y_min;

    private Vector2 distance;
    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);

        if(pos.y > y_limit)
        {
            pos.y = y_limit;
        }
        if(pos.y < y_min) 
        {
            pos.y = y_min;
        }

        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }
}
