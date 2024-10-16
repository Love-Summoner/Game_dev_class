using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    public int target_number = 0;

    private GameObject target;
    private Rigidbody2D rb;

    private bool firing = false;

    public void Select_Target()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");
        if (targets.Length == 0)
        {
            Destroy(gameObject);
            return;
        }
        else if(targets.Length <= target_number) 
        {
            target = targets[0];
        }
        else if(targets.Length > target_number) 
            target = targets[target_number];

        rb = GetComponent<Rigidbody2D>();
        firing = true;
    }
    public void Delete_bullet()
    {
        Destroy (gameObject);
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
    private void Update()
    {
        transform.right = rb.velocity.normalized;
    }
}
