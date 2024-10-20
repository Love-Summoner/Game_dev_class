using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    public int target_number = 0;
    private Targeting targeting_system;

    private GameObject target;
    private Rigidbody2D rb;
    private Transform player_transform;

    private void Start()
    {
        player_transform = GameObject.Find("Player").transform;
        targeting_system = GameObject.Find("Targeting_system").GetComponent<Targeting>();
        distance = Camera.main.ScreenToWorldPoint(Input.mousePosition) - player_transform.position;
        rb = GetComponent<Rigidbody2D>();
    }
    
    public void Delete_bullet()
    {
        Destroy (gameObject);
    }
    private Vector2 distance = new Vector2();
    void FixedUpdate()
    {
        if ((player_transform.position - transform.position).sqrMagnitude > 300)
            Destroy(gameObject);

        rb.velocity = distance.normalized * speed;
    }
    private void Update()
    {
        transform.right = rb.velocity.normalized;
    }
}
