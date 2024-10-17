using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private float min_distance, speed;
    [SerializeField] private Summon_materials material_type = 0;
    [SerializeField] public float exp_worth = 1;
    public bool is_interacting = false;
    private Transform player_loc;
    private Inventory player_inventory;
    private Main_circle circle;

    private void Start()
    {
        player_loc = GameObject.Find("Player").transform;
        player_inventory = player_loc.gameObject.GetComponent<Inventory>();
        circle = GameObject.Find("Main_circle").GetComponent<Main_circle>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(circle.Ritual_being_done && circle.current_ritual == STAT_TYPE.GROWTH)
            {
                circle.cur_exp += exp_worth;
            }
            player_inventory.level_up(exp_worth);

            Destroy(gameObject);
        }
    }
    private Vector2 distance = Vector2.zero;
    private void Update()
    {
        distance =  player_loc.position-transform.position;

        if(distance.SqrMagnitude() < min_distance)
        {
            is_interacting = true;
            
        }
        if(is_interacting)
        {
            if(Mathf.Abs(distance.x) > .1f)
                transform.position = new Vector2(transform.position.x + (Time.deltaTime * speed * Mathf.Sign(distance.x)), transform.position.y);
            if(Mathf.Abs(distance.y) > .1f)
                transform.position = new Vector2(transform.position.x, transform.position.y + (Time.deltaTime * speed * Mathf.Sign(distance.y)));
        }
    }
}
