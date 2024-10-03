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
    public int wolf_count = 0;

    private bool is_targeting = false;
    private GameObject target;
    private Targeting target_system;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        attack_box = transform.GetChild(0).gameObject;
        default_target = GameObject.Find("Player");
        target_system = GameObject.Find("Targeting_system").GetComponent<Targeting>();
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
        if (!is_targeting && target == null)
        {
            search_for_target();
            return;
        }
        
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
        if (target_system.target_exist(wolf_count))
        {
            target = target_system.get_target(wolf_count);
            is_targeting = true;
        }
    }

    private bool is_attacking = false;
    IEnumerator attack()
    {
        if(target != null) 
            StartCoroutine(target.GetComponent<Enemy_AI>().Damage(1));
        is_attacking = true;
        attack_box.SetActive(true);
        yield return new WaitForSeconds(attack_speed);
        attack_box.SetActive(false);
        is_attacking = false;
        
    }
}
