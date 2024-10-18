using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_pointer : MonoBehaviour
{
    public Transform target;

    private Vector2 distance;
    void Update()
    {
        distance = target.position - transform.position;
        transform.right = distance.normalized;

        if(!target.GetComponent<Renderer>().isVisible)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        else
            transform.GetChild(0).gameObject.SetActive(false);
    }
}
