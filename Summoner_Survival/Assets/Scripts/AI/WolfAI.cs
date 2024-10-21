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

    private Summon_stats stats;
    public animation_controller anim_controller;

    private void Start()
    {
        stats = GameObject.Find("Summon_stats").GetComponent<Summon_stats>();
        rb = GetComponent<Rigidbody2D>();
        attack_box = transform.GetChild(0).gameObject;
        default_target = GameObject.Find("Player");
        target_system = GameObject.Find("Targeting_system").GetComponent<Targeting>();
    }

    private Vector2 distance = Vector2.zero;
    void Update()
    {
        if (rb.velocity.magnitude > 0)
        {
            anim_controller.play_running_anim();
        }
        else
            anim_controller.set_idle();
        speed = stats.wolf_move_speed;
        if (target != null)
        {
            distance = target.transform.position - transform.position;
            Chase_target();
        }
        else
        {
            locked_on = false;
            Reset_state();
        }
        if (!is_targeting && target == null && target_system.target_exist(wolf_count))
        {
            search_for_target();
            return;
        }
        
    }
    private bool locked_on = false;
    private void Chase_target()
    {
        if (Mathf.Abs(distance.x) <= attack_range && Mathf.Abs(distance.y) <= 1f || locked_on)
        {
            locked_on = true;
            rb.velocity = Vector2.zero;
            if (!is_attacking)
                StartCoroutine(attack());
            target.GetComponent<Enemy_AI>().rooted = true;
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
        
        if (Mathf.Abs(distance.x) < 1 && Mathf.Abs(distance.y) <= 1f)
        {
            is_targeting = false;
            rb.velocity = Vector2.zero;
            return;
        }
        if (distance.sqrMagnitude > 1)
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
            target = target_system.wolf_target(0);
            if (target == null)
                return;
            is_targeting = true;
        }
    }

    private bool is_attacking = false;
    IEnumerator attack()
    {
        if(target != null) 
            target.GetComponent<Enemy_AI>().safe_damage(stats.melee_damage);
        is_attacking = true;
        attack_box.SetActive(true);
        yield return new WaitForSeconds(.05f);
        attack_box.SetActive(false);
        yield return new WaitForSeconds(stats.melee_speed);
        is_attacking = false;
        
    }
}
