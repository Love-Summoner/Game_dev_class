using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] private GameObject experience;

    private Transform player_loc;
    private Rigidbody2D rb;

    public float health = 3;
    private Targeting target_system;
    private SpriteRenderer enemy_sprite;

    private void Start()
    {
        player_loc = GameObject.Find("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        target_system = GameObject.Find("Targeting_system").GetComponent<Targeting>();
        enemy_sprite = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (health <= 0)
            return;
        if (collision.tag == "Player")
        {
            kill_player();
        }
        else if(collision.tag == "Player_bullet")
        {
            collision.gameObject.GetComponent<Bullet>().Delete_bullet();
            StartCoroutine(Damage(1));
            
        }
        else if(collision.tag == "Melee_attack")
        {
            rooted = true;
        }
    }
    IEnumerator Damage(float damage)
    {
        enemy_sprite.color = new Color(.75f, 0f, 0f, 1) ;
        yield return new WaitForSeconds(.1f);
        health-= damage;
        enemy_sprite.color = new Color(1, 0, 0, 1);
    }

    public void safe_damage(float i)
    {
        StartCoroutine(Damage(i));
    }
    public bool rooted = false, targeted = false;
    private Vector2 distance = Vector2.zero;
    private void Update()
    {
        if(!is_dead && health <= 0)
            die();
        if (gameObject != null && is_dead)
        {
            StopAllCoroutines();
            Destroy(gameObject);
            return;
        }
        if (rooted)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        distance = player_loc.position - transform.position;

        rb.velocity = distance.normalized * speed;
    }
    private void kill_player()
    {
        
    }
    private bool is_dead = false;
    public void die()
    {
        if (gameObject == null)
            return;
        Instantiate(experience, transform.position, transform.rotation);
        is_dead = true;
        StopAllCoroutines();
    }
}
