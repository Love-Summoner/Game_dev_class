using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam_track : MonoBehaviour
{
    [SerializeField] private Transform player_loc;
    [SerializeField] private float max_distance, speed;

    private Rigidbody2D rb;
    private Player_movement player;
    void Start()
    {
        rb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        player = rb.gameObject.GetComponent<Player_movement>();
    }

    private Vector2 distance = new Vector2 ();
    void Update()
    {
        distance = new Vector2 (player_loc.position.x - transform.position.x, player_loc.position.y - transform.position.y);

        if(distance.sqrMagnitude > .1f)
        {
            transform.position = new Vector3(transform.position.x + (Time.deltaTime * speed * distance.x), transform.position.y + (Time.deltaTime * speed * distance.y), transform.position.z);
        }
    }
}
