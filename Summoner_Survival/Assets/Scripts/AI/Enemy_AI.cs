using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] private GameObject experience;

    private Transform player_loc;
    private Rigidbody2D rb;

    public float health = 3, exp_mod = 0;
    private Targeting target_system;
    private SpriteRenderer enemy_sprite;
    private Player_movement movement;
    private Summon_stats stats;
    private Main_circle ritual_tracker;
    public float base_exp = 1;
    public float probability_mod = 0;
    public Sprite health_sprite;

    private void Start()
    {
        stats = GameObject.Find("Summon_stats").GetComponent<Summon_stats>();
        player_loc = GameObject.Find("Player").transform;
        movement = player_loc.gameObject.GetComponent<Player_movement>();
        rb = GetComponent<Rigidbody2D>();
        target_system = GameObject.Find("Targeting_system").GetComponent<Targeting>();
        enemy_sprite = GetComponent<SpriteRenderer>();
        ritual_tracker = GameObject.Find("Main_circle").GetComponent<Main_circle>();
    }
    private bool hurting_player = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (health <= 0)
            return;
        if (collision.tag == "Player")
        {
            hurting_player = true;
            ritual();
        }
        else if(collision.tag == "Player_bullet")
        {
            collision.gameObject.GetComponent<Bullet>().Delete_bullet();
            StartCoroutine(Damage(stats.bullet_damage));
            
        }
        else if(collision.tag == "Flame_breath")
        {
            StartCoroutine(Damage(stats.breath_damage));
        }
        else if (collision.tag == "Melee_attack")
        {

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            hurting_player = false;
        }
    }
    IEnumerator Damage(float damage)
    {
        enemy_sprite.color = new Color(.75f, 0f, 0f, 1) ;
        yield return new WaitForSeconds(.1f);
        health-= damage;
        enemy_sprite.color = new Color(1, 1, 1, 1);
    }
    public void safe_damage(float i)
    {
        StartCoroutine(Damage(i));
    }
    public bool rooted = false, targeted = false;
    private Vector2 distance = Vector2.zero;
    private void Update()
    {
        if(hurting_player)
        {
            movement.hurt();
        }
        if(!is_dead && health <= 0)
            die();
        if (gameObject != null && is_dead)
        {
            StopAllCoroutines();
            ritual();
            Destroy(transform.parent.gameObject);
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

    private bool is_dead = false;
    public void die()
    {
        if (gameObject == null)
            return;
        GameObject temp = Instantiate(experience, transform.position, transform.rotation);
        float chance = Random.Range(0f, 1f);

        temp.GetComponent<Item>().exp_worth = 1 + exp_mod;
        if (chance >= .90f - probability_mod)
        {
            temp.GetComponent<Item>().exp_worth = (1+exp_mod) * 5;
            temp.GetComponent<SpriteRenderer>().color = new Color(0, 1, 1);
            if (chance >= .99f- probability_mod / 2)
            {
                temp.GetComponent<Item>().exp_worth = (1 + exp_mod) * 10;
                temp.GetComponent<SpriteRenderer>().color = new Color(1, 0, .894f);
                if(chance >= 1 -probability_mod / 10)
                {
                    temp.GetComponent<Item>().exp_worth = (1 + exp_mod) * 30;
                    temp.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
                }
                if (chance >= 1.02 - probability_mod / 10)
                {
                    temp.GetComponent<Item>().exp_worth = (1 + exp_mod) * 100;
                    temp.GetComponent<SpriteRenderer>().color = new Color(1, .858f, .24f);
                }
            }
            if(chance > .995)
            {
                temp.GetComponent<Item>().is_health = true;
                temp.GetComponent<SpriteRenderer>().sprite = health_sprite;
                temp.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
        is_dead = true;
        StopAllCoroutines();
    }
    private void ritual()
    {
        if (ritual_tracker.Ritual_being_done)
        {
            if (ritual_tracker.count_kills)
            {
                ritual_tracker.add_kill();
            }
            else if(ritual_tracker.current_ritual == STAT_TYPE.SPEED)
            {
                ritual_tracker.was_hit = true;
            }
        }
    }
}
