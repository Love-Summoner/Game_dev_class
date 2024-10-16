using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keep_within_camera_image : MonoBehaviour
{
    public float y_limit;
    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);

        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }
}
