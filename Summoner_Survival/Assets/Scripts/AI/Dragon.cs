using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour
{
    private GameObject shadow, breath, target;
    private Targeting target_system;
    private Summon_stats stats;

    private void Start()
    {
        stats = GameObject.Find("Summon_stats").GetComponent<Summon_stats>();
        shadow = transform.GetChild(0).gameObject;
        breath = transform.GetChild(1).gameObject;
        target_system = GameObject.Find("Targeting_system").GetComponent<Targeting>();
    }

    private bool on_cool_down = false, prepping_attack = false, attacking = false;
    void Update()
    {
        if(prepping_attack && target == null && target_system.random_target() != null)
        {
            target = target_system.random_target();
        }
        if (!on_cool_down)
        {
            target = target_system.random_target();
            if (target == null)
                return;
            StartCoroutine(dragon_behavior());
        }
    }
    private void FixedUpdate()
    {
        if(target != null)
        {
            transform.position = target.transform.GetChild(0).position;
        }
    }
    IEnumerator dragon_behavior()
    {
        on_cool_down = true;
        prepping_attack = true;
        yield return new WaitForSeconds(stats.cool_down);
        prepping_attack = false;
        shadow.SetActive(true);
        yield return new WaitForSeconds(stats.shadow_time);
        breath.SetActive(true);
        yield return new WaitForSeconds(stats.attack_time);
        breath.SetActive(false);
        shadow.SetActive(false);
        on_cool_down = false;
    }
}
