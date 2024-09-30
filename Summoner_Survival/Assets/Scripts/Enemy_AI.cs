using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject experience;

    private Transform player_loc;
    private Rigidbody2D rb;

    private float health = 3;

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
        if(collision.tag == "Player_bullet")
        {
            health--;

            if(health <= 0)
            {
                Instantiate(experience, transform.position, transform.rotation);
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
        }
    }

    private Vector2 distance = Vector2.zero;
    private void Update()
    {
        distance = player_loc.position - transform.position;

        rb.velocity = distance.normalized * speed;
    }
    private void kill_player()
    {
        Debug.Log("player killed");
    }
}
