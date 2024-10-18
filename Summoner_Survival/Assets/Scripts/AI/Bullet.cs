using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    public int target_number = 0;

    private GameObject target;
    private Rigidbody2D rb;
    private Transform player_transform;

    private bool firing = false;
    private void Start()
    {
        player_transform = GameObject.Find("Player").transform;
    }
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
            target = targets[Random.Range(0, targets.Length)];
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
        distance = player_transform.position - transform.position;
        if (!firing)
            return;
        if (target == null)
        {
            if (distance.sqrMagnitude > 15)
                Destroy(gameObject);
            else
                return;
        }
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
