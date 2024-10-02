using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;

    private GameObject target;
    private Rigidbody2D rb;

    private bool firing = false;
    void Awake()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");
        if (targets.Length == 0)
        {
            Destroy(gameObject);
            return;
        }
        target = targets[0];

        rb = GetComponent<Rigidbody2D>();
        firing = true;
    }

    private Vector2 distance = new Vector2();
    void FixedUpdate()
    {
        if (!firing)
            return;
        if (target == null)
            Destroy(gameObject);
        if (target != null)
        { 
            distance = target.transform.position - transform.position;

            rb.velocity = distance.normalized * speed;
        }
    }
}
