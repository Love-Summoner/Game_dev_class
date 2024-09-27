using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : MonoBehaviour
{
    [SerializeField] private float speed;

    private Transform player_loc;
    private Rigidbody2D rb;

    private void Start()
    {
        player_loc = GameObject.Find("Player").transform;
        rb = GetComponent<Rigidbody2D>();  
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            kill_player();
        }
    }

    private Vector2 distance = Vector2.zero;
    private void Update()
    {
        distance = player_loc.position - transform.position;

        if (Mathf.Abs(distance.x) > .1f)
            rb.velocity = new Vector2(speed * Mathf.Sign(distance.x), rb.velocity.y);
        if (Mathf.Abs(distance.y) > .1f)
            rb.velocity = new Vector2(rb.velocity.x, speed * Mathf.Sign(distance.y));
    }
    private void kill_player()
    {
        Debug.Log("player killed");
    }
}
