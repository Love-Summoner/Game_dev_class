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
        else if(collision.tag == "Player_bullet")
        {
            StartCoroutine(Damage(1));
            Destroy(collision.gameObject);
        }
        else if(collision.tag == "Melee_attack")
        {
            collision.gameObject.SetActive(false);
            rooted = true;
        }
    }
    public IEnumerator Damage(float damage)
    {
        yield return new WaitForSeconds(.1f);
        health-= damage;
        if (health <= 0 && gameObject != null)
        {
            Instantiate(experience, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    private bool rooted = false;
    private Vector2 distance = Vector2.zero;
    private void Update()
    {
        if(rooted)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        distance = player_loc.position - transform.position;

        rb.velocity = distance.normalized * speed;
    }
    private void kill_player()
    {
        Debug.Log("player killed");
    }
}
