using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class point_towards_mouse : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = true;
    }

    private Vector2 distance;
    void Update()
    {
        distance = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        transform.right = distance.normalized;
    }
}
