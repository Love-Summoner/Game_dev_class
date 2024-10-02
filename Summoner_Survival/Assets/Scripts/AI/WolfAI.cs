using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class WolfAI : MonoBehaviour
{
    [SerializeField]private float speed = 5, attack_range = 1, attack_speed = .5f;
    [SerializeField] private LayerMask enemy_layer;
    private Rigidbody2D rb;
    private GameObject attack_box, default_target;
    private CircleCollider2D circle;

    private bool is_targeting = false;
    private GameObject target;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy" && !is_targeting)
        {
            target = collision.gameObject;
            is_targeting = true;
        }
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        attack_box = transform.GetChild(0).gameObject;
        default_target = GameObject.Find("Player");
        circle = GetComponent<CircleCollider2D>();
    }

    private Vector2 distance = Vector2.zero;
    private bool already_searched = false;
    void FixedUpdate()
    {
        if (target != null)
        {
            distance = target.transform.position - transform.position;
            Chase_target();
        }
        else
        {
            Reset_state();
        }
        if (!is_targeting && target == null && !already_searched)
        {
            search_for_target();
            return;
        }
        already_searched = false;
        
    }
    private void Chase_target()
    {
        if (Mathf.Abs(distance.x) < attack_range && Mathf.Abs(distance.y) <= 1f)
        {
            rb.velocity = Vector2.zero;
            if (!is_attacking)
                StartCoroutine(attack());
            return;
            
        }
        if (distance.sqrMagnitude > attack_range)
        {
            rb.velocity = distance.normalized * speed;
        }
        if ((distance.x > 0 && transform.localScale.x <= 0) || (distance.x < 0 && transform.localScale.x >= 0))
        {
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        }
        
    }
    private void Reset_state()
    {
        distance = default_target.transform.position - transform.position;
        
        if (Mathf.Abs(distance.x) < attack_range && Mathf.Abs(distance.y) <= 1f)
        {
            is_targeting = false;
            rb.velocity = Vector2.zero;
            return;
        }
        if (distance.sqrMagnitude > attack_range)
        {
            rb.velocity = distance.normalized * speed;
        }
        if ((distance.x > 0 && transform.localScale.x < 0) || (distance.x < 0 && transform.localScale.x > 0))
        {
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        }
        
    }
    private void search_for_target()
    {
        GameObject[] options = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject go in options)
        {
            if((go.transform.position - transform.position).sqrMagnitude < circle.radius)
            {
                target = go;
                already_searched = true;
                return;
            }
        }
    }

    private bool is_attacking = false;
    IEnumerator attack()
    {
        is_attacking = true;
        attack_box.SetActive(true);
        yield return new WaitForSeconds(attack_speed);
        attack_box.SetActive(false);
        is_attacking = false;
        if(target != null) 
            StartCoroutine(target.GetComponent<Enemy_AI>().Damage(1));
    }
}
